using P03_FootballBetting.Common;
using System.ComponentModel.DataAnnotations;

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

    public int CountryId { get; set; }

    public ICollection<Team> Teams { get; set; } = null!;
}
