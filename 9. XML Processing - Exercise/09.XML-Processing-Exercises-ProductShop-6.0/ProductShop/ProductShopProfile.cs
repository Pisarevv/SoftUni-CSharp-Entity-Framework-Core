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
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                //User
                this.CreateMap<ImportUserDto, User>();

                this.CreateMap<User, ExportUserAndSoldProductsDto>()
                .ForMember(s => s.SoldProducts, obj => obj.MapFrom(s => s.ProductsSold));
          

                //Product
                this.CreateMap<ImportProductDto, Product>();

                this.CreateMap<Product,ExportProductInRangeDto>()
                .ForMember(d => d.BuyerName, opt => opt.MapFrom(s => $"{s.Buyer.FirstName} {s.Buyer.LastName}"));

                this.CreateMap<Product, ExportProductDto>();
                
                

                //Category
                this.CreateMap<ImportCategoryDto, Category>();

                this.CreateMap<Category, ExportCategoryDto>()
                .ForMember(d => d.ProductsCount, obj => obj.MapFrom(s => s.CategoryProducts.Count))
                .ForMember(d => d.AverageProductsPrice, obj => obj.MapFrom(s => s.CategoryProducts.Select(cp => cp.Product.Price).Average()))
                .ForMember(d => d.TotalRevenue, obj => obj.MapFrom(s => s.CategoryProducts.Select(cp => cp.Product.Price).Sum()));

                //CategoryProduct
                this.CreateMap<ImportCategoryProductDto,CategoryProduct>();
            });
        }
    }
}
