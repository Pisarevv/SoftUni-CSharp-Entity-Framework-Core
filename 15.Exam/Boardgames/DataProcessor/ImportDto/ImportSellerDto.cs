using Boardgames.Data.Common;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Boardgames.DataProcessor.ImportDto;

public class ImportSellerDto
{
    [JsonProperty("Name")]
    [MaxLength(ValidationConstants.MaxSellerNameLength)]
    [MinLength(ValidationConstants.MinSellerNameLength)]
    public string Name { get; set; } = null!;

    [JsonProperty("Address")]
    [MaxLength(ValidationConstants.MaxSellerAddressLength)]
    [MinLength(ValidationConstants.MinSellerAddressLength)]
    public string Address { get; set; } = null!;

    [JsonProperty("Country")]
    public string Country { get; set; } = null!;

    [JsonProperty("Website")]
    [RegularExpression("^www\\.[a-zA-Z0-9\\-]+\\.com$")]
    public string Website { get; set; } = null!;

    [JsonProperty("Boardgames")]
    public int[] Boardgames { get; set; }
}
