namespace EvergreenWebAPI.Models;

public partial class FlowersHexColor
{
    public Guid Id { get; set; }

    public Guid FlowerId { get; set; }

    public Guid ColorId { get; set; }

    public virtual HexColor Color { get; set; } = null!;

    public virtual Flower Flower { get; set; } = null!;
}