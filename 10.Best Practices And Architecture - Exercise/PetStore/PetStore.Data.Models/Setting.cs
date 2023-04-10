namespace PetStore.Data.Models
{
    using PetStore.Data.Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class Setting : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Value { get; set; } = null!;
    }
}
