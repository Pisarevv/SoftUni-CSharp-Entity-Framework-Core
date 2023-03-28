using Footballers.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Footballers.DataProcessor.ImportDto;

[XmlType("Coach")]
public class ImportCoachDto
{
    [XmlElement("Name")]
    [Required]
    [MaxLength(ValidationConstants.MaxCoachNameLength)]
    [MinLength(ValidationConstants.MinCoachNameLength)]
    public string Name { get; set; } = null!;

    [Required]
    [XmlElement("Nationality")]
    public string Nationality { get; set; } 

    [XmlArray("Footballers")]
    public ImportFootballerDto[] Footballers { get; set; } 

}
