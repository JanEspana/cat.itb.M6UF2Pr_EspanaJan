using Npgsql;
using cat.itb.M6UF2Pr_EspanaJan.Connection;
using System.Security.Cryptography;

namespace cat.itb.M6UF2Pr_EspanaJan.CRUDs
{
    public class GeneralCRUD
    {
        public void DropTables(List<string> tables)
        {
            using (NpgsqlConnection connection = new CloudConnection().GetConnection())
            {
                foreach(string table in tables)
                {
                    using (NpgsqlCommand command = new NpgsqlCommand($"DROP TABLE IF EXISTS {table}", connection))
                    {
                        if (command.ExecuteNonQuery() != 0)
                        {
                            Console.WriteLine($"Table {table} dropped");
                        }
                        else
                        {
                            Console.WriteLine($"ERROR. Table {table} not dropped");
                        }
                    }
                }
            }
        }
        public void RunScript()
        {
            List<string> tables = ["orderp", "supplier", "product", "employee"];
            DropTables(tables);
            string path = @"../../../shop.sql";

            using (StreamReader sr = File.OpenText(path))
            {
                string sql = sr.ReadToEnd();
                using (NpgsqlConnection connection = new CloudConnection().GetConnection())
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        if (command.ExecuteNonQuery() != 0)
                        {
                            Console.WriteLine("Database reseted");
                        }
                        else
                        {
                            Console.WriteLine("ERROR. Script not executed");
                        }
                    }
                }
            }
        }
    }
}
