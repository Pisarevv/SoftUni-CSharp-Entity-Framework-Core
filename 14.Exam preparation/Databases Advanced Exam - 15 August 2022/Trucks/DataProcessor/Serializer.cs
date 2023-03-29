namespace Trucks.DataProcessor
{
    using AutoMapper;
    using Data;

    public class Serializer
    {
        public static string ExportDespatchersWithTheirTrucks(TrucksContext context)
        {
            throw new NotImplementedException();
        }

        public static string ExportClientsWithMostTrucks(TrucksContext context, int capacity)
        {
            throw new NotImplementedException();
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
