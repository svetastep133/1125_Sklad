using System;
using _1125_Sveta.Models;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace _1125_Sveta.Repository;

public class NewProductRepository
{
    MySqlConnection connection;
    public  NewProductRepository(IOptions<DataBaseConnection> connect)
    {
        connection = new MySqlConnection(connect.Value.ConnectionString);
    }
     public void SaveProduct(Product product,Incoming incoming, IncomingItem incomingItem, Stock stock, Warehouse warehouse, Supplier supplier )
    {
       // string sql1="insert into Products  values(0, @Name,@Weight,@Category_id)";
        string sql2 = "insert into Incoming values (0, @Doc_number, @Supplier_id, @Warehouse_id, @Date)";
        string sql3 = "insert into Incoming_items values (0, @Incoming_id, @Product_id, @Cost, @Quantity)";
        string sql4 ="insert INTO  Stock Values (@Product_id, @Warehouse_id, @Quantity, CURRENT_TIMESTAMP()) on DUPLICATE KEY UPDATE `Quantity` = `Quantity`+ @Quantity, Last_updated = CURRENT_TIMESTAMP()";
        MySqlTransaction transaction = null;
        try
        {
            connection.Open();
            transaction = connection.BeginTransaction();
            using (var mc2 = new MySqlCommand(sql2, connection, transaction))
            {
                mc2.Parameters.AddWithValue("Doc_number", incoming.DocNumber);
                mc2.Parameters.AddWithValue("Supplier_id", incoming.SupplierId);
                mc2.Parameters.AddWithValue("Warehouse_id", incoming.WarehouseId);
                mc2.Parameters.AddWithValue("Date", incoming.Date);
                mc2.ExecuteNonQuery();
            }

            using (var mc2 = new MySqlCommand("SELECT LAST_INSERT_ID();", connection, transaction))
                incoming.Id = Convert.ToInt32(mc2.ExecuteScalar());
            incomingItem.IncomingId = incoming.Id;
            incomingItem.ProductId = product.Id;
            stock.ProductId = product.Id;
            stock.WarehouseId = warehouse.Id;
            stock.LastUpdated = DateTime.Now;
            stock.Quantity = incomingItem.Quantity;

            using (var mc3 = new MySqlCommand(sql3, connection, transaction))
            {
                mc3.Parameters.AddWithValue("Incoming_id", incomingItem.IncomingId);
                mc3.Parameters.AddWithValue("Product_id", incomingItem.ProductId);
                mc3.Parameters.AddWithValue("Cost", incomingItem.Cost);
                mc3.Parameters.AddWithValue("Quantity", incomingItem.Quantity);
                mc3.ExecuteNonQuery();
            }

            using (var mc4 = new MySqlCommand(sql4, connection, transaction))
            {
                mc4.Parameters.AddWithValue("Product_id", stock.ProductId);
                mc4.Parameters.AddWithValue("Warehouse_id", stock.WarehouseId);
                mc4.Parameters.AddWithValue("Quantity", stock.Quantity);
                mc4.ExecuteNonQuery();
            }
            transaction.Commit();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            transaction?.Rollback();

        }
        finally
        {
            connection.Close();
        }
    }
    
}