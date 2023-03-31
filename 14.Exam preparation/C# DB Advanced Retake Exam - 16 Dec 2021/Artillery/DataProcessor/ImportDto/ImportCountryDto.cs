using Artillery.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Artillery.DataProcessor.ImportDto;

[XmlType("Country")]
public class ImportCountryDto
{
    [XmlElement("CountryName")]
    [MaxLength(ValidationConstants.MaxCountryNameLength)]
    [MinLength(ValidationConstants.MinCountryNameLength)]
    public string CountryName { get; set; } 

    [XmlElement("ArmySize")]
    [Range(ValidationConstants.MinArmySize, ValidationConstants.MaxArmySize)]
    public int ArmySize { get; set; }

}
