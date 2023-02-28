namespace P03_FootballBetting.Data.Models;

using Common;
using System.ComponentModel.DataAnnotations;

public class Color
{
    [Key]
    public int ColorId { get; set; }

    [MaxLength(ValidationConstants.MaxColorNameLength)]
    public string Name { get; set; } = null!;
}
