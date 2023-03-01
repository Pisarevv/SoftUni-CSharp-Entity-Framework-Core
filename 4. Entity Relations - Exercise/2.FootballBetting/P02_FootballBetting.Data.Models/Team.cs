namespace P02_FootballBetting.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common;

public class Team
{
    public Team()
    {
        this.HomeGames = new HashSet<Game>();
        this.AwayGames = new HashSet<Game>();

        this.Players = new HashSet<Player>();
    }

    [Key]
    public int TeamId { get; set; }

    [MaxLength(ValidationConstants.MaxTeamNameLength)]
    public string Name { get; set; } = null!;

    [MaxLength(ValidationConstants.MaxLogoUrlLength)]
    public string? LogoUrl { get; set; }

    [MaxLength (ValidationConstants.MaxInitialsLength)]
    public string Initials { get; set; } = null!;

    public decimal Budget { get; set; }

    [ForeignKey(nameof(PrimaryKitColor))]
    public int PrimaryKitColorId { get; set; }

    public Color PrimaryKitColor { get; set; } = null!;

    [ForeignKey(nameof(SecondaryKitColor))]
    public int SecondaryKitColorId { get; set; }

    public Color SecondaryKitColor { get; set; } = null!;

    [ForeignKey(nameof(Town))]
    public int TownId { get; set; }

    public Town Town { get; set; } = null!;

    [InverseProperty(nameof(Game.HomeTeam))]
    public virtual ICollection<Game> HomeGames { get; set; } = null!;

    [InverseProperty(nameof(Game.AwayTeam))]
    public virtual ICollection<Game> AwayGames { get; set; } = null!;

    public virtual ICollection<Player> Players { get; set; } = null!;
}
