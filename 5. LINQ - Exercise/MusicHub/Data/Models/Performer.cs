namespace MusicHub.Data.Models;

using Common;
using System.ComponentModel.DataAnnotations;

public class Performer
{
    [Key]
    public int Id { get; set; }

    [MaxLength(ValidationConstants.MaxPerformerFirstNameLength)]
    public string FirstName { get; set; } = null!;

    [MaxLength(ValidationConstants.MaxPerformerLastNameLength)]
    public string LastName { get; set; } = null!;

    public int Age { get; set; }

    public decimal NetWorth { get; set; }

    //Todo: add perforersongs collection
}
