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

                this.CreateMap<Customer, ExportCustomerDto>()
                .ForMember(d => d.BirthDate, opt => opt.MapFrom(s => s.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));

                //Sales
                this.CreateMap<ImporSalesDto,Sale>();

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
