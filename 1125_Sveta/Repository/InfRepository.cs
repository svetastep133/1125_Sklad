using System;
using System.Collections.Generic;
using _1125_Sveta.Models;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace _1125_Sveta.Repository;

public class InfRepository
{
    MySqlConnection connection;
    public  InfRepository(IOptions<DataBaseConnection> connect)
    {
        connection = new MySqlConnection(connect.Value.ConnectionString);
    }

    public List<Product> GetInfInc(int productId)
    {
        List<Product> products = new List<Product>();
        string sql =
            "select p.Id, p.Name, p.Weight, s.Name as sName, ii.Cost, ii.Quantity, i.Doc_number, i.Date, s.Email  from Products p inner join Incoming_items ii on ii.Product_id = p.Id inner join Incoming i on i.Id = ii.Incoming_id inner join Suppliers s on s.Id = i.Supplier_id where p.Id = " + productId;

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
                        Id = dr.GetInt32("Id"),
                        Name = dr.GetString("Name"),
                        Weight = dr.GetDecimal("Weight"),
                        Cost = dr.GetInt32("Cost"),
                        Quantity = dr.GetInt32("Quantity"),
                        DocNumber = dr.GetString("Doc_number"),
                        Date = dr.GetDateTime("Date"),
                        SupplierName = dr.GetString("sName"),
                        Email = dr.GetString("Email"),

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

    public List<Product> GetInfQua(int productId)
    {
        List<Product> products = new List<Product>();
        string sql =
            "select oi.Quantity from Products p inner join Outgoing_items oi on oi.Product_id = p.Id inner join Outgoing o on o.Id = oi.Outgoing_id  inner join Suppliers s on s.Id = o.Supplier_id where p.Id ="+productId;;
        try
        {
            connection.Open();
            using (var mc = new MySqlCommand(sql, connection))
                using (var dr = mc.ExecuteReader())
                    while (dr.Read())
                    {
                        products.Add(new Product
                        {
                            OutQuantity = dr.GetInt32("Quantity"),
                        });
                    }
            
            connection.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
           
        }
        return products;
    }
    
}