using System.Text.Json.Serialization;

namespace CarDealer.DTOs.Export;

public class ExportCustomerSaleDto
{
    public string FullName { get; set; } 

    public int BoughtCars { get; set; }

    public decimal SpentMoney { get; set; }

  
}
