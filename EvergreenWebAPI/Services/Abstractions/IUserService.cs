using EvergreenWebAPI.DTOs;
using EvergreenWebAPI.Models;

namespace EvergreenWebAPI.Services.Abstractions;

public interface IUserService
{
    Task<ServerResponse> RegisterUserAsync(RegisterUserDTO dto, string iconPath);
    Task<ServerResponse> LoginUserAsync(SignInUserDTO dto);
    Task<ServerResponse> UpdateUserAsync(UpdateUserDTO dto);
}