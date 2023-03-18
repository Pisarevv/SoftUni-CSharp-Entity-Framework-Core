using System.Xml.Serialization;

namespace ProductShop.DTOs.Export;

[XmlType("Category")]
public class ExportCategoryDto
{
    [XmlElement("name")]
    public string Name { get; set; } = null!;

    [XmlElement("count")]
    public int ProductsCount { get; set; }

    [XmlElement("averagePrice")]
    public decimal AverageProductsPrice { get; set; }

    [XmlElement("totalRevenue")]
    public decimal TotalRevenue { get; set; }
}
