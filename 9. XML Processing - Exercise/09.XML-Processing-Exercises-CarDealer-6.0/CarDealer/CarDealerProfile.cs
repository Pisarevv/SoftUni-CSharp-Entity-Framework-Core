using AutoMapper;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using System.Globalization;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            new MapperConfiguration(cfg =>
            {
                //Supplier
                this.CreateMap<ImportSupplierDto,Supplier>();

                //Part
                this.CreateMap<ImportPartDto,Part>();
                this.CreateMap<ImportPartById, PartCar>()
                .ForMember(d => d.PartId, obj => obj.MapFrom(s => s.Id));

                //Car
                this.CreateMap<ImportCarDto, Car>();

                //Customer
                this.CreateMap<ImportCustomerDto,Customer>()
                .ForMember(d => d.BirthDate, obj => obj.MapFrom(s => DateTime.Parse(s.BirthDate, CultureInfo.InvariantCulture)));
                
            });
        }
    }
}
