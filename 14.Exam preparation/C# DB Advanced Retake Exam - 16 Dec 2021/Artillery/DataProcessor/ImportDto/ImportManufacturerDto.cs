using Artillery.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Artillery.DataProcessor.ImportDto;

[XmlType("Manufacturer")]
public class ImportManufacturerDto
{
    [XmlElement("ManufacturerName")]
    [MaxLength(ValidationConstants.MaxManufacturerNameLength)]
    [MinLength(ValidationConstants.MinManufacturerNameLength)]
    public string ManufacturerName { get; set; } 

    [XmlElement("Founded")]
    [MaxLength(ValidationConstants.MaxFoundedTextLength)]
    [MinLength(ValidationConstants.MinFoundedTextLength)]
    public string Founded { get; set; } 
}
