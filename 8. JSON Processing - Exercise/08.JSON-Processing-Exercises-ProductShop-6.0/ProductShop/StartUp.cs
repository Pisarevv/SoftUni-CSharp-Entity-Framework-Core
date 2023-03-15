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

            string inputJson = File.ReadAllText(@"..\..\..\Datasets\users.json");

            var result = ImportUsers(context, inputJson);

            Console.WriteLine(result);


        }



        //Problem 1
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            IMapper mapper = CreateMapper();

            ICollection<User> validUsers = new HashSet<User>();

            var inputUsers = JsonConvert.DeserializeObject<List<ImportUserDTO>> (inputJson);

            foreach (var userDto in inputUsers)
            {
                User validUser = mapper.Map<User>(userDto);    
                validUsers.Add(validUser);
            }

            context.Users.AddRange(validUsers);
            context.SaveChanges();

            return $"Successfully imported {validUsers.Count}";
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