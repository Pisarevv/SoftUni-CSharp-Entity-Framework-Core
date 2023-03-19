using System.Xml.Serialization;

namespace CarDealer.DTOs.Import;

[XmlType("partId")]
public class ImportPartById
{
    [XmlAttribute("id")]
    public int Id { get; set; }
}
