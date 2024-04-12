using NHibernate;
using Npgsql;
using cat.itb.M6UF2Pr_EspanaJan.models;
using cat.itb.M6UF2Pr_EspanaJan.Connection;

namespace cat.itb.M6UF2Pr_EspanaJan.CRUDs
{
    public class ProductCRUD
    {
        public List<Product> SelectAllProducts()
        {
            using (var session = SessionFactoryCloud.Open())
            {
                return session.Query<Product>().ToList();
            }
        }
        public Product SelectProductById(int id)
        {
            Product product;
            using (var session = SessionFactoryCloud.Open())
            {
                product = session.Get<Product>(id);
                session.Close();
            }
            return product;
        }
        public void InsertProduct(Product product)
        {
            using (var session = SessionFactoryCloud.Open())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(product);
                    transaction.Commit();
                    Console.WriteLine("Product inserted");
                    session.Close();
                }
            }
        }
        public void UpdateProduct(Product product)
        {
            using (var session = SessionFactoryCloud.Open())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(product);
                        transaction.Commit();
                        Console.WriteLine("Product updated");
                    }
                    catch (Exception e)
                    {
                        if (!transaction.WasCommitted)
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
        }
        public void DeleteProduct(Product product)
        {
            using (var session = SessionFactoryCloud.Open())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(product);
                    transaction.Commit();
                    Console.WriteLine("Product deleted");
                    session.Close();
                }
            }
        }
        public Product SelectByCodeADO(int code)
        {
            CloudConnection cloudConnection = new CloudConnection();
            NpgsqlConnection conn = cloudConnection.GetConnection();
            string sql = "SELECT * FROM product WHERE code = " + code;
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            Product product = new Product();
            if (dr.Read())
            {
                product.Id = dr.GetInt32(0);
                product.Code = dr.GetInt32(1);
                product.Description = dr.GetString(2);
                product.CurrentStock = dr.GetInt32(3);
                product.MinStock = dr.GetInt32(4);
                product.Price = dr.GetDouble(5);
            }
            dr.Close();
            conn.Close();
            return product;
        }
        public void UpdateADO(List<Product> products)
        {
            string sql = "UPDATE Product SET Description = @description, CurrentStock = @currentstock, MinStock = @minstock, Price = @price, EmpNo = @empno WHERE Code = @code";
            using (NpgsqlConnection connection = new CloudConnection().GetConnection())
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    foreach (Product product in products)
                    {
                        command.Parameters.AddWithValue("description", product.Description);
                        command.Parameters.AddWithValue("currentstock", product.CurrentStock);
                        command.Parameters.AddWithValue("minstock", product.MinStock);
                        command.Parameters.AddWithValue("price", product.Price);
                        command.Parameters.AddWithValue("code", product.Code);
                        if (command.ExecuteNonQuery() != 0)
                        {
                            Console.WriteLine($"Product {product.Code} updated");
                        }
                        else
                        {
                            Console.WriteLine($"Product {product.Code} no updated");
                        }
                        command.Parameters.Clear();
                    }
                }
            }
        }
        public List<Product> SelectByPriceLowThan(double cost)
        {
            using (var session = SessionFactoryCloud.Open())
            {
                return session.Query<Product>().Where(p => p.Price < cost).ToList();
            }
        }
    }
}
