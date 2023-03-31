namespace Artillery
{
    using Artillery.Data.Models;
    using Artillery.DataProcessor.ImportDto;
    using AutoMapper;

public class ArtilleryProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public ArtilleryProfile()
        {
            new MapperConfiguration(cfg =>
            {
                //Country
                this.CreateMap<ImportCountryDto, Country>();    


                //Manufacturer
                this.CreateMap<ImportManufacturerDto, Manufacturer>();

                //Shells 
                this.CreateMap<ImportShellDto, Shell>();

                //Guns
                this.CreateMap<ImportGunDto, Gun>()
                .ForMember(d => d.CountriesGuns, obj => obj.Ignore());
                
            });
        }
    }
}