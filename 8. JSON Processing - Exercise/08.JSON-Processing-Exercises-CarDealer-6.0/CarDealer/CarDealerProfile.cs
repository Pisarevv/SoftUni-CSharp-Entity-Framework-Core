using AutoMapper;
using CarDealer.DTOs.Import;
using CarDealer.Models;

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
                this.CreateMap<ImportCustomerDto, Customer>();
            });
        }
    }
}
