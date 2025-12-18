using EvergreenWebAPI.DTOs;
using EvergreenWebAPI.Models;
using EvergreenWebAPI.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EvergreenWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private const string DefaultDropboxUserIconsFolderName = "UserIcons";
    private const string DefaultIconName = "Default-icon.png";

    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    private readonly IDropboxService _dropboxService;

    public UserController(ILogger<UserController> logger, IUserService userService, IDropboxService dropboxService)
    {
        _logger = logger;
        _userService = userService;
        _dropboxService = dropboxService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUserAsync([FromForm] RegisterUserDTO dto)
    {
        try
        {
            string fileName = dto.Image != null ? $"{Guid.NewGuid()}_{dto.Image.FileName}" : DefaultIconName;
            string iconPath = $"/{DefaultDropboxUserIconsFolderName}/{fileName}";

            var result = await _userService.RegisterUserAsync(dto, iconPath);

            if(result.IsSuccess && dto.Image != null)
                await _dropboxService.UploadFileAsync(fileName, dto.Image, DefaultDropboxUserIconsFolderName);

            return Ok(result);
        }
        catch(Exception exception)
        {
            _logger.LogError(exception.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost("Login")]
    public async Task<ActionResult<ServerResponse>> LoginUserAsync([FromForm] SignInUserDTO dto)
    {
        try
        {
            return await _userService.LoginUserAsync(dto);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserAsync([FromForm] UpdateUserDTO dto)
    {
        try
        {
            return Ok(await _userService.UpdateUserAsync(dto));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}