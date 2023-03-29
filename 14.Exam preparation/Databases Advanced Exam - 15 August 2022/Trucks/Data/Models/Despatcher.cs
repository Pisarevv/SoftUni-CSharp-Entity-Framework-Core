using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trucks.Data.Common;

namespace Trucks.Data.Models;
public class Despatcher
{
    public Despatcher()
    {
        this.Trucks = new HashSet<Truck>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(ValidationConstants.MaxDispatcherNameLength)]
    public string Name { get; set; } = null!;

    public string Position { get; set; }

    public ICollection<Truck> Trucks { get; set; }

}
