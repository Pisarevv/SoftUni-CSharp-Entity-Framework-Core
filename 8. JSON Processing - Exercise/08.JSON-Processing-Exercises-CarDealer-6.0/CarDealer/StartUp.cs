using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.DTOs.Export;
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

            //string inputJson = File.ReadAllText(@"..\..\..\Datasets\customers.json");

            string result = GetCarsFromMakeToyota(context);
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

        //Problem 11
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            IMapper mapper = CreateMapper();

            ICollection<ImportCarDto> inputCars = JsonConvert.DeserializeObject<IList<ImportCarDto>> (inputJson);

            int validCars = 0;

            foreach (var carDto in inputCars)
            {
                ICollection<PartCar> carParts = new HashSet<PartCar>();

                int nextFreeId = context.Cars
                                .OrderByDescending(c => c.Id)
                                .Select(c => c.Id)
                                .FirstOrDefault();
                                
                                
                                
                               



                Car validCar = new Car()
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                    TraveledDistance = carDto.TravelledDistance,

                };

                foreach (var carPartId in carDto.PartsCars)
                {
                    PartCar part = new PartCar()
                    {
                        PartId = carPartId,
                        CarId = validCar.Id
                    };

                    carParts.Add(part);
                }

                validCar.PartsCars = carParts;

                context.Cars.Add(validCar);
                context.SaveChanges();
                validCars++;
            }


            return $"Successfully imported {validCars}.";
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

        //Problem 13
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var mapper = CreateMapper();

            ICollection<ImporSalesDto> inputSales = JsonConvert.DeserializeObject<List<ImporSalesDto>>(inputJson);
            ICollection<Sale> validSales = new HashSet<Sale>();

            foreach(var saleDto in inputSales)
            {
                Sale validSale = mapper.Map<Sale>(saleDto);
                validSales.Add(validSale);
            }

            context.Sales.AddRange(validSales);
            context.SaveChanges();

            return $"Successfully imported {validSales.Count}.";
        }

        //Problem 14
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            IMapper mapper = CreateMapper();

            var customers = context.Customers
                            .OrderBy(c => c.BirthDate)
                            .ThenBy(c => c.IsYoungDriver)
                            .ProjectTo<ExportCustomerDto>(mapper.ConfigurationProvider)
                            .ToArray();

            return JsonConvert.SerializeObject(customers, Formatting.Indented);
        }

        //Problem 15
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var mapper = CreateMapper();

            var cars = context.Cars
                       .Where(c => c.Make == "Toyota")
                       .OrderBy(c => c.Model)
                       .ThenByDescending(c => c.TraveledDistance)
                       .ProjectTo<ExportToyotaDto>(mapper.ConfigurationProvider)
                       .ToArray();

            return JsonConvert.SerializeObject (cars, Formatting.Indented);
        }



        private static IMapper CreateMapper()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile<CarDealerProfile>())); 
        }
    }
}