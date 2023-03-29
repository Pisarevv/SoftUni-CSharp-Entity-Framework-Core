using System.ComponentModel.DataAnnotations;
using Trucks.Data.Common;

namespace Trucks.Data.Models;

public class Client
{
    public Client()
    {
        this.ClientsTrucks = new HashSet<ClientTruck>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(ValidationConstants.MaxClientNameLength)]
    public string Name { get; set; }

    [Required]
    [MaxLength(ValidationConstants.MaxClientNationalityLength)]
    public string Nationality { get; set; }

    [Required]
    public string Type { get; set; }

    public ICollection<ClientTruck> ClientsTrucks { get; set;}
}
