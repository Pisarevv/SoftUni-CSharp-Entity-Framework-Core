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

            var inputXML = File.ReadAllText("../../../Datasets/users.xml");
           
            var result = ImportUsers(context, inputXML);
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


        public static IMapper CreateMapper()
        { 
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            }));
        }
    }
}