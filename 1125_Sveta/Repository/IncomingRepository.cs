using System;
using System.Collections.Generic;
using _1125_Sveta.Models;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace _1125_Sveta.Repository;

public class IncomingRepository
{
    MySqlConnection connection;
    public  IncomingRepository(IOptions<DataBaseConnection> connect)
    {
        connection = new MySqlConnection(connect.Value.ConnectionString);
    }

    public List<Incoming> GetIncoming()
    {
        List<Incoming> incoming = new List<Incoming>();
        string sql = "SELECT i.Id, i.Doc_number, i.Date,  i.Supplier_id, i.Warehouse_id, w.Name,s.Name as sName, s.Id as sId from Incoming i inner join Suppliers s on i.Supplier_id = s.Id inner join Warehouses w on i.Warehouse_id = w.Id ";
        try
        {
            connection.Open();
            using (var mc = new MySqlCommand(sql, connection))
            using (var dr = mc.ExecuteReader())
            {
                while (dr.Read())
                {
                    incoming.Add(new Incoming
                    {
                        Id = dr.GetInt32( "Id"),
                        DocNumber = dr.GetString("Doc_number"),
                        Date = dr.GetDateTime( "Date"),
                        SupplierName = dr.GetString("sName"),
                        SupplierId = dr.GetInt32( "Supplier_id"),
                        WarehouseName = dr.GetString("Name"),
                        WarehouseId = dr.GetInt32( "Warehouse_id")
                    });
                }
            }
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
        return incoming;
    }
    
}