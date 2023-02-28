﻿namespace P01_StudentSystem.Data.Models;
using System.ComponentModel.DataAnnotations;
using Common;

public class Student
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(ValidationConstants.StudentNameMaxLenght)]
    public string Name { get; set; } = null!;

    [StringLength(10)]
    public string? PhoneNumber { get; set; }

    public DateTime RegisteredOn { get; set; }

    public DateTime? BirthDay { get; set; } 
}
