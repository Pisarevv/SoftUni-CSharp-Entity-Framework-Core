using AutoMapper;
using CarDealer.DTOs.Export;
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
                this.CreateMap<Supplier,ExportSupplierDto>()
                .ForMember(d => d.ProductsCount, obj => obj.MapFrom(s => s.Parts.Count));

                //Part
                this.CreateMap<ImportPartDto,Part>();
                this.CreateMap<ImportPartById, PartCar>()
                .ForMember(d => d.PartId, obj => obj.MapFrom(s => s.Id));

                //Car
                this.CreateMap<ImportCarDto, Car>();
                this.CreateMap<Car, ExportCarWithDistanceDto>()
                .ForMember(d => d.TraveledDistance, obj => obj.MapFrom(s => s.TraveledDistance));
                this.CreateMap<Car, ExportBmwDto>();

                //Customer
                this.CreateMap<ImportCustomerDto,Customer>()
                .ForMember(d => d.BirthDate, obj => obj.MapFrom(s => DateTime.Parse(s.BirthDate, CultureInfo.InvariantCulture)));

                //Sale
                this.CreateMap<ImportSaleDto, Sale>();
            });
        }
    }
}
