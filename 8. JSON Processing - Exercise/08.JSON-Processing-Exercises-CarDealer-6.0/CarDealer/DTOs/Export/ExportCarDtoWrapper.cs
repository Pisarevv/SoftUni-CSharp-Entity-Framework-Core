
using Newtonsoft.Json;

namespace CarDealer.DTOs.Export;

public class ExportCarDtoWrapper
{
    [JsonProperty("car")]
    public ExportCarInfoDto Car {get; set;}

    [JsonProperty("parts")]
    public ExportPartDto[] Parts { get; set;}
}
