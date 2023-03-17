namespace CarDealer.DTOs.Export;

public class ExportCarInfoDbDto
{
    public string Make { get; set; } = null!;

    public string Model { get; set; } = null!;

    public long TraveledDistance { get; set; }

    public ExportPartDto[] Parts { get; set; }
}
