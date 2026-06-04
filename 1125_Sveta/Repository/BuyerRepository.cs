using System;
using System.Collections.Generic;
using _1125_Sveta.Models;
using _1125_Sveta.ViewModels;
using _1125_Sveta.Views;
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
                        Email = reader.GetString("Email"),
                       
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
        string sql="insert into Buyer values (0,@Name,@Email)";
        try
        {
            connection.Open();
            using (var mc1 = new MySqlCommand(sql, connection))
            {
                mc1.Parameters.AddWithValue("Name", buyer.Name );
                mc1.Parameters.AddWithValue("Email", buyer.Email);
                mc1.ExecuteNonQuery();
            }
            
            connection.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
          
        }
    }
    
    
    public bool DeleteBuyer(int id)
    {
        string checkSql = "SELECT COUNT(*) FROM Outgoing WHERE Buyer_id = @Buyer_id";
        string sql = "DELETE FROM `Buyer` where Id=@Id";
        try
        {
            connection.Open();
            
            using (var checkCmd = new MySqlCommand(checkSql, connection))
            {
                checkCmd.Parameters.AddWithValue("@Buyer_id", id);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    throw new Exception("Невозможно удалить.");
                }
            }
            using (var cmd = new MySqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            connection.Close();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            MessageBoxWindow messageBox = new MessageBoxWindow(new MessageBoxViewModel(ex.Message));

            messageBox.Show();
            connection.Close();
        }
        
        return false;
        

    }
    public bool Update(Buyer buyer)
    {
        
        string sql="update  `Buyer` set `Name` =@Name, `Email`=@Email where `Id` = " + buyer.Id;
        try
        {
            connection.Open();
            using (var mc1 = new MySqlCommand(sql, connection))
            {
                mc1.Parameters.AddWithValue("@Name", buyer.Name );
                mc1.Parameters.AddWithValue("@Email", buyer.Email);
                mc1.ExecuteNonQuery();
            }
            
            connection.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
          
        }

        return true;

    }

}