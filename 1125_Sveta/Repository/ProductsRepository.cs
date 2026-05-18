using System;
using System.Collections.Generic;
using _1125_Sveta.Models;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace _1125_Sveta.Repository;

public class ProductsRepository
{
    MySqlConnection connection;

   public  ProductsRepository(IOptions<DataBaseConnection> connect)
    {
        connection = new MySqlConnection(connect.Value.ConnectionString);
    }

    public List<Product> GetProducts()
    {
        List<Product> products = new List<Product>();
        string sql = "select p.Id, p.Name, p.Weight, p.Category_id, c.Name as cName from Products p join Categories c on p.Category_id = c.Id ";
        try
        {
            connection.Open();
            using (var mc = new MySqlCommand(sql, connection))
            using (var dr = mc.ExecuteReader())
            {
                while (dr.Read())
                {
                    products.Add(new Product
                    {
                        Id = dr.GetInt32( "Id"),
                        Name = dr.GetString("Name"),
                        Weight = dr.GetDecimal( "Weight"),
                        CategoryId = dr.GetInt32( "Category_id"),
                        CategoryName = dr.GetString("cName")
                     });
                    
                }
            }
            
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return products;
    }

    public void AddProduct(Product product)
    {
        string sql="insert into Products values (0,@Name,@Weight,@Category_id)";
        try
        {
            connection.Open();
            using (var mc1 = new MySqlCommand(sql, connection))
            {
                mc1.Parameters.AddWithValue("Name", product.Name );
                mc1.Parameters.AddWithValue("Weight", product.Weight );
                mc1.Parameters.AddWithValue("Category_id", product.CategoryId);
                mc1.ExecuteNonQuery();
            }
            
            connection.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
          
        }
    }
    
    
}