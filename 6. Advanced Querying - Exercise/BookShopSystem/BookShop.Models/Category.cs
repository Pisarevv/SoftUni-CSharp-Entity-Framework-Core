namespace BookShop.Models;

using System.ComponentModel.DataAnnotations;
using Common;

public class Category
{
    public Category()
    {
        this.CategoryBooks = new HashSet<BookCategory>();
    }

    [Key]
    public int CategoryId { get; set; }

    [MaxLength(ValidationConstants.CategoryNameMaxLength)]
    public string Name { get; set; } = null!;

    public virtual ICollection<BookCategory> CategoryBooks { get; set; }

}
