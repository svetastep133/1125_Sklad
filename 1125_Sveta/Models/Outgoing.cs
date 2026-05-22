using System;

namespace _1125_Sveta.Models;

public class Outgoing
{
    public int Id { get; set; }
    public string DocNumber { get; set; }
    public DateTime Date { get; set; } 
    public int SupplierId { get; set; }
    public int WarehouseId { get; set; }
   
}