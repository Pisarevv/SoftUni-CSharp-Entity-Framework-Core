using Artillery.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Artillery.Data.Models;

public class Manufacturer
{
    public Manufacturer()
    {
        this.Guns = new HashSet<Gun>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(ValidationConstants.MaxManufacturerNameLength)]
    public string ManufacturerName { get; set; } = null!;

    [Required]
    [MaxLength(ValidationConstants.MaxFoundedTextLength)]
    public string Founded { get; set; } = null!;

    public ICollection<Gun> Guns { get; set; }
 }
