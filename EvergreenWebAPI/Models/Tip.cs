namespace EvergreenWebAPI.Models;

public partial class Tip
{
    public Guid Id { get; set; }

    public string NameUa { get; set; } = null!;

    public virtual ICollection<TipsFlower> TipsFlowers { get; set; } = new List<TipsFlower>();
}