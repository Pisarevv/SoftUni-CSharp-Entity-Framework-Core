namespace MusicHub.Data.Models;

using Common;
using System.ComponentModel.DataAnnotations;

public class Writer
{
    public Writer()
    {
        this.Songs = new HashSet<Song>();
    }

    [Key]
    public int Id { get; set; }

    [MaxLength(ValidationConstants.MaxWriterNameLength)]
    public string Name { get; set; } = null!;

    public string? Pseudonym { get; set; }

    public ICollection<Song> Songs { get; set; }
}
