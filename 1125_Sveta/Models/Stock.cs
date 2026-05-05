using System;

namespace _1125_Sveta.Models;

public class Stock
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int Reserved {get; set;}
    public DateTime Last_updated { get; set; }
}