namespace BookShop.Models;

using BookShop.Models.Enums;
using Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Book
{
    public Book()
    {
      this.BookCategories = new HashSet<BookCategory>();  
    }

    [Key]
    public int BookId { get; set; }

    [MaxLength(ValidationConstants.BookTitleMaxLength)]
    public string Title { get; set; } = null!;

    [MaxLength(ValidationConstants.BookDescriptionMaxLength)]
    public string Description { get; set; } = null!;

    public DateTime? ReleaseDate { get; set; }

    public int Copies { get; set; }

    public decimal Price { get; set; }

    public EditionType EditionType { get; set; }

    public AgeRestriction AgeRestriction { get; set; }

    [ForeignKey(nameof(Author))]
    public int AuthorId { get; set; }

    public Author Author { get; set; } = null!;
 
    public ICollection<BookCategory> BookCategories { get; set; }
}
