using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Castle.Core.Resource;
using Newtonsoft.Json;
using System.IO;
using System.Linq.Expressions;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext context = new CarDealerContext();

            string inputJson = File.ReadAllText(@"..\..\..\Datasets\customers.json");

            string result = ImportCustomers(context, inputJson);
            Console.WriteLine(result);

        }

        //Problem 9
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            IMapper mapper = CreateMapper();

            var inputSuppliers = JsonConvert.DeserializeObject<List<ImportSupplierDto>>(inputJson);

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

        //Problem 10
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            IMapper mapper = CreateMapper();

            ICollection<int> validSupplierIds = context.Suppliers.Select(supplier => supplier.Id).ToList();

            ICollection<ImportPartDto> inputParts = JsonConvert.DeserializeObject<ICollection<ImportPartDto>> (inputJson);

            ICollection<Part> validParts = new HashSet<Part>();
           

            foreach(var partDto in inputParts)
            {
                if (!validSupplierIds.Any(id => partDto.SupplierId == id))
                {
                    continue;
                }
                Part validPart = mapper.Map<Part>(partDto);
                validParts.Add(validPart);
            }

            context.Parts.AddRange(validParts);
            context.SaveChanges();

            return $"Successfully imported {validParts.Count}.";
        }


        //Problem 12
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            IMapper mapper = CreateMapper(); 
            
            ICollection<ImportCustomerDto> inputCustomers = JsonConvert.DeserializeObject<List<ImportCustomerDto>> (inputJson);
            ICollection<Customer> validCostumers = new HashSet<Customer>();

            foreach(var customerDto in inputCustomers)
            {
                Customer validCustomer = mapper.Map<Customer>(customerDto);
                validCostumers.Add(validCustomer);

            }
            context.Customers.AddRange(validCostumers);
            context.SaveChanges();

            return $"Successfully imported {validCostumers.Count}.";
        }




        private static IMapper CreateMapper()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile<CarDealerProfile>())); 
        }
    }
}