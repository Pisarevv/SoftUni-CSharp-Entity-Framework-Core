﻿namespace P02_FootballBetting.Data.Models;

using Common;
using System.ComponentModel.DataAnnotations;

public class User
{
    public User()
    {
        this.Bets = new HashSet<Bet>();
    }

    [Key]
    public int UserId { get; set; }

    [MaxLength(ValidationConstants.MaxUsernameLength)]
    public string Username { get; set; } = null!;

    [MaxLength(ValidationConstants.MaxPasswordLength)]
    public string Password { get; set; } = null!;

    [MaxLength(ValidationConstants.MaxEmailLength)]
    public string Email { get; set; } = null!;

    [MaxLength(ValidationConstants.MaxUserNameLength)]
    public string Name { get; set; }

    public decimal Balance { get; set; }

    public virtual ICollection<Bet> Bets { get; set; }
}
