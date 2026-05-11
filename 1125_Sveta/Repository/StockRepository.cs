using System;
using System.Collections.Generic;
using _1125_Sveta.Models;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace _1125_Sveta.Repository;

public class StockRepository
{
    MySqlConnection connection;

    public StockRepository(IOptions<DataBaseConnection> connect)
    {
        connection = new MySqlConnection(connect.Value.ConnectionString);
    }

    public List<Stock> GetStocks(Warehouse house)
    {
        List<Stock> stocks = new List<Stock>();
        string sql =
            "SELECT s.Id, s.Quantity, s.Reserved, s.Last_updated, p.Name as pName, w.Name as wName, s.Warehouse_id, s.Product_id FROM Stock s INNER JOIN Products p on p.Id = s.Product_id INNER JOIN Warehouses w on w.Id = s.Warehouse_id where Warehouse_id =" +
            house.Id;
        try
        {
            connection.Open();
            using (var mc = new MySqlCommand(sql, connection))
            using (var dr = mc.ExecuteReader())
            {
                while (dr.Read())
                {
                    stocks.Add(new Stock
                    {
                        Id = dr.GetInt32("Id"),
                        Quantity = dr.GetInt32("Quantity"),
                        Reserved = dr.GetInt32("Reserved"),
                        LastUpdated = dr.GetDateTime("Last_updated"),
                        WarehouseName = dr.GetString("wName"),
                        ProductsName = dr.GetString("pName")
                    });
                }
            }

            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return stocks;
    }
}