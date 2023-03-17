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

                //Part
                this.CreateMap<ImportPartDto,Part>();

                //Customer
                this.CreateMap<ImportCustomerDto, Customer>()
                .ForMember(d => d.BirthDate, opt => opt.MapFrom(s => DateTime.Parse(s.BirthDate,CultureInfo.InvariantCulture)));

                this.CreateMap<Customer, ExportCustomerDto>()
                .ForMember(d => d.BirthDate, opt => opt.MapFrom(s => s.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));

                //Sales
                this.CreateMap<ImporSalesDto,Sale>();
            });
        }
    }
}
