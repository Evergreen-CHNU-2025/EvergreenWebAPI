using EvergreenWebAPI.DTOs;
using EvergreenWebAPI.Models;
using EvergreenWebAPI.Repositories.Abstractions;
using EvergreenWebAPI.Services.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EvergreenWebAPI.Services;

public class UserService : IUserService
{
    private const string DefaultDropboxUserIconsFolderName = "UserIcons";
    private const string DefaultIconName = "Default-icon.png";

    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    private readonly IDropboxService _dropboxService;

    public UserService(IUnitOfWork unitOfWork, IConfiguration configuration, IDropboxService dropboxService)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _dropboxService = dropboxService;
    }

    public async Task<ServerResponse> RegisterUserAsync(RegisterUserDTO dto, string iconPath)
    {
        if (await _unitOfWork.UserRepository.IsEmailExist(dto.Email))
            return new ServerResponse
            {
                IsSuccess = false,
                Message = $"Email {dto.Email} already exists."
            };

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = dto.Name,
            Email = dto.Email,
            IconPath = iconPath,
            PasswordHashed = HashPassword(dto.Password)
        };

        await _unitOfWork.UserRepository.CreateAsync(user);

        await _unitOfWork.SaveChangesAsync();

        return new ServerResponse
        {
            IsSuccess = true,
            Message = "User registered successfully."
        };
    }

    public async Task<ServerResponse> LoginUserAsync(SignInUserDTO dto)
    {
        var user = await _unitOfWork.UserRepository.FindByEmailAsync(dto.Email);

        if (user == null)
            return new ServerResponse
            {
                IsSuccess = false,
                Message = $"User not found with email: {dto.Email}"
            };

        if (!VerifyPassword(dto.Password, user.PasswordHashed))
            return new ServerResponse
            {
                IsSuccess = false,
                Message = "Wrong password."
            };

        var claims = await GenerateClaims(user);

        return new ServerResponse
        {
            IsSuccess = true,
            Message = await CreateAccessToken(claims, 5)
        };
    }

    public async Task<ServerResponse> UpdateUserAsync(UpdateUserDTO dto)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(dto.Id);

        if (user == null)
            return new ServerResponse
            {
                IsSuccess = false,
                Message = "User not fouund."
            };

        if (user.Email != dto.Email && await _unitOfWork.UserRepository.IsEmailExist(dto.Email))
            return new ServerResponse
            {
                IsSuccess = false,
                Message = "Email is already used by another user."
            };

        string[] fileInfoArray = user.IconPath!.Split('/', StringSplitOptions.RemoveEmptyEntries);
        string file = fileInfoArray[1];

        if(dto.Image != null)
        {
            if (file != DefaultIconName)
                await _dropboxService.DeleteFileAsync(user.IconPath);

            file = $"{Guid.NewGuid()}_{dto.Image.FileName}";
            string iconPath = $"/{DefaultDropboxUserIconsFolderName}/{file}";

            user.IconPath = iconPath;

            await _dropboxService.UploadFileAsync(file, dto.Image, DefaultDropboxUserIconsFolderName);
        }

        if (!string.IsNullOrWhiteSpace(dto.Name))
            user.Username = dto.Name;

        if (!string.IsNullOrWhiteSpace(dto.Email))
            user.Email = dto.Email;

        if (!string.IsNullOrWhiteSpace(dto.Password))
            user.PasswordHashed = HashPassword(dto.Password);

        _unitOfWork.UserRepository.UpdateAsync(user);

        await _unitOfWork.SaveChangesAsync();

        var claims = await GenerateClaims(user);

        return new ServerResponse
        {
            IsSuccess = true,
            Message = await CreateAccessToken(claims, 5)
        };
    }

    private static string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            iterations: 100_000,
            hashAlgorithm: HashAlgorithmName.SHA256,
            outputLength: 32);

        var result = new byte[48];
        Buffer.BlockCopy(salt, 0, result, 0, 16);
        Buffer.BlockCopy(hash, 0, result, 16, 32);

        return Convert.ToBase64String(result);
    }

    private static bool VerifyPassword(string password, string storedHash)
    {
        var bytes = Convert.FromBase64String(storedHash);

        var salt = bytes[..16];
        var stored = bytes[16..48];

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            iterations: 100_000,
            hashAlgorithm: HashAlgorithmName.SHA256,
            outputLength: 32);

        return CryptographicOperations.FixedTimeEquals(hash, stored);
    }

    private async Task<List<Claim>> GenerateClaims(User user)
    {
        string[] fileInfoArray = user.IconPath!.Split('/', StringSplitOptions.RemoveEmptyEntries);
        string folder = fileInfoArray[0], file = fileInfoArray[1];

        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new("Email", user.Email),
            new("Name", user.Username),
            new("Image", await _dropboxService.GetTemporaryLinkAsync(folder, file))
        };

        return claims;
    }

    private async Task<string> CreateAccessToken(List<Claim> claims, int tokenValidityInHours)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(tokenValidityInHours),
            claims: claims,
            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}