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
     public void SaveProduct(Product product, Category category,Incoming incoming, IncomingItem incomingItem, Stock stock, Warehouse warehouse, Supplier supplier )
    {
        string sql1="insert into Products  values(0, @Name,@Unit,@Weight,@Category_id)";
        string sql2 = "insert into Incoming values (0, @Doc_number, @Supplier_id, @Warehouse_id, @Date, @Status)";
        string sql3 = "insert into Incoming_items values (0, @Incoming_id, @Product_id, @Cost, @Quantity)";
        string sql4 ="insert into Stock values (0, @Product_id, @Warehouse_id ,@Quantity, @Reserved, @Last_updated)";
        using var transaction = connection.BeginTransaction();
        try
        {
            connection.Open();
            using (var mc1 = new MySqlCommand(sql1, connection, transaction))
            {
                mc1.Parameters.AddWithValue("Name", product.Name );
                mc1.Parameters.AddWithValue("Unit", product.Unit );
                mc1.Parameters.AddWithValue("Weight", product.Weight );
                mc1.Parameters.AddWithValue("Category_id", category.Id);
                mc1.ExecuteNonQuery();
            }
            using (var mc2 = new MySqlCommand(sql2, connection, transaction))
            {
                mc2.Parameters.AddWithValue("DocNumber", incoming.DocNumber );
                mc2.Parameters.AddWithValue("Supplier_id", supplier.Id);
                mc2.Parameters.AddWithValue("Warehouse_id", warehouse.Id );
                mc2.Parameters.AddWithValue("Date", incoming.Date );
                mc2.ExecuteNonQuery();
            }
            using (var mc3 = new MySqlCommand(sql3, connection, transaction))
            {
                mc3.Parameters.AddWithValue("Incoming_id", incoming.Id );
                mc3.Parameters.AddWithValue("Product_id", product.Id);
                mc3.Parameters.AddWithValue("Cost", incomingItem.Cost);
                mc3.Parameters.AddWithValue("Quantity", incomingItem.Quantity);
                mc3.ExecuteNonQuery();
            }
            using (var mc4 = new MySqlCommand(sql4, connection, transaction))
            {
                mc4.Parameters.AddWithValue("Product_id", product.Id );
                mc4.Parameters.AddWithValue("Warehouse_id", warehouse.Id );
                mc4.Parameters.AddWithValue("Quantity", stock.Quantity );
                mc4.Parameters.AddWithValue("Reserved", stock.Reserved );
                mc4.Parameters.AddWithValue("Last_updated", stock.LastUpdated );
                mc4.ExecuteNonQuery();
            }
            connection.Close();
            transaction.Commit();
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            transaction.Rollback();
            
        }
    }
    
}