namespace EvergreenWebAPI.Models;

public partial class UserFavoriteFlower
{
    public Guid Id { get; set; }

    public Guid FlowerId { get; set; }

    public Guid UserId { get; set; }

    public virtual Flower Flower { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}