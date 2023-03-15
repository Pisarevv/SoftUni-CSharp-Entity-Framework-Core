namespace ProductShop.DTOs.Export;
public class SoldProductsUserDto
{
   
    public int Count { get; set; }

    public ICollection<ProductDto> Products { get; set; }
}
