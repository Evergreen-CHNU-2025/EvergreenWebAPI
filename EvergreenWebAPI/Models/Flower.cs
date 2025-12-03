namespace EvergreenWebAPI.Models;

public partial class Flower
{
    public Guid Id { get; set; }

    public string NameUa { get; set; } = null!;

    public string NameLat { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ImagePath { get; set; } = null!;

    public string Symbolics { get; set; } = null!;

    public string Meaning { get; set; } = null!;

    public virtual ICollection<TipsFlower> TipsFlowers { get; set; } = new List<TipsFlower>();

    public virtual ICollection<UserFavoriteFlower> UserFavoriteFlowers { get; set; } = new List<UserFavoriteFlower>();
}