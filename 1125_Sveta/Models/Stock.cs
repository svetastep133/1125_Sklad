using System;

namespace _1125_Sveta.Models;

public class Stock
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int Reserved {get; set;}
    public DateTime LastUpdated { get; set; }
    public string WarehouseName { get; set; }
    public string ProductsName { get; set; }
}