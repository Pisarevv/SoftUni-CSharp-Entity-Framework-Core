using AutoMapper;
using AutoMapper.QueryableExtensions;
using Castle.Core.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductShop.Data;
using ProductShop.DTOs.Export;
using ProductShop.DTOs.Import;
using ProductShop.Models;
using System.Security.Cryptography.X509Certificates;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext context = new ProductShopContext();

            //string inputJson = File.ReadAllText(@"..\..\..\Datasets\categories-products.json");

            var result = GetProductsInRange(context);

            Console.WriteLine(result);


        }



        //Problem 1
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            IMapper mapper = CreateMapper();

            ICollection<User> validUsers = new HashSet<User>();

            var inputUsers = JsonConvert.DeserializeObject<List<ImportUserDto>> (inputJson);

            foreach (var userDto in inputUsers)
            {
                User validUser = mapper.Map<User>(userDto);    
                validUsers.Add(validUser);
            }

            context.Users.AddRange(validUsers);
            context.SaveChanges();

            return $"Successfully imported {validUsers.Count}";
        }

        //Problem 2
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            IMapper mapper = CreateMapper();

            ICollection<Product> validProducts = new HashSet<Product>();

            ImportProductDto[] ?inputProducts = JsonConvert.DeserializeObject<ImportProductDto[]> (inputJson);

            foreach(var productDto in inputProducts)
            {
                Product validProduct = mapper.Map<Product>(productDto);
                validProducts.Add(validProduct);
            }

            context.Products.AddRange(validProducts);
            context.SaveChanges();

            return $"Successfully imported {validProducts.Count}";

        }

        //Problem 3 
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            IMapper mapper = CreateMapper();

            ICollection<Category> validCategories = new HashSet<Category>();

            ImportCategoryDto[] ?inputCategories = JsonConvert.DeserializeObject<ImportCategoryDto[]>(inputJson);

            foreach (var categoryDto in inputCategories)
            {
                if (categoryDto.Name is null)
                {
                    continue;
                }
                Category validCategory = mapper.Map<Category>(categoryDto);
                validCategories.Add(validCategory);
            }
            context.Categories.AddRange(validCategories);
            context.SaveChanges();

            return $"Successfully imported {validCategories.Count}";
        }

        //Problem 4
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            IMapper mapper = CreateMapper();

            ICollection<CategoryProduct> validCategories = new HashSet<CategoryProduct>();

            ImportCategoryProductDto[] inputCategories = JsonConvert.DeserializeObject<ImportCategoryProductDto[]>(inputJson);

            foreach(var productCategoryDto in inputCategories)
            {
                CategoryProduct categoryProduct = mapper.Map<CategoryProduct>(productCategoryDto);
                validCategories.Add(categoryProduct);
            }

            context.CategoriesProducts.AddRange(validCategories);
            context.SaveChanges();

            return $"Successfully imported {validCategories.Count}";
        }

        //Problem 5
        public static string GetProductsInRange(ProductShopContext context)
        {
            IMapper mapper = CreateMapper();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateProjection<Product, ProductInRangeDto>()
                .ForMember(dto => dto.Seller, conf => conf.MapFrom(p => $"{p.Seller.FirstName} {p.Seller.LastName}"));
            });
            var camelCaseStrategy = SetCamelCaseStrategy();

            var products = context.Products
                           .Where(p => p.Price >= 500 && p.Price <= 1000)
                           .OrderBy(p => p.Price)
                           .ProjectTo<ProductInRangeDto>(configuration).ToList();

            string resultJson = JsonConvert.SerializeObject(products, new JsonSerializerSettings
            {
                ContractResolver = camelCaseStrategy,
                Formatting = Formatting.Indented
            });

            return resultJson;

        }







        private static IMapper CreateMapper()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            }));
        }

        private static DefaultContractResolver SetCamelCaseStrategy()
        {

            return new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy(),
            };
        }
    }
}