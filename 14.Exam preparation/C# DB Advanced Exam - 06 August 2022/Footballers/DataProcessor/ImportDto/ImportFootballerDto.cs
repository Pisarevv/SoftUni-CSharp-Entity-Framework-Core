using Footballers.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Footballers.DataProcessor.ImportDto;

[XmlType("Footballer")]
public class ImportFootballerDto
{
    [XmlElement("Name")]
    [MaxLength(ValidationConstants.MaxFootballerNameLength)]
    [MinLength(ValidationConstants.MinFootballerNameLength)]
    public string Name { get; set; } = null!;

    [XmlElement("ContractStartDate")]
    [Required]
    public string ContractStartDate { get; set; }

    [Required]
    [XmlElement("ContractEndDate")]
    public string ContractEndDate { get; set; }

    [Required]
    [XmlElement("BestSkillType")]
    public int BestSkillType { get; set; }

    [Required]
    [XmlElement("PositionType")]
    public int PositionType { get; set; }


}
