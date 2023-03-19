using System.Xml.Serialization;

namespace ProductShop.DTOs.Export;

public class ExportUsersWrapperDto
{
    [XmlElement("count")]
    public int Count { get; set; }

    [XmlArray("users")]
    public ExportUserProductsInfoDto[] Users { get; set; }
}
