using System.Xml.Serialization;

namespace CarDealer.DTOs.Import;

[XmlType("Car")]
public class ImportCarDto
{
    [XmlElement("make")]
    public string? Make { get; set; }
    [XmlElement("model")]
    public string? Model { get; set; }
    [XmlElement("traveledDistance")]
    public long TravelledDistance { get; set; }
    [XmlArray("parts")]
    public ImportPartById[] Parts { get; set; }
}




