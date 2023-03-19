using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using ProductShop.Utilities;
using System.Linq;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext context = new CarDealerContext();

            string inputXml = File.ReadAllText("../../../Datasets/customers.xml");
            string result = ImportCustomers(context, inputXml);
            Console.WriteLine(result);


        }

        //Problem 9
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();

            var suppliers = xmlHelper.DeserializeCollection<ImportSupplierDto>(inputXml);

            var validSuppliers = new HashSet<Supplier>();

            foreach (var supplierDto in suppliers) 
            {
                if(supplierDto.Name == null)
                {
                    continue;
                }

                Supplier validSupplier = mapper.Map<Supplier>(supplierDto);
                validSuppliers.Add(validSupplier);
            }

            context.Suppliers.AddRange(validSuppliers);
            context.SaveChanges();

            return $"Successfully imported {validSuppliers.Count}";
        }

        //Problem 10
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();

            var validSupplierIds = context.Suppliers
                                   .Select(x => x.Id);

            var parts = xmlHelper.DeserializeCollection<ImportPartDto>(inputXml);
            var validParts = new HashSet<Part>();

            foreach (var partDto in parts)
            {
                if(partDto.SupplierId.Value == null || 
                    !validSupplierIds.Any(x => x == partDto.SupplierId.Value))
                {
                    continue;
                }

                Part validPart = mapper.Map<Part>(partDto);
                validParts.Add(validPart);

            }

            context.Parts.AddRange(validParts);
            context.SaveChanges();

            return $"Successfully imported {validParts.Count}";

        }


        //Problem 11
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();
            List<int> validPartsIds = context.Parts
                                  .Select(x => x.Id)
                                  .ToList(); 

            var cars = xmlHelper.DeserializeCollection<ImportCarDto>(inputXml);
            var validCars = new HashSet<Car>();

            foreach (var carDto in cars)
            {
                var validParts = new HashSet<PartCar>();

                if(carDto.Make == null ||
                    carDto.Model == null)
                {
                    continue;
                }

                Car validCar = mapper.Map<Car>(carDto);

                foreach(var partId in carDto.Parts.DistinctBy(x => x.Id))
                {
                    if(!validPartsIds.Any(x => x == partId.Id))
                    {
                        continue;
                    }
                    PartCar validPartCar = mapper.Map<PartCar>(partId);
                    validParts.Add(validPartCar);
                }

                validCar.PartsCars = validParts;

                validCars.Add(validCar);
               
            }

            context.Cars.AddRange(validCars);
            context.SaveChanges();

            return $"Successfully imported {validCars.Count}";
        }

        //Problem 12
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();

            var customers = xmlHelper.DeserializeCollection<ImportCustomerDto>(inputXml);

            var validCostumers = new HashSet<Customer>();

            foreach(var customerDto in customers)
            {
                if(customerDto.Name == null ||
                   customerDto.BirthDate == null)
                {
                    continue;
                }

                Customer validCustomer = mapper.Map<Customer>(customerDto);

                validCostumers.Add(validCustomer);
            }

            context.Customers.AddRange(validCostumers);
            context.SaveChanges();

            return $"Successfully imported {validCostumers.Count}";
        }











        public static IMapper CreateMapper()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            }));
        }
    }
}