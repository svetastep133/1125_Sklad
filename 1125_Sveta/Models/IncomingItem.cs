namespace _1125_Sveta.Models;

public class IncomingItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int Cost { get; set; }
    public int ProductId { get; set; }
    public int IncomingId  { get; set; }
    public int DocNumber { get; set; }
    public string ProductName { get; set; }
}