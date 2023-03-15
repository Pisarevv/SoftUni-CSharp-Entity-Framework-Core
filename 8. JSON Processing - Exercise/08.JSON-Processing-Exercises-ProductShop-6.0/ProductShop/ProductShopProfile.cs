using AutoMapper;
using ProductShop.DTOs.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile() 
        {
            var config = new MapperConfiguration(cfg =>
            {
                //User
                this.CreateMap<ImportUserDTO, User>();
            });
        }
    }
}
