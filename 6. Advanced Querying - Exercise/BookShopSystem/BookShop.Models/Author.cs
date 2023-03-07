namespace BookShop.Models;

using Common;
using System.ComponentModel.DataAnnotations;

public class Author
{
    [Key]
    public int AuthorId { get; set; }

    [MaxLength(ValidationConstants.AuthorFirstNameMaxLength)]
    public string? FirstName { get; set; }

    [MaxLength(ValidationConstants.AuthorLastNameMaxLength)]
    public string? LastName { get; set; } = null!;
}
