namespace EvergreenWebAPI.DTOs;

public class FlowerInfoDTO
{
    public Guid Id { get; set; }

    public string NameUa { get; set; } = null!;

    public string NameLat { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ImagePath { get; set; } = null!;

    public string Symbolics { get; set; } = null!;

    public string Meaning { get; set; } = null!;

    public string InspectRecomendations { get; set; } = null!;

    public string[] Colors { get; set; } = null!;
}