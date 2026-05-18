using System;
using System.Collections.Generic;
using _1125_Sveta.Models;
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
}