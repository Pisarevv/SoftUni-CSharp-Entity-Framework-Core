namespace Trucks.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trucks.Data.Common;
using Trucks.Data.Models.Enums;

public class Truck
{
    public Truck()
    {
        this.ClientsTrucks = new HashSet<ClientTruck>();
    }

    [Key]
    public int Id { get; set; }

    [MaxLength(ValidationConstants.MaxRegistrationNumberLength)]
    public string RegistrationNumber { get; set; }

    [Required]
    [MaxLength(ValidationConstants.MaxVinNumberLength)]
    public string VinNumber { get; set; } = null!;

    public int TankCapacity { get; set; }

    public int CargoCapacity { get; set; }

    [Required]
    public CategoryType CategoryType { get; set; }

    [Required]
    public MakeType MakeType { get; set; }

    [Required]
    [ForeignKey(nameof(Despatcher))]
    public int DespatcherId { get; set; }

    public Despatcher Despatcher { get; set; }

    public ICollection<ClientTruck> ClientsTrucks { get; set; }
}
