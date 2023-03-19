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

            //var inputXML = File.ReadAllText("../../../Datasets/products.xml");

            var result = GetUsersWithProducts(context);
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

        //Problem 6
        public static string GetSoldProducts(ProductShopContext context)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper= new XmlHelper();

            var users = context.Users
                        .Where(u => u.ProductsSold.Count > 1)
                        .OrderBy(u => u.LastName)
                        .ThenBy(u => u.FirstName)
                        .Take(5)
                        .ProjectTo<ExportUserAndSoldProductsDto>(mapper.ConfigurationProvider)
                        .ToArray();

            string result = xmlHelper.Serialize<ExportUserAndSoldProductsDto>(users, "Users");

            return result;
        }

        //Problem 7
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper= new XmlHelper();

            var categories = context.Categories
                             .ProjectTo<ExportCategoryDto>(mapper.ConfigurationProvider)
                             //.ToArray()
                             .OrderByDescending(c => c.ProductsCount)
                             .ThenBy(c => c.TotalRevenue)
                             .ToArray();

            string result = xmlHelper.Serialize<ExportCategoryDto>(categories, "Categories");

            return result;
        }


        //Problem 8
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper= new XmlHelper();

            var users = context.Users
                        .Where(u => u.ProductsSold.Any(x => x.Buyer != null))
                        .Select(u => new ExportUserProductsInfoDto
                        {
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Age = u.Age,
                            SoldProductsCollection = new ExportProductsCollectionDto
                            {
                                ProductsCount = u.ProductsSold.Count(x => x.BuyerId != null),
                                SoldProducts = u.ProductsSold
                                               .Where(p => p.BuyerId != null)
                                               .Select(p => new ExportProductDto
                                               {
                                                   Name = p.Name,
                                                   Price = p.Price
                                               })
                                               .OrderByDescending(p => p.Price)
                                               .ToArray()



                            }
                        })
                        .OrderByDescending(u => u.SoldProductsCollection.ProductsCount)
                        .ToArray();
               


            var userWarpper = new ExportUsersWrapperDto
            {
                Count = users.Length,
                Users = users.Take(10).ToArray(),
            };



            string result = xmlHelper.Serialize<ExportUsersWrapperDto>(userWarpper, "Users");

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