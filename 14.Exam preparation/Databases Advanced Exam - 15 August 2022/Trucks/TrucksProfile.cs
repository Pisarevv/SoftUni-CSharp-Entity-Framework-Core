namespace Trucks
{
    using AutoMapper;
    using Trucks.Data.Models;
    using Trucks.DataProcessor.ExportDto;
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

                this.CreateMap<Truck, ExportTruckDto>()
                .ForMember(d => d.Make, obj => obj.MapFrom(s => s.MakeType.ToString()));
               

                //Dispatcher
                this.CreateMap<ImportDespatcherDto, Despatcher>()
                .ForMember(d => d.Trucks, obj => obj.Ignore());

                this.CreateMap<Despatcher, ExportDespatcherDto>()
                .ForMember(d => d.TrucksCount, obj => obj.MapFrom(s => s.Trucks.Count))
                .ForMember(d => d.DespatcherName, obj => obj.MapFrom(s => s.Name))
                .ForMember(d => d.Trucks, obj => obj.MapFrom(s => s.Trucks.ToArray().OrderBy(t => t.RegistrationNumber)));

                //Client
                this.CreateMap<ImportClientDto, Client>()
                .ForMember(d => d.ClientsTrucks, obj => obj.Ignore());
            });
        }


    }
}
