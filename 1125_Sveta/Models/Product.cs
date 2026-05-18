namespace _1125_Sveta.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Weight{ get; set; }
    public int CategoryId{ get; set; }
    public string CategoryName {get; set;}
}