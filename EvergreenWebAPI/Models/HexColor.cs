namespace EvergreenWebAPI.Models;

public partial class HexColor
{
    public Guid Id { get; set; }

    public string Color { get; set; } = null!;

    public virtual ICollection<FlowersHexColor> FlowersHexColors { get; set; } = new List<FlowersHexColor>();
}