namespace Footballers.Data.Models;

using Footballers.Data.Common;
using Footballers.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Footballer
{
    public Footballer()
    {
        this.TeamsFootballers = new HashSet<TeamFootballer>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(ValidationConstants.MaxFootballerNameLength)]
    public string Name { get; set; } = null!;

    [Required]
    public DateTime ContractStartDate { get; set; }

    [Required]
    public DateTime ContractEndDate { get; set; }

    [Required] 
    public PositionType PositionType { get; set; }

    [Required]
    public BestSkillType BestSkillType { get; set; }

    [Required]
    [ForeignKey(nameof(Coach))]
    public int CoachId { get; set; }

    public Coach Coach { get; set; } = null!;
    
    public ICollection<TeamFootballer> TeamsFootballers { get; set; }


}
