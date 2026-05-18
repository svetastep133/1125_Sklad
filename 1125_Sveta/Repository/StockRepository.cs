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
            "SELECT s.Quantity, s.Last_updated, p.Name as pName, w.Name as wName, s.Warehouse_id, s.Product_id FROM Stock s INNER JOIN Products p on p.Id = s.Product_id INNER JOIN Warehouses w on w.Id = s.Warehouse_id where Warehouse_id =" +
            house.Id;
        /*List<Product> products = new List<Product>();
        List<IncomingItem> incomingItems = new List<IncomingItem>();
        List<Incoming> incomings = new List<Incoming>();
        List<Supplier> suppliers = new List<Supplier>();    
        List<Category> categories = new List<Category>();*/
        try 
            
        {
            connection.Open();
            using (var mc = new MySqlCommand(sql, connection))
            using (var dr = mc.ExecuteReader())
            {
                while (dr.Read())
                {
                    /*categories.Add(new Category
                    {
                        Name = dr.GetString("Name")
                    });
                    suppliers.Add(new Supplier
                    {
                        Name = dr.GetString("Name"),
                        Email = dr.GetString("Email")
                    });
                    incomings.Add(new Incoming
                    {
                        DocNumber = dr.GetString("Doc_number"),
                        SupplierId = dr.GetInt32("Supplier_id"),
                        WarehouseId = dr.GetInt32("Warehouse_id"),
                        Date = dr.GetDateTime("Date")
                    });
                    incomingItems.Add(new IncomingItem
                    {
                        IncomingId = dr.GetInt32("Incoming_id"),
                        ProductId = dr.GetInt32("Product_id"),
                        Quantity = dr.GetInt32("Quantity"),
                        Cost = dr.GetInt32("Cost")
                    });
                    products.Add(new Product
                    {
                        Name = dr.GetString("pName"),
                        Weight = dr.GetInt32("pWeight"),
                        CategoryId =  dr.GetInt32("pCategoryId")
                    });*/
                    stocks.Add(new Stock
                    {
                        WarehouseId = dr.GetInt32("Warehouse_id"),
                        ProductId = dr.GetInt32("Product_id"),
                        Quantity = dr.GetInt32("Quantity"),
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