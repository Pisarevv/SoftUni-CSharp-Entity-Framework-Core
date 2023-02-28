namespace P03_FootballBetting.Data.Models;

using System.ComponentModel.DataAnnotations;
using Common;

public class Team
{
    [Key]
    public int TeamId { get; set; }

    [MaxLength(ValidationConstants.MaxTeamNameLength)]
    public string Name { get; set; } = null!;

    [MaxLength(ValidationConstants.MaxLogoUrlLength)]
    public string? LogoUrl { get; set; }

    [MaxLength (ValidationConstants.MaxInitialsLength)]
    public string Initials { get; set; } = null!;

    public decimal Budget { get; set; }

    public int PrimaryKitColorId { get; set; }

    public int SecondaryKitColorId { get; set; }

    public int TownId { get; set; }

    //TODO : Add relations
}
