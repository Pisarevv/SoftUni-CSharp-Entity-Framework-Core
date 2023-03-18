using AutoMapper;
using AutoMapper.QueryableExtensions;
using ProductShop.Data;
using ProductShop.DTOs.Export;
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

            var inputXML = File.ReadAllText("../../../Datasets/products.xml");

            var result = ImportProducts(context,inputXML);
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

            var categories = xmlHelper.DeserializeCollection<ImportCategoryDto>(inputXml);
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

        //Problem 4
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            IMapper mapper= CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();

            var catategoriesProducts = xmlHelper.DeserializeCollection<ImportCategoryProductDto>(inputXml);

            var validCategoriesProducts = new HashSet<CategoryProduct>();

            foreach(var categoryDto in catategoriesProducts)
            {
                CategoryProduct validCategoryProduct = mapper.Map<CategoryProduct>(categoryDto);
                validCategoriesProducts.Add(validCategoryProduct);
            }

            context.CategoryProducts.AddRange(validCategoriesProducts);
            context.SaveChanges();

            return $"Successfully imported {validCategoriesProducts.Count}";
        }

        //Problem 5
        public static string GetProductsInRange(ProductShopContext context)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper= new XmlHelper();

            var products = context.Products
                           .Where(p => p.Price >= 500 && p.Price <= 1000)
                           .ProjectTo<ExportProductInRangeDto>(mapper.ConfigurationProvider)
                           .OrderBy(p => p.Price)
                           .Take(10)
                           .ToArray();
                           


            string result = xmlHelper.Serialize<ExportProductInRangeDto>(products, "Products");

            return result;
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