using Microsoft.Data.SqlClient;

// Server နာမည်နေရာမှာ မင်းရဲ့ Computer Name\SQLEXPRESS ကို အစားထိုးဖို့ မမေ့ပါနဲ့
string connectionString = "Server=localhost\\QLEXPRESS;Database=master;Trusted_Connection=True;";

while (true)
{
    Console.WriteLine("\n--- Inventory Management System ---");
    Console.WriteLine("1. Add  2. View  3. Update Price  4. Delete  0. Exit");
    Console.Write("ရွေးချယ်ရန်: ");
    string choice = Console.ReadLine();

    using (SqlConnection conn = new SqlConnection(connectionString))
    {
        conn.Open();
        if (choice == "1") // ADD
        {
            Console.Write("Name: "); string name = Console.ReadLine();
            Console.Write("Price: "); decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Qty: "); int qty = int.Parse(Console.ReadLine());

            string query = "INSERT INTO Products (Name, Price, Quantity) VALUES (@name, @price, @qty)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@qty", qty);
            cmd.ExecuteNonQuery();
            Console.WriteLine("အောင်မြင်စွာ ထည့်သွင်းပြီးပါပြီ။");
        }
        else if (choice == "2") // VIEW
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
        else if (choice == "3") // UPDATE
        {
            Console.Write("ပြင်မည့် ပစ္စည်း ID ကိုရိုက်ပါ: "); int id = int.Parse(Console.ReadLine());
            Console.Write("စျေးနှုန်းအသစ်: "); decimal newPrice = decimal.Parse(Console.ReadLine());

            string query = "UPDATE Products SET Price = @price WHERE Id = @id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@price", newPrice);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            Console.WriteLine("စျေးနှုန်း ပြင်ဆင်ပြီးပါပြီ။");
        }
        else if (choice == "4") // DELETE
        {
            Console.Write("ဖျက်မည့် ပစ္စည်း ID ကိုရိုက်ပါ: "); int id = int.Parse(Console.ReadLine());
            string query = "DELETE FROM Products WHERE Id = @id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            Console.WriteLine("ဖျက်သိမ်းပြီးပါပြီ။");
        }
        else if (choice == "0") break;
    }
}