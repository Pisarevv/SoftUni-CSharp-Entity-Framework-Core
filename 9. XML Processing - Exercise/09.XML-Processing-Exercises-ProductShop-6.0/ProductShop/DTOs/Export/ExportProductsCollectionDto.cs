using System.Xml.Serialization;

namespace ProductShop.DTOs.Export;

[XmlType("SoldProduct")]
public class ExportProductsCollectionDto
{
    [XmlElement("count")]
    public int ProductsCount { get; set; }

    [XmlArray("products")]
    public ExportProductDto[] SoldProducts { get; set; }
}
