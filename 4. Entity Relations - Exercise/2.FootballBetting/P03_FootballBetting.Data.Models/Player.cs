namespace P03_FootballBetting.Data.Models;

using System.ComponentModel.DataAnnotations;
using Common;

public class Player
{
    [Key]
    public int PlayerId { get; set; }

    [MaxLength(ValidationConstants.MaxPlayerNameLength)]
    public string Name { get; set; } = null!;

    [MaxLength(ValidationConstants.MaxSquadNameLength)]
    public string SquadName { get; set; } = null!;

    public int TeamId { get; set; }

    public int PositionId { get; set; }

    public bool IsInjured { get; set; }
}
