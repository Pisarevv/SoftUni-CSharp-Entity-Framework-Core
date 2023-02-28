namespace P03_FootballBetting.Data.Models;

using Common;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int UserId { get; set; }

    [MaxLength(ValidationConstants.MaxUsernameLength)]
    public string UserName { get; set; } = null!;

    [MaxLength(ValidationConstants.MaxPasswordLength)]
    public string Password { get; set; } = null!;

    [MaxLength(ValidationConstants.MaxEmailLength)]
    public string Email { get; set; } = null!;

    public decimal Balance { get; set; }
}
