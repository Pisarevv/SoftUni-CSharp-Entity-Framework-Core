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
             MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                //Supplier
                this.CreateMap<ImportSupplierDto, Supplier>();
                this.CreateMap<Supplier, ExportSuppliersDto>()
                .ForMember(d => d.PartsCount, opt => opt.MapFrom(s => s.Parts.Count));

                //Part
                this.CreateMap<ImportPartDto,Part>();
                this.CreateMap<Part,ExportPartDto>()
                .ForMember(d => d.Price, obj => obj.MapFrom(s => s.Price.ToString("F2")));

                //Customer
                this.CreateMap<ImportCustomerDto, Customer>()
                .ForMember(d => d.BirthDate, opt => opt.MapFrom(s => DateTime.Parse(s.BirthDate,CultureInfo.InvariantCulture)));

                this.CreateMap<Customer, ExportCustomerSaleDto>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.BoughtCars, opt => opt.MapFrom(s => s.Sales.Count))
                .ForMember(d => d.SpentMoney, opt => opt.MapFrom(s => s.Sales.SelectMany(c => c.Car.PartsCars.Select(pc => pc.Part.Price)).Sum()));

                this.CreateMap<Customer, ExportCustomerDto>()
                .ForMember(d => d.BirthDate, opt => opt.MapFrom(s => s.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));

                //Sales
                this.CreateMap<ImporSalesDto,Sale>();

                this.CreateMap<Sale, ExportSaleWithDiscountDto>()
                .ForMember(d => d.Car, opt => opt.MapFrom(s => new ExportCarInfoDto
                {
                    Make = s.Car.Make,
                    Model = s.Car.Model,
                    TraveledDistance = s.Car.TraveledDistance
                }))
                .ForMember(d => d.CustomerName, opt => opt.MapFrom(s => s.Customer.Name))
                .ForMember(d => d.Discount, opt => opt.MapFrom(s => s.Discount.ToString("F2")))
                .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Car.PartsCars.Select(pc => pc.Part.Price).Sum().ToString()))
                .ForMember(d => d.PriceWithDiscount, opt => opt.MapFrom(s => Math.Round((s.Car.PartsCars.Select(pc => pc.Part.Price).Sum() * ((100 - s.Discount) / 100)),2).ToString("F2")));

                //Car
                this.CreateMap<Car, ExportToyotaDto>();

                this.CreateMap<Car, ExportCarInfoDbDto>()
                .ForMember(d => d.Parts, obj => obj.MapFrom(s => s.PartsCars.Select(ps => ps.Part)));

                this.CreateMap<ExportCarInfoDbDto, ExportCarDtoWrapper>()
                .ForMember(d => d.Car, obj => obj.MapFrom(s => new ExportCarInfoDto()
                {
                    Make = s.Make,
                    Model = s.Model,
                    TraveledDistance = s.TraveledDistance
                }))
                .ForMember(d => d.Parts, obj => obj.MapFrom(s => s.Parts));
                
             
            });
        }
    }
}
