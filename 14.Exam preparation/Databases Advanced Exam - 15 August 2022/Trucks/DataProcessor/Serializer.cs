namespace Trucks.DataProcessor
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using ProductShop.Utilities;
    using Trucks.Data.Models.Enums;
    using Trucks.DataProcessor.ExportDto;

    public class Serializer
    {
        public static string ExportDespatchersWithTheirTrucks(TrucksContext context)
        {
            IMapper mapper = CreateMapper();
            IXmlHelper xmlHelper = new XmlHelper();

            var despatchers = context.Despatchers
                              .Where(d => d.Trucks.Count > 0)
                              .OrderByDescending(d => d.Trucks.Count)
                              .ThenBy(d => d.Name)
                              .ProjectTo<ExportDespatcherDto>(mapper.ConfigurationProvider)
                              .AsNoTracking()
                              .ToArray();

            var result = xmlHelper.Serialize<ExportDespatcherDto[]>(despatchers, "Despatchers");

            return result.ToString();
        }

        public static string ExportClientsWithMostTrucks(TrucksContext context, int capacity)
        {
            var clients = context.Clients
                          .Where(c => c.ClientsTrucks.Any(ct => ct.Truck.TankCapacity >= capacity))
                          .ToArray()
                          .Select(c => new
                          {
                              Name = c.Name,
                              Trucks = c.ClientsTrucks
                                         .Select(ct => ct.Truck)
                                         .Where(t => t.TankCapacity >= capacity)
                                         .Select(t => new
                                         {
                                             TruckRegistrationNumber = t.RegistrationNumber,
                                             VinNumber = t.VinNumber,
                                             TankCapacity = t.TankCapacity,
                                             CargoCapacity = t.CargoCapacity,
                                             CategoryType = t.CategoryType.ToString(),
                                             MakeType = t.MakeType.ToString()
                                         })
                                         .OrderBy(x => x.MakeType)
                                         .ThenByDescending(x => x.CargoCapacity)
                                         .ToArray()
                          })
                          .OrderByDescending(x => x.Trucks.Count())
                          .ThenBy(x => x.Name)
                          .Take(10)
                          .ToArray();

            var result = JsonConvert.SerializeObject(clients, Formatting.Indented);

            return result;
                          
        }

        private static IMapper CreateMapper()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TrucksProfile>();
            }));
        }
    }
}
