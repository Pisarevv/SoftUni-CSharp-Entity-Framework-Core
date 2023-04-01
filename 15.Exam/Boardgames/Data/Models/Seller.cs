using Boardgames.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Boardgames.Data.Models;

public class Seller
{
    public Seller()
    {
        this.BoardgamesSellers = new HashSet<BoardgameSeller>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(ValidationConstants.MaxSellerNameLength)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(ValidationConstants.MaxSellerAddressLength)]
    public string Address { get; set; } = null!;

    [Required]
    public string Country { get; set; } = null!;

    [Required]
    public string Website { get; set; } = null!;

    public ICollection<BoardgameSeller> BoardgamesSellers { get; set; }
} 
