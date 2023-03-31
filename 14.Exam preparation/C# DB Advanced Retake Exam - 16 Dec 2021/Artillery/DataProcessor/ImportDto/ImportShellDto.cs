using Artillery.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Artillery.DataProcessor.ImportDto;

[XmlType("Shell")]
public class ImportShellDto
{
    [XmlElement("ShellWeight")]
    [Range(ValidationConstants.MinShellWeight, ValidationConstants.MaxShellWeight)]
    public double ShellWeight { get; set; }

    [XmlElement("Caliber")]
    [MaxLength(ValidationConstants.MaxCaliberLength)]
    [MinLength(ValidationConstants.MinCaliberLength)]
    public string Caliber { get; set; } = null!;
}
