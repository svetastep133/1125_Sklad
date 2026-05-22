using System;

namespace _1125_Sveta.Models;

public class Stock
{
    public int Quantity { get; set; }
    public DateTime LastUpdated { get; set; }
    public string WarehouseName { get; set; }
    public string ProductsName { get; set; }
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
     public string CategoryName { get; set; }
    
    
}