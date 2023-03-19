using AutoMapper;
using CarDealer.DTOs.Import;
using CarDealer.Models;

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


            });
        }
    }
}
