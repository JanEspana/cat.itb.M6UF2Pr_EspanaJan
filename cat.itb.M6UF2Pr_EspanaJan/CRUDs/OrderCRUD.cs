using NHibernate;
using cat.itb.M6UF2Pr_EspanaJan.models;
using cat.itb.M6UF2Pr_EspanaJan.Connection;
using NHibernate.Criterion;
using Npgsql;
using System.Xml.Linq;

namespace cat.itb.M6UF2Pr_EspanaJan.CRUDs
{
    public class OrderCRUD
    {
        public List<Orderp> SelectAllOrders()
        {
            using (var session = SessionFactoryCloud.Open())
            {
                return session.Query<Orderp>().ToList();
            }
        }
        public Orderp SelectOrderById(int id)
        {
            Orderp order;
            using (var session = SessionFactoryCloud.Open())
            {
                order = session.Get<Orderp>(id);
                session.Close();
            }
            return order;
        }
        public void InsertOrder(Orderp order)
        {
            using (var session = SessionFactoryCloud.Open())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(order);
                    transaction.Commit();
                    Console.WriteLine("Order inserted");
                    session.Close();
                }
            }
        }
        public void UpdateOrder(Orderp order)
        {
            using (var session = SessionFactoryCloud.Open())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(order);
                        transaction.Commit();
                        Console.WriteLine("Order updated");
                    }
                    catch (Exception e)
                    {
                        if (!transaction.WasCommitted)
                        {
                            transaction.Rollback();
                        }
                        throw new Exception("Error updating order");
                    }
                }
                session.Close();
            }
        }
        public void DeleteOrder(Orderp order)
        {
            using (var session = SessionFactoryCloud.Open())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(order);
                        transaction.Commit();
                        Console.WriteLine("Order deleted");
                    }
                    catch (Exception e)
                    {
                        if (!transaction.WasCommitted)
                        {
                            transaction.Rollback();
                        }
                        throw new Exception("Error deleting order");
                    }
                }
                session.Close();
            }
        }
        public List<Orderp> SelectOrdersSupplierADO(int supplierno)
        {
            List<Orderp> orders = new List<Orderp>();
            string sql = "SELECT * FROM ORDERP WHERE supplierno = @supplierno";
            using (NpgsqlConnection conn = new CloudConnection().GetConnection())
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("supplierno", supplierno);
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Orderp order = new Orderp();
                            order.Id = reader.GetInt32(0);
                            order.OrderDate = reader.GetDateTime(2);
                            order.Amount = reader.GetDouble(3);
                            order.DeliveryDate = reader.GetDateTime(4);
                            order.Cost = reader.GetDouble(5);

                            orders.Add(order);
                        }
                    }
                }
            }
            return orders;
        }
        public List<Orderp> SelectByCostHigherThan(decimal cost, decimal amount)
        {
            using (var session = SessionFactoryCloud.Open())
            {
                var orders = session.CreateCriteria<Orderp>()
                    .Add(NHibernate.Criterion.Restrictions.Gt("cost", cost))
                    .Add(NHibernate.Criterion.Restrictions.Gt("amount", amount))
                    .List<Orderp>();
                return orders.ToList();
            }
        }
    }
}
