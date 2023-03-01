namespace P02_FootballBetting.Data.Models;

using P02_FootballBetting.Common;
using System.ComponentModel.DataAnnotations;

public class Country
{
    public Country()
    {
       this.Towns = new HashSet<Town>(); 
    }

    [Key]
    public int CountryId { get; set; }

    [MaxLength(ValidationConstants.MaxCountryNameLength)]
    public string Name { get; set; } = null!;

    public ICollection<Town> Towns { get; set; } = null!;
}
