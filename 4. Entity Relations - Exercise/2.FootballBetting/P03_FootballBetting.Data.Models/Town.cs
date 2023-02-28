using P03_FootballBetting.Common;
using System.ComponentModel.DataAnnotations;

namespace P03_FootballBetting.Data.Models;


public class Town
{
    [Key]
    public int TownId { get; set; }

    [MaxLength(ValidationConstants.MaxTownNameLength)]
    public string Name { get; set; } = null!;

    public int CountryId { get; set; }
}
