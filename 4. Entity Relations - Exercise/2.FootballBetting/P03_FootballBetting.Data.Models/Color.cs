namespace P03_FootballBetting.Data.Models;

using Common;
using System.ComponentModel.DataAnnotations;

public class Color
{
    public Color()
    {
        this.PrimaryKitTeams = new HashSet<Team>();
        this.SecondaryKitTeams = new HashSet<Team>();
    }
    [Key]
    public int ColorId { get; set; }

    [MaxLength(ValidationConstants.MaxColorNameLength)]
    public string Name { get; set; } = null!;

    public ICollection<Team> PrimaryKitTeams { get; set; }

    public ICollection<Team> SecondaryKitTeams { get; set; }
}
