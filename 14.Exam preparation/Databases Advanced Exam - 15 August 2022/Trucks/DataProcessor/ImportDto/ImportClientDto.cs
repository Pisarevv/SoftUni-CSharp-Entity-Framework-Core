using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Trucks.Data.Common;

namespace Trucks.DataProcessor.ImportDto;


public class ImportClientDto
{
    [JsonProperty("Name")]
    [MaxLength(ValidationConstants.MaxClientNameLength)]
    [MinLength(ValidationConstants.MinClientNameLength)]
    public string Name { get; set; }

    [JsonProperty("Nationality")]
    [MaxLength(ValidationConstants.MaxClientNationalityLength)]
    [MinLength(ValidationConstants.MinClientNationalityLength)]
    public string Nationality { get; set; }

    [JsonProperty("Type")]
    public string Type { get; set; }

    [JsonProperty("Trucks")]
    public int[] Trucks { get; set; }
}
