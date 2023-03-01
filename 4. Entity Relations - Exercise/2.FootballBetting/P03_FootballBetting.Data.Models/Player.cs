namespace P03_FootballBetting.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common;

public class Player
{
    [Key]
    public int PlayerId { get; set; }

    [MaxLength(ValidationConstants.MaxPlayerNameLength)]
    public string Name { get; set; } = null!;

    [MaxLength(ValidationConstants.MaxSquadNameLength)]
    public string SquadName { get; set; } = null!;

    [ForeignKey(nameof(Team))]
    public int TeamId { get; set; }

    public Team Team { get; set; } = null!;

    public int PositionId { get; set; }

    public bool IsInjured { get; set; }
}
