namespace FastFood.Core.ViewModels.Orders
{
    using System.Collections.Generic;

    public class CreateOrderViewModel
    {
        public CreateOrderViewModel()
        {
            this.Employees = new List<int>();
            this.Items = new List<int>();
        }
        public List<int> Items { get; set; } = null!;

        public List<int> Employees { get; set; } = null!;
    }
}
