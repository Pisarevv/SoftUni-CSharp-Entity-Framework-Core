namespace FastFood.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class OrderItem
    {
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }

        [Required]
        public Order Order { get; set; } = null!;

        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }

        [Required]
        public Item Item { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}