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
                this.CreateMap<Part, ExportPartDto>();

                //Car
                this.CreateMap<ImportCarDto, Car>();
                this.CreateMap<Car, ExportCarWithDistanceDto>()
                .ForMember(d => d.TraveledDistance, obj => obj.MapFrom(s => s.TraveledDistance));
                this.CreateMap<Car, ExportBmwDto>();
                this.CreateMap<Car, ExportCarWithPartsDto>()
                .ForMember(d => d.Parts, obj => obj.MapFrom(s => s.PartsCars.Select(x => x.Part).OrderByDescending(p => p.Price)));

                //Customer
                this.CreateMap<ImportCustomerDto,Customer>()
                .ForMember(d => d.BirthDate, obj => obj.MapFrom(s => DateTime.Parse(s.BirthDate, CultureInfo.InvariantCulture)));
                this.CreateMap<Customer,ExportCustomerDto>()
                .ForMember(d => d.BoughtCars, obj => obj.MapFrom(s => s.Sales.Count))
                .ForMember(d => d.SpentMoney, obj => obj.MapFrom(s => Math.Truncate(100 * (s.Sales.SelectMany(sa => sa.Car.PartsCars.Select(pc => pc.Part.Price * (s.IsYoungDriver? 0.95m : 1))).Sum()))/100));

                //Sale
                this.CreateMap<ImportSaleDto, Sale>();
            });
        }
    }
}
