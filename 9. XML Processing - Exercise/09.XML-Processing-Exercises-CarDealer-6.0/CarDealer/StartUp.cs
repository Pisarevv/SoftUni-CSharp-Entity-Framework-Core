using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.DTOs.Export;
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

            //string inputXml = File.ReadAllText("../../../Datasets/sales.xml");
            string result = GetSalesWithAppliedDiscount(context);
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

        //Problem 13
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();

            List<int> validCarIds = context.Cars
                                   .Select(c => c.Id)
                                   .ToList();

            var sales = xmlHelper.DeserializeCollection<ImportSaleDto>(inputXml);
            var validSales = new HashSet<Sale>();

            foreach(var saleDto in sales)
            {
                if(!validCarIds.Any(x => x == saleDto.CarId))
                {
                    continue;
                }

                Sale validSale = mapper.Map<Sale>(saleDto);
                validSales.Add(validSale);
            }

            context.Sales.AddRange(validSales);
            context.SaveChanges();

            return $"Successfully imported {validSales.Count}";
        }

        //Problem 14
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();

            var cars = context.Cars
                       .Where(c => c.TraveledDistance > 2000000)
                       .OrderBy(c => c.Make)
                       .ThenBy(c => c.Model)
                       .ProjectTo<ExportCarWithDistanceDto>(mapper.ConfigurationProvider)
                       .Take(10)
                       .ToArray();

            return xmlHelper.Serialize<ExportCarWithDistanceDto>(cars, "cars");
        }

        //Problem 15
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();

            var bmws = context.Cars
                       .Where(c => c.Make == "BMW")
                       .OrderBy(c => c.Model)
                       .ThenByDescending(c => c.TraveledDistance)
                       .ProjectTo<ExportBmwDto>(mapper.ConfigurationProvider)
                       .ToArray();

            return xmlHelper.Serialize<ExportBmwDto>(bmws, "cars");
        }

        //Problem 16
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();

            var suppliers = context.Suppliers
                            .Where(s => !s.IsImporter)
                            .ProjectTo<ExportSupplierDto>(mapper.ConfigurationProvider)
                            .ToArray();

            return xmlHelper.Serialize<ExportSupplierDto>(suppliers,"suppliers");
        }

        //Problem 17
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();

            var cars = context.Cars
                       .OrderByDescending(c => c.TraveledDistance)
                       .ThenBy(c => c.Model)
                       .Take(5)
                       .ProjectTo<ExportCarWithPartsDto>(mapper.ConfigurationProvider)
                       .ToArray();

            return xmlHelper.Serialize<ExportCarWithPartsDto>(cars, "cars");
        }

        //Problem 18
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();

            var customers = context.Customers
                            .Where(c => c.Sales.Count > 0)
                            .ProjectTo<ExportCustomerDto>(mapper.ConfigurationProvider)
                            .OrderByDescending(c => c.SpentMoney)
                            .ToArray();

            return xmlHelper.Serialize<ExportCustomerDto>(customers,"customers");
        }

        //Problem 19
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            IMapper mapper= CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();

            var sales = context.Sales
                        .ProjectTo<ExportSaleDto>(mapper.ConfigurationProvider)
                        .ToArray();

            return xmlHelper.Serialize<ExportSaleDto>(sales, "sales");
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