using AutoMapper;
using ProductShop.DTOs.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                //User
                this.CreateMap<ImportUserDto, User>();

                //Product
                this.CreateMap<ImportProductDto, Product>();
            });
        }
    }
}
