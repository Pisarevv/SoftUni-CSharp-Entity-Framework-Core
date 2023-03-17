using Newtonsoft.Json;

namespace CarDealer.DTOs.Export;

public class ExportSaleWithDiscountDto
{
    [JsonProperty("car")]
    public ExportCarInfoDto Car { get; set; }

    [JsonProperty("customerName")]
    public string CustomerName { get; set; }

    [JsonProperty("discount")]
    public string Discount { get; set; }

    [JsonProperty("price")]
    public string Price { get; set; }

    [JsonProperty("priceWithDiscount")]
    public string PriceWithDiscount { get; set; }

}
