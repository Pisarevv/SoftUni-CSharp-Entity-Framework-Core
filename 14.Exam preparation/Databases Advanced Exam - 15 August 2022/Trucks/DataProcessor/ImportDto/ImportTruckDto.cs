using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Trucks.Data.Common;

namespace Trucks.DataProcessor.ImportDto;

[XmlType("Truck")]
public class ImportTruckDto
{
    [XmlElement("RegistrationNumber")]
    [MaxLength(ValidationConstants.MaxRegistrationNumberLength)]
    [MinLength(ValidationConstants.MinRegistrationNumberLength)]
    [RegularExpression("[A-Z]{2}[0-9]{4}[A-Z]{2}")]
    public string RegistrationNumber { get; set; } = null!;

    [XmlElement("VinNumber")]
    [MaxLength(ValidationConstants.MaxVinNumberLength)]
    [MinLength(ValidationConstants.MinVinNumberLength)]
    public string VinNumber { get; set; } = null!;

    [XmlElement("TankCapacity")]
    [Range(ValidationConstants.MinTankCapacity, ValidationConstants.MaxTankCapacity)]
    public int TankCapacity { get; set; }

    [XmlElement("CargoCapacity")]
    [Range(ValidationConstants.MinMaxCargoCapacity, ValidationConstants.MaxCargoCapacity)]
    public int CargoCapacity { get; set; }

    [XmlElement("CategoryType")]
    [Range(0,3)]
    public int CategoryType { get; set; }

    [XmlElement("MakeType")]
    [Range(0,4)]
    public int MakeType { get; set; }
}
