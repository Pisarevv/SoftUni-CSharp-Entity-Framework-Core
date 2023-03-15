using AutoMapper;
using Newtonsoft.Json;
using ProductShop.Data;
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

            string inputJson = File.ReadAllText(@"..\..\..\Datasets\products.json");

            var result = ImportProducts(context, inputJson);

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


        private static IMapper CreateMapper()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            }));
        }
    }
}