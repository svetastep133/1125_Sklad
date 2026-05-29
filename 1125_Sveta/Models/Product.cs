using System;

namespace _1125_Sveta.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Weight{ get; set; }
    public int CategoryId{ get; set; }
    public string CategoryName {get; set;}
    
    public int Cost { get; set; }
    public int Quantity { get; set; }
    public int OutQuantity {get; set;}
    public string DocNumber { get; set; }
    public DateTime Date { get; set; }
    public string SupplierName { get; set; }
    public string Email { get; set; }
}