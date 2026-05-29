using System;
using _1125_Sveta.Models;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace _1125_Sveta.Repository;

public class OutProductRepository
{
    MySqlConnection connection;
    public  OutProductRepository(IOptions<DataBaseConnection> connect)
    {
        connection = new MySqlConnection(connect.Value.ConnectionString);
    }
     public void SaveProduct(Outgoing outgoing, OutgoingItem outgoingItem, Stock stock )
    {
        // добавить запрос на проверку наличия указанного товара в указанном количестве на указанном складе       
            // если FALSE , то прерываемся, пишем ошибку - не хватает товара
            
        string sql2 = "insert into Outgoing values (0, @Doc_number, @Warehouse_id, @Date,@Buyer_id)";
        string sql3 = "insert into Outgoing_items (`id`, `Outgoing_id`, `Product_id`, `Cost`, `Quantity`) values (0, @Outgoing_id, @Product_id, @Cost, @Quantity)";
        string sql4 ="insert INTO  Stock Values (@Product_id, @Warehouse_id, @Quantity, CURRENT_TIMESTAMP()) on DUPLICATE KEY UPDATE `Quantity` = `Quantity`- @Quantity, Last_updated = CURRENT_TIMESTAMP()";
        
        MySqlTransaction transaction = null;
        try
        {
            connection.Open();
            transaction = connection.BeginTransaction();
            using (var mc2 = new MySqlCommand(sql2, connection, transaction))
            {
                mc2.Parameters.AddWithValue("Doc_number", outgoing.DocNumber);
                mc2.Parameters.AddWithValue("Buyer_id", outgoing.BuyerId);
                mc2.Parameters.AddWithValue("Warehouse_id", outgoing.WarehouseId);
                mc2.Parameters.AddWithValue("Date", outgoing.Date);
                mc2.ExecuteNonQuery();
            }

            using (var mc2 = new MySqlCommand("SELECT LAST_INSERT_ID();", connection, transaction)) 
                outgoing.Id = Convert.ToInt32(mc2.ExecuteScalar());
            
            outgoingItem.OutgoingId = outgoing.Id;
            outgoingItem.ProductId = stock.ProductId;

            using (var mc3 = new MySqlCommand(sql3, connection, transaction))
            {
                mc3.Parameters.AddWithValue("Outgoing_id", outgoingItem.OutgoingId);
                mc3.Parameters.AddWithValue("Product_id", outgoingItem.ProductId);
                mc3.Parameters.AddWithValue("Cost", outgoingItem.Cost);
                mc3.Parameters.AddWithValue("Quantity", outgoingItem.Quantity);
                mc3.ExecuteNonQuery();
            }

            using (var mc4 = new MySqlCommand(sql4, connection, transaction))
            {
                mc4.Parameters.AddWithValue("Product_id", stock.ProductId);
                mc4.Parameters.AddWithValue("Warehouse_id", stock.WarehouseId);
                mc4.Parameters.AddWithValue("Quantity", outgoingItem.Quantity);
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