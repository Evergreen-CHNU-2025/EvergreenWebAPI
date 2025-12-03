namespace EvergreenWebAPI.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string? IconPath { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHashed { get; set; } = null!;

    public virtual ICollection<UserFavoriteFlower> UserFavoriteFlowers { get; set; } = new List<UserFavoriteFlower>();
}