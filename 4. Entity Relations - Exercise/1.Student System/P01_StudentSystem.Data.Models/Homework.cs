using P01_StudentSystem.Common;
using P01_StudentSystem.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace P01_StudentSystem.Data.Models;

public class Homework
{
    [Key]
    public int HomeworkId { get; set; }

    [MaxLength(ValidationConstants.HomeworkContentMaxLenghth)]
    public string Content { get; set; } = null!;

    public ContentType ContentType { get; set; }

    public DateTime SubmissionTime { get; set; }

    public int StudentId { get; set; }

    public int CourseId { get; set; }
}
