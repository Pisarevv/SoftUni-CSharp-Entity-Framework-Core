using Newtonsoft.Json;

namespace CarDealer.DTOs.Import;

public class ImportCustomerDto
{
    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    [JsonProperty("birthDate")]
    public string BirthDate { get; set; } = null!;

    [JsonProperty("isYoungDriver")]
    public bool IsYoungDriver { get; set; }
}
