using System;
using System.Collections.Generic;
using _1125_Sveta.Models;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace _1125_Sveta.Repository;

public class WareHouseRepository
{
    MySqlConnection connection;

    public  WareHouseRepository(IOptions<DataBaseConnection> connect)
    {
        connection = new MySqlConnection(connect.Value.ConnectionString);
    }

    public List<Warehouse> GetWarehouses()
    {
        List<Warehouse> warehouses = new List<Warehouse>();
        string sql = "SELECT * FROM Warehouses";
        try
        {
            connection.Open();
            using (var mc = new MySqlCommand(sql, connection))
            using (var dr = mc.ExecuteReader())
            {
                while (dr.Read())
                {
                    warehouses.Add(new Warehouse
                    {
                        Id = dr.GetInt32( "Id"),
                        Name = dr.GetString("Name"),
                        Location = dr.GetString("Location"),
                    });
                }
            }
            

            connection.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
          
        }
        
        
        return warehouses;
    }
    
    
    
    
}