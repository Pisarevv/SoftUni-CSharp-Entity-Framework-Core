namespace ProductShop.Utilities;
public interface IXmlHelper
{
    public T Deserialize<T>(string inputXml);

    public T[] DeserializeCollection<T>(string inputXml);
}
