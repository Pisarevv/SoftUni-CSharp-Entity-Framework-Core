using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using ProductShop.Utilities;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext context = new CarDealerContext();

            string inputXml = File.ReadAllText("../../../Datasets/parts.xml");
            string result = ImportParts(context, inputXml);
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














        public static IMapper CreateMapper()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            }));
        }
    }
}