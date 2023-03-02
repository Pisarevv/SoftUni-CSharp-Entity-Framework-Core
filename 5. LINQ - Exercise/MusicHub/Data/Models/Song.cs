namespace MusicHub.Data.Models;

using System.ComponentModel.DataAnnotations;
using Common;
using Enums;

public class Song
{
    [Key]
    public int Id { get; set; }

    [MaxLength(ValidationConstants.MaxSongNameLength)]
    public string Name { get; set; } = null!;

    public TimeSpan Duration { get; set; }

    public DateTime CreatedOn { get; set; }

    public Genre Genre { get; set; }
    
    public int AlbumId { get; set; }

    //Todo: add Album 

    public int WriterId { get; set; }

    //Todo: add writer

    public decimal Price { get; set; }

    //Todo : add songperfores collection
}
