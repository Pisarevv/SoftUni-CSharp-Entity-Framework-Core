﻿namespace Footballers.Data.Models;

using Footballers.Data.Common;
using System.ComponentModel.DataAnnotations;

public class Coach
{
    public Coach()
    {
        this.Footballers = new HashSet<Footballer>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(ValidationConstants.MaxCoachNameLength)]
    public string Name { get; set; } = null!;

    [Required]
    public string Nationality { get; set; } = null!;

    public ICollection<Footballer> Footballers { get; set;}


}
