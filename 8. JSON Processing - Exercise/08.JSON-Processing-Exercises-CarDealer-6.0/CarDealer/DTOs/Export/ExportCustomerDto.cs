using Newtonsoft.Json;

namespace CarDealer.DTOs.Export;

public class ExportCustomerDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string BirthDate { get; set; } = null!;
}
