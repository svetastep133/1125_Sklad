using System;

namespace _1125_Sveta.Models;

public class Incoming
{
    public int Id { get; set; }
    public string DocNumber { get; set; }
    public DateTime Date { get; set; } 
    public string Status { get; set; }
    public string SupplierName { get; set; }
    public string WarehouseName { get; set; }
}