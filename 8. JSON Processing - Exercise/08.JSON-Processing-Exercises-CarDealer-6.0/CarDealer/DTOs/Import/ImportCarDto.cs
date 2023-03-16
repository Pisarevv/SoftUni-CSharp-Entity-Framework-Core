using CarDealer.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CarDealer.DTOs.Import;

public class ImportCarDto
{
    public ImportCarDto()
    {
        this.PartsCars = new HashSet<int>();
    }

    [JsonProperty("make")]
    public string Make { get; set; } = null!;

    [JsonProperty("model")]
    public string Model { get; set; } = null!;

    [JsonProperty("traveledDistance")]
    public long TravelledDistance { get; set; }

    [JsonProperty("partsId")]
    public ICollection<int> PartsCars { get; set; }
}
