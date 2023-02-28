namespace P01_StudentSystem.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common;

public class Student
{
    public Student()
    {
        this.StudentsCourses = new HashSet<StudentCourse>();
    }

    [Key]
    public int StudentId { get; set; }

    [Required]
    [MaxLength(ValidationConstants.StudentNameMaxLength)]
    public string Name { get; set; } = null!;

    [StringLength(10)]
    public string? PhoneNumber { get; set; }

    public DateTime RegisteredOn { get; set; }

    public DateTime? Birthday { get; set; }

    public virtual ICollection<StudentCourse> StudentsCourses { get; set; } = null!;

    public virtual ICollection<Homework> Homeworks { get; set; } = null!;
}
