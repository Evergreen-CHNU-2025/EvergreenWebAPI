namespace EvergreenWebAPI.DTOs;

public class RegisterUserDTO
{
    public IFormFile? Image { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}