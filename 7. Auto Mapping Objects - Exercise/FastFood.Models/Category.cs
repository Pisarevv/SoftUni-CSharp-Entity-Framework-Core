namespace FastFood.Models
{
    using FastFood.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public Category()
        {
            this.Items = new HashSet<Item>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.CategoryNameMaxLenghth, MinimumLength = 3)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Item> Items { get; set; } 
    }
}
