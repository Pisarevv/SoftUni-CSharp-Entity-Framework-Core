using Eventmi.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Eventmi.Infrastructure.Data.Models
{

    public class Event
    {
        [Key]
        [Comment("Identification")]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxNameLength)]
        [Comment("Name of the event")]
        public string Name { get; set; } = null!;

        [Required]
        [Comment("Start date and hour")]
        public DateTime Start { get; set; }

        [Required]
        [Comment("End date and hour")]
        public DateTime End { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxPlaceLength)]
        [Comment("Location of the event")]
        public string Place { get; set; } = null!;
   
    }

}

