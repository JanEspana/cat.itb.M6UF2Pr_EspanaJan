using NHibernate;
using cat.itb.M6UF2Pr_EspanaJan.models;
using cat.itb.M6UF2Pr_EspanaJan.Connection;
using Npgsql;
using NHibernate.Criterion;

namespace cat.itb.M6UF2Pr_EspanaJan.CRUDs
{
    public class SupplierCRUD
    {
        public List<Supplier> SelectAllOrders()
        {
            using (var session = SessionFactoryCloud.Open())
            {
                return session.Query<Supplier>().ToList();
            }
        }
        public Supplier SelectSupplierById(int id)
        {
            Supplier supplier;
            using (var session = SessionFactoryCloud.Open())
            {
                supplier = session.Get<Supplier>(id);
                session.Close();
            }
            return supplier;
        }
        public void InsertSupplier(Supplier supplier)
        {
            using (var session = SessionFactoryCloud.Open())
            {
                using (var transaction = session.BeginTransaction())
                {
                    
                   
                        session.Save(supplier);
                        transaction.Commit();
                        Console.WriteLine("Supplier inserted");
                        session.Close();
                    
                }
            }
        }
        public void UpdateSupplier(Supplier supplier)
        {
            using (var session = SessionFactoryCloud.Open())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(supplier);
                    transaction.Commit();
                    Console.WriteLine("Supplier updated");
                }
            }
        }
        public void DeleteSupplier(Supplier supplier)
        {
            using (var session = SessionFactoryCloud.Open())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(supplier);
                        transaction.Commit();
                        Console.WriteLine("Supplier deleted");
                    }
                    catch (Exception e)
                    {
                        if (!transaction.WasCommitted)
                        {
                            transaction.Rollback();
                        }
                        Console.WriteLine("Error deleting supplier");
                    }
                    finally
                    {
                        session.Close();
                    }
                }
            }
        }
        public List<Supplier> SelectHigherThan(double credit)
        {
            string sql = "SELECT * FROM SUPPLIER WHERE CREDIT > @CREDIT";
            List<Supplier> suppliers = new List<Supplier>();
            using (NpgsqlConnection conn = new CloudConnection().GetConnection())
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("CREDIT", credit);
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Supplier supplier = new Supplier();
                            supplier.Id = reader.GetInt32(0);
                            supplier.Name = reader.GetString(1);
                            supplier.Address = reader.GetString(2);
                            supplier.City = reader.GetString(3);
                            supplier.StCode = reader.GetString(4);
                            supplier.ZipCode = reader.GetString(5);
                            supplier.Area = reader.GetDouble(6);
                            supplier.Phone = reader.GetString(7);
                            supplier.Amount = reader.GetDouble(9);
                            supplier.Credit = reader.GetDouble(10);
                            supplier.Remark = reader.GetString(11);
                            suppliers.Add(supplier);
                        }
                    }
                }
            }
            return suppliers;
        }
        public List<Supplier> SelectByCity(string city)
        {
            string sql = "SELECT * FROM SUPPLIER WHERE CITY = @CITY";
            List<Supplier> suppliers = new List<Supplier>();
            using (NpgsqlConnection conn = new CloudConnection().GetConnection())
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("CITY", city);
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Supplier supplier = new Supplier();
                            supplier.Id = reader.GetInt32(0);
                            supplier.Name = reader.GetString(1);
                            supplier.Address = reader.GetString(2);
                            supplier.City = reader.GetString(3);
                            supplier.StCode = reader.GetString(4);
                            supplier.ZipCode = reader.GetString(5);
                            supplier.Area = reader.GetDouble(6);
                            supplier.Phone = reader.GetString(7);
                            supplier.Amount = reader.GetDouble(9);
                            supplier.Credit = reader.GetDouble(10);
                            supplier.Remark = reader.GetString(11);
                            suppliers.Add(supplier);
                        }
                    }
                }
            }
            return suppliers;
        }
        public Supplier SelectLowestAmount()
        {
            using (var session = SessionFactoryCloud.Open())
            {
                QueryOver<Supplier> maxSal = QueryOver.Of<Supplier>()
                    .SelectList(p => p.SelectMin(c => c.Amount));
                Supplier emp = session.QueryOver<Supplier>()
                    .WithSubquery.WhereProperty(c => c.Amount)
                    .Eq(maxSal).SingleOrDefault();
                return emp;
            }
        }
    }
}
