using Microsoft.Data.SqlClient;

// @ လေးခံထားရင် \ တစ်ခုပဲ သုံးလို့ရပါတယ်
// Server= နေရာမှာ SSMS က ကူးလာတဲ့ နာမည်ကို သေချာထည့်ပါ
string connectionString = @"Server=localhost\SQLEXPRESS;Database=InventoryDB;Trusted_Connection=True;TrustServerCertificate=True;";

while (true)
{
    Console.WriteLine("\n--- Inventory Management System ---");
    Console.WriteLine("1. Add  2. View  3. Update Price  4. Delete  0. Exit");
    Console.Write("ရွေးချယ်ရန်: ");
    string? choice = Console.ReadLine();

    try 
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            if (choice == "1")
            {
                Console.Write("Name: "); string name = Console.ReadLine() ?? "";
                Console.Write("Price: "); decimal price = decimal.Parse(Console.ReadLine() ?? "0");
                Console.Write("Qty: "); int qty = int.Parse(Console.ReadLine() ?? "0");

                string query = "INSERT INTO Products (Name, Price, Quantity) VALUES (@name, @price, @qty)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.ExecuteNonQuery();
                Console.WriteLine("အောင်မြင်စွာ ထည့်သွင်းပြီးပါပြီ။");
            }
            else if (choice == "2")
            {
                string query = "SELECT * FROM Products";
                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nID | Name | Price | Qty");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Id"]} | {reader["Name"]} | {reader["Price"]} | {reader["Quantity"]}");
                    }
                }
            }
            else if (choice == "0") break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error တက်သွားပါတယ်: {ex.Message}");
    }
}