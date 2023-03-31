using Artillery.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Artillery.Data.Models;

public class Country
{
    public Country()
    {
        this.CountriesGuns = new HashSet<CountryGun>();      
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(ValidationConstants.MaxCountryNameLength)]
    public string CountryName { get; set; } = null!;

    [Required]
    public int ArmySize { get; set; }

    public ICollection<CountryGun> CountriesGuns  { get; set; }


}
