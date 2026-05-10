using System;
using System.Collections.Generic;
using _1125_Sveta.Models;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace _1125_Sveta.Repository;

public class CategoryRepository
{
    MySqlConnection connection;
    public  CategoryRepository(IOptions<DataBaseConnection> connect)
    {
        connection = new MySqlConnection(connect.Value.ConnectionString);
    }

    public List<Category> GetCategories()
    {
        List<Category> categories = new List<Category>();
        string sql = "SELECT * FROM Categories";
        try
        {
            connection.Open();
            using(var cmd = new MySqlCommand(sql, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    categories.Add(new Category
                    {
                        Id = reader.GetInt32("Id"),
                        Name = reader.GetString("Name"),

                    });
                }
            }
            
            
            connection.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
        return categories;
        
        
    }
    
    
    
}