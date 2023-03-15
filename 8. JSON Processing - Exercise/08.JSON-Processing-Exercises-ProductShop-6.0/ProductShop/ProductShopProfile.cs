using AutoMapper;
using ProductShop.DTOs.Export;
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
                this.CreateMap<ImportUserDto, User>();

                //Product
                this.CreateMap<ImportProductDto, Product>();
                this.CreateMap<Product, ProductInRangeDto>();

                //Category
                this.CreateMap<ImportCategoryDto, Category>();
                this.CreateMap<ImportCategoryProductDto, CategoryProduct>();
            });
        }
    }
}
