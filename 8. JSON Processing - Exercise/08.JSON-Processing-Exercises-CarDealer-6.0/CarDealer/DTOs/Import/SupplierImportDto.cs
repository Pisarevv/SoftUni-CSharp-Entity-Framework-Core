using Newtonsoft.Json;

namespace CarDealer.DTOs.Import;

public class SupplierImportDto
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("isImporter")]
    public bool IsImporter { get; set; }
}
