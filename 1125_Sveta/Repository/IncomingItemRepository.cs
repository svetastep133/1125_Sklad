using System;
using System.Collections.Generic;
using _1125_Sveta.Models;
using _1125_Sveta.ViewModels;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace _1125_Sveta.Repository;

public class IncomingItemRepository
{
    MySqlConnection connection;
    public  IncomingItemRepository(IOptions<DataBaseConnection> connect)
    {
        connection = new MySqlConnection(connect.Value.ConnectionString);
    }

    public List<IncomingItem> GetIncomingItems()
    {
        List <IncomingItem> items = new List<IncomingItem>();
        string sql = "SELECT i.Id, i.Incoming_id, i.Product_id, i.Cost, i.Quantity, p.Name as pName, n.Doc_number from Incoming_items i inner join Products p on i.Product_id = p.Id inner join Incoming n on i.Incoming_id = n.Id";
        try
        {
            connection.Open();
            using (var mc = new MySqlCommand(sql, connection))
            using (var dr = mc.ExecuteReader())
            {
                while (dr.Read())
                {
                    items.Add(new IncomingItem
                    {
                        Id = dr.GetInt32( "Id"),
                        IncomingId = dr.GetInt32( "Incoming_id"),
                        ProductId = dr.GetInt32( "Product_id"),
                        Cost = dr.GetInt32( "Cost"),
                        Quantity = dr.GetInt32( "Quantity"),
                        DocNumber = dr.GetInt32( "Doc_number"),
                        ProductName = dr.GetString("Name"),
                    });
                }
            }
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return items;
    }
   
}