using System;
using System.Collections.Generic;
using _1125_Sveta.Models;
using _1125_Sveta.ViewModels;
using _1125_Sveta.Views;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace _1125_Sveta.Repository;

public class SuppliersRepository
{
    MySqlConnection connection;

    public SuppliersRepository(IOptions<DataBaseConnection> connect)
    {
        connection = new MySqlConnection(connect.Value.ConnectionString);
    }

    public List<Supplier> GetSuppliers()
    {
        List<Supplier> suppliers = new List<Supplier>();
        string sql = "SELECT * FROM Suppliers;";
        try
        {
            connection.Open();
            using (var cmd = new MySqlCommand(sql, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    suppliers.Add(new Supplier
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

        return suppliers;
    }
    
    public void AddSupplier(Supplier supplier)
    {
        string sql="insert into Suppliers values (0,@Name,@Email)";
        try
        {
            connection.Open();
            using (var mc1 = new MySqlCommand(sql, connection))
            {
                mc1.Parameters.AddWithValue("Name", supplier.Name );
                mc1.Parameters.AddWithValue("Email", supplier.Email);
                mc1.ExecuteNonQuery();
            }
            
            connection.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
          
        }
    }
    public bool DeleteSupplier(int id)
    {
        string checkSql = "SELECT COUNT(*) FROM Incoming WHERE Supplier_id = @Supplier_id";
        string sql = "DELETE FROM `Suppliers` where Id=@Id";
        try
        {
            connection.Open();
            
            using (var checkCmd = new MySqlCommand(checkSql, connection))
            {
                checkCmd.Parameters.AddWithValue("@Supplier_id", id);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    throw new Exception("Невозможно удалить поставщика.");
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
    
    public bool Update(Supplier supplier)
    {
        
        string sql="update  `Suppliers` set `Name` =@Name, `Email`=@Email where `Id` = " + supplier.Id;
        try
        {
            connection.Open();
            using (var mc1 = new MySqlCommand(sql, connection))
            {
                mc1.Parameters.AddWithValue("@Name", supplier.Name );
                mc1.Parameters.AddWithValue("@Email", supplier.Email);
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