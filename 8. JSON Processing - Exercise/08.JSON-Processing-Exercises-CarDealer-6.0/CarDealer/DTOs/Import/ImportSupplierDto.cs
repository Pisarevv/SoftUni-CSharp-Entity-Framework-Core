using Newtonsoft.Json;

namespace CarDealer.DTOs.Import;

public class ImportSupplierDto
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("isImporter")]
    public bool IsImporter { get; set; }
}
