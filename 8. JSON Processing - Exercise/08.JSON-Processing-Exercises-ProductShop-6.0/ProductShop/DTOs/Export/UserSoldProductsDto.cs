namespace ProductShop.DTOs.Export;

public class UserSoldProductsDto
{
    public UserSoldProductsDto()
    {
        this.SoldProducts = new HashSet<SoldProductsDto>();
    }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public ICollection<SoldProductsDto> SoldProducts { get; set; }
}
