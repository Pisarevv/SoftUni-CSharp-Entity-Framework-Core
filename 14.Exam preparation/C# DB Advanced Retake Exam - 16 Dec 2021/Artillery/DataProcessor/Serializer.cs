
namespace Artillery.DataProcessor
{
    using Artillery.Data;
    using AutoMapper;

    public class Serializer
    {
        public static string ExportShells(ArtilleryContext context, double shellWeight)
        {
            throw new NotImplementedException();
        }

        public static string ExportGuns(ArtilleryContext context, string manufacturer)
        {
            throw new NotImplementedException();
        }

        private static IMapper CreateMapper()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ArtilleryProfile>();
            }));
        }


    }

    
}
