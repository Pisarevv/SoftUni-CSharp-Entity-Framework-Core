namespace ProductShop.DTOs.Export;
public class UserDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int? Age { get; set; }

    public SoldProductsUserDto SoldProducts { get; set; }

}
