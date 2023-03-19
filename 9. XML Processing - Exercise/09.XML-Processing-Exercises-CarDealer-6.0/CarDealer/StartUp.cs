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

            string inputXml = File.ReadAllText("../../../Datasets/suppliers.xml");
            string result = ImportSuppliers(context, inputXml);
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




        public static IMapper CreateMapper()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            }));
        }
    }
}