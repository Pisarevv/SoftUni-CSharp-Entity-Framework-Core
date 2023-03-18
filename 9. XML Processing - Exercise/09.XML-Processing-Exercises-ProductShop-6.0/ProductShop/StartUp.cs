using AutoMapper;
using ProductShop.Data;
using ProductShop.DTOs.Import;
using ProductShop.Models;
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

            var inputXML = File.ReadAllText("../../../Datasets/categories.xml");
           
            var result = ImportCategories(context, inputXML);
            Console.WriteLine(result);
        }

        //Problem 1
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            IXmlHelper xmlHelper = new XmlHelper();
            IMapper mapper = CreateMapper();

            var users = xmlHelper.Deserialize<ImportUserDto[]>(inputXml);

            var validUsers = new HashSet<User>();

            foreach (var userDto in users) 
            {
                User validUser = mapper.Map<User>(userDto);
                validUsers.Add(validUser);
            }

            context.Users.AddRange(validUsers);
            context.SaveChanges();

            return $"Successfully imported {validUsers.Count}";

        }

        //Problem 2
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();

            var products = xmlHelper.Deserialize<ImportProductDto[]>(inputXml);

            var validProducts = new HashSet<Product>();

            foreach(var productDto in products)
            {             
                Product validProduct = mapper.Map<Product>(productDto);
                validProducts.Add(validProduct);
            }

            context.Products.AddRange(validProducts);
            context.SaveChanges();

            return $"Successfully imported {validProducts.Count}";
        }

        //Problem 3
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            IMapper mapper= CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();

            var categories = xmlHelper.DeserializeCollection<ImportCategoriesDto>(inputXml);
            var validCategories = new HashSet<Category>();

            foreach(var categoryDto in categories)
            {
                Category category = mapper.Map<Category>(categoryDto);
                validCategories.Add(category);
            }

            context.Categories.AddRange(validCategories);
            context.SaveChanges();

            return $"Successfully imported {validCategories.Count}";
        }




        public static IMapper CreateMapper()
        { 
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            }));
        }
    }
}