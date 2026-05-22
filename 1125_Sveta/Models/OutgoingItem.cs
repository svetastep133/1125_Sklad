namespace _1125_Sveta.Models;

public class OutgoingItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int Cost { get; set; }
    public int ProductId { get; set; }
    public int OutgoingId  { get; set; }
    public int DocNumber { get; set; }
    public string ProductName { get; set; }
}