using P03_FootballBetting.Common;
using System.ComponentModel.DataAnnotations;

namespace P03_FootballBetting.Data.Models;


public class Color
{
    [Key]
    public int ColorId { get; set; }

    [MaxLength(ValidationConstants.MaxColorNameLength)]
    public string Name { get; set; } = null!;
}
