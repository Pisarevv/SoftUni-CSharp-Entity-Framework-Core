namespace P02_FootballBetting.Data.Models;

using System.ComponentModel.DataAnnotations;
using Common;

public class Position
{
    public Position()
    {
       this.Players = new HashSet<Player>(); 
    }

    [Key]
    public int PositionId { get; set; }

    [MaxLength(ValidationConstants.MaxPositionNameLength)]
    public string Name { get; set; } = null!;

    ICollection<Player> Players { get; set; }
}
