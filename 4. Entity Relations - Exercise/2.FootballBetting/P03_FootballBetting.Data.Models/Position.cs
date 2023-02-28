namespace P03_FootballBetting.Data.Models;

using System.ComponentModel.DataAnnotations;
using Common;

public class Position
{
    [Key]
    public int PositionId { get; set; }

    [MaxLength(ValidationConstants.MaxPositionNameLength)]
    public string Name { get; set; } = null!;
}
