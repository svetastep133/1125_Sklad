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
            "SELECT s.Quantity, s.Last_updated, p.Name as pName, w.Name as wName, s.Warehouse_id, s.Product_id, c.Name  FROM Stock s INNER JOIN Products p on p.Id = s.Product_id INNER JOIN Warehouses w on w.Id = s.Warehouse_id inner join Categories c on c.Id = p.Category_id where s.Warehouse_id =" +
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
                        WarehouseId = dr.GetInt32("Warehouse_id"),
                        ProductId = dr.GetInt32("Product_id"),
                        Quantity = dr.GetInt32("Quantity"),
                        LastUpdated = dr.GetDateTime("Last_updated"),
                        WarehouseName = dr.GetString("wName"),
                        ProductsName = dr.GetString("pName"),
                        CategoryName = dr.GetString("Name")
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