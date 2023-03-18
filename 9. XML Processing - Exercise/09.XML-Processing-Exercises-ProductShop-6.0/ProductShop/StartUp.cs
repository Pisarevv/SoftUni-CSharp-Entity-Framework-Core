using ProductShop.Data;
using ProductShop.DTOs.Import;
using ProductShop.Utilities;
using System.Xml;
using System.Xml.Serialization;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext context = new ProductShopContext();

            var inputXML = File.ReadAllText("../../../Datasets/users.xml");
           
            var result = ImportProducts(context, inputXML);
            Console.WriteLine(result);
        }

        //Problem 1
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            throw new NotImplementedException();
        }
    }
}