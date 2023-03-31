using Artillery.Data.Models.Enums;
using Artillery.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Artillery.Data.Common;
using Newtonsoft.Json;

namespace Artillery.DataProcessor.ImportDto;

public class ImportGunDto
{
    [JsonProperty("ManufacturerId")]
    public int ManufacturerId { get; set; }

    [JsonProperty("GunWeight")]
    [Range(ValidationConstants.MinGunWeight, ValidationConstants.MaxGunWeight)]
    public int GunWeight { get; set; }

    [JsonProperty("BarrelLength")]
    [Range(ValidationConstants.MinBarrelLength, ValidationConstants.MaxBarrelLength)]
    public double BarrelLength { get; set; }

    [JsonProperty("NumberBuild")]
    public int? NumberBuild { get; set; }

    [JsonProperty("Range")]
    [Range(ValidationConstants.MinGunRange, ValidationConstants.MaxGunRange)]
    public int Range { get; set; }

    [JsonProperty("GunType")]
    public string GunType { get; set; }

    [JsonProperty("ShellId")]
    public int ShellId { get; set; }

    [JsonProperty("Countries")]
    public ImportCountryIdDto[] Countries { get; set; }
}
