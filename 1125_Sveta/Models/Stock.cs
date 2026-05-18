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
    /*public Product Product { get; set; }
    public IncomingItem IncomingItem { get; set; }
    public Incoming Incoming { get; set; }
    public Supplier Supplier { get; set; }
    public Category Category { get; set; }*/
    
    
}