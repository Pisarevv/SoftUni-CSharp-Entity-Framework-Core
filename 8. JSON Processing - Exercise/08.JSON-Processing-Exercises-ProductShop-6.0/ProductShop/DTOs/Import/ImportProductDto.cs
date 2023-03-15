﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductShop.DTOs.Import;
public class ImportProductDto
{
    [JsonProperty("Name")]
    public string Name { get; set; }

    [JsonProperty("Price")]
    public decimal Price { get; set; }

    [JsonProperty("BuyerId")]
    public int? BuyerId { get; set; }

    [JsonProperty("SellerId")]
    public int SellerId { get; set; }
}
