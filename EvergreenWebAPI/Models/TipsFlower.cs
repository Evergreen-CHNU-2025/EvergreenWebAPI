namespace EvergreenWebAPI.Models;

public partial class TipsFlower
{
    public Guid FlowerId { get; set; }

    public Guid TipId { get; set; }

    public string Description { get; set; } = null!;

    public virtual Flower Flower { get; set; } = null!;

    public virtual Tip Tip { get; set; } = null!;
}