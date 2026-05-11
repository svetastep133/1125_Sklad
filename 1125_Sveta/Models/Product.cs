namespace _1125_Sveta.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public decimal Weight{ get; set; }
    public int Category_id{ get; set; }
    public string CategoriesName {get; set;}
    
}