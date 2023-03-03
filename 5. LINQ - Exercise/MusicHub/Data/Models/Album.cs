namespace MusicHub.Data.Models;

using Common;
using System.ComponentModel.DataAnnotations;

public class Album
{
    public Album()
    {
       this.Songs = new HashSet<Song>(); 
    }

    [Key]
    public int Id { get; set; }

    [MaxLength(ValidationConstants.MaxAlbumNameLength)]
    public string Name { get; set; } = null!;

    public DateTime ReleaseDate { get; set; }

    public decimal Price => this.Songs.Sum(s => s.Price);

    public int? ProducerId { get; set; }

    public Producer Producer { get; set; }

    public ICollection<Song> Songs { get; set; }
}
