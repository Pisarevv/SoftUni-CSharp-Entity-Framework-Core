using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext context = new CarDealerContext();

            string inputJson = File.ReadAllText(@"..\..\..\Datasets\suppliers.json");

            string result = ImportSuppliers(context, inputJson);
            Console.WriteLine(result);

        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            IMapper mapper = CreateMapper();

            var inputSuppliers = JsonConvert.DeserializeObject<List<SupplierImportDto>>(inputJson);

            ICollection<Supplier> validSuppliers = new HashSet<Supplier>();
            
            foreach(var supplier in inputSuppliers)
            {
                Supplier validSupplier = mapper.Map<Supplier>(supplier);
                validSuppliers.Add(validSupplier);
            }

            context.Suppliers.AddRange(validSuppliers);
            context.SaveChanges();

            return $"Successfully imported {validSuppliers.Count}.";
        }

        private static IMapper CreateMapper()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile<CarDealerProfile>())); 
        }
    }
}