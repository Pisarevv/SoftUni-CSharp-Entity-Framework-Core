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
                this.CreateMap<SupplierImportDto, Supplier>();

                //Part
                this.CreateMap<ImportPartDto,Part>();
            });
        }
    }
}
