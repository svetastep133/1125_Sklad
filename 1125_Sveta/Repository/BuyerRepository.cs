using System;
using System.Collections.Generic;
using _1125_Sveta.Models;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace _1125_Sveta.Repository;

public class BuyerRepository
{
    MySqlConnection connection;
    public BuyerRepository(IOptions<DataBaseConnection> connect)
    {
        connection = new MySqlConnection(connect.Value.ConnectionString);
    }

    public List<Buyer> GetBuyers()
    {
        List<Buyer> buyer = new List<Buyer>();
        string sql = "SELECT * FROM Buyer;";
        try
        {
            connection.Open();
            using (var cmd = new MySqlCommand(sql, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    buyer.Add(new Buyer
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

        return buyer;
    }
    public void AddBuyer(Buyer buyer)
    {
        string sql="insert into Buyer values (0,@Name)";
        try
        {
            connection.Open();
            using (var mc1 = new MySqlCommand(sql, connection))
            {
                mc1.Parameters.AddWithValue("Name", buyer.Name );
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