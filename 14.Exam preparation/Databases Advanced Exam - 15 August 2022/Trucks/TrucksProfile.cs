namespace Trucks
{
    using AutoMapper;
    using Trucks.Data.Models;
    using Trucks.DataProcessor.ImportDto;

    public class TrucksProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE OR RENAME THIS CLASS
        public TrucksProfile()
        {
            new MapperConfiguration(cfg =>
            {
                //Truck
                this.CreateMap<ImportTruckDto, Truck>();

                //Dispatcher
                this.CreateMap<ImportDespatcherDto, Despatcher>()
                .ForMember(d => d.Trucks, obj => obj.Ignore());

                //Client
                this.CreateMap<ImportClientDto, Client>()
                .ForMember(d => d.ClientsTrucks, obj => obj.Ignore());
            });
        }


    }
}
