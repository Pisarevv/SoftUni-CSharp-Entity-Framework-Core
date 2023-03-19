using AutoMapper;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
             
        }


        public static IMapper createMapper()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            }));
        }
    }
}