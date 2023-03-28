namespace Footballers.DataProcessor.ImportDto;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Data.Common;

public class ImportTeamDto
{
    [JsonProperty("Name")]
    [Required]
    [MaxLength(ValidationConstants.MaxTeamNameLength)]
    [MinLength(ValidationConstants.MinTeamNameLength)]
    [RegularExpression("^[a-zA-Z0-9 .-]+$")]
    public string Name { get; set; } = null!;

    [JsonProperty("Nationality")]
    [Required]
    [MaxLength(ValidationConstants.MaxNationalityTextLength)]
    [MinLength(ValidationConstants.MinNationalityTextLength)]
    public string Nationality { get; set; } = null!;

    [Required]
    [JsonProperty("Trophies")]
    public int Trophies { get; set; }

    [JsonProperty("Footballers")]
    public int[] Footballers { get; set; }
}
