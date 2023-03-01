using P03_FootballBetting.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_FootballBetting.Data.Models;


public class Town
{
    public Town()
    {
      this.Teams = new HashSet<Team>();   
    }

    [Key]
    public int TownId { get; set; }

    [MaxLength(ValidationConstants.MaxTownNameLength)]
    public string Name { get; set; } = null!;

    [ForeignKey(nameof(Country))]
    public int CountryId { get; set; }

    public Country Country { get; set; } = null!;

    public ICollection<Team> Teams { get; set; } = null!;
}
