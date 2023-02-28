namespace P01_StudentSystem.Data;

using Microsoft.EntityFrameworkCore;

using Common;
using P01_StudentSystem.Data.Models;

public class StudentSystemContext : DbContext
{
    public StudentSystemContext()
    {

    }

    public StudentSystemContext (DbContextOptions<StudentSystemContext> options)
        :base(options) 
    {

    }

    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Homework> Homeworks { get; set; } = null!;
    public DbSet<StudentCourse> StudentsCourses { get; set; } = null!;
    public DbSet<Resource> Resources { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(DbConfig.ConnectionString);
        }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(p => p.PhoneNumber)
            .IsUnicode(false);
        });

        modelBuilder.Entity<Resource>(entity =>
        {
            entity.Property(p => p.ResourceId)
            .IsUnicode(false);
        });

        modelBuilder.Entity<StudentCourse>(entity =>
        {
            entity.HasKey(sc => new { sc.StudentId, sc.CourseId });
        });
    }
}
