using Boardgames.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Boardgames.Data.Models;

public class Creator
{
    public Creator()
    {
        this.Boardgames = new HashSet<Boardgame>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(ValidationConstants.MaxCreatorFirstNameLength)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(ValidationConstants.MaxCreatorLastNameLength)]
    public string LastName { get; set; } = null!;

    public ICollection<Boardgame> Boardgames { get; set; }

}
