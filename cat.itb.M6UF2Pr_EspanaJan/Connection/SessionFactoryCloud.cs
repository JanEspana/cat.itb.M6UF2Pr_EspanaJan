using FluentNHibernate.Cfg.Db;
using NHibernate;
using FluentNHibernate.Cfg;
using cat.itb.M6UF2Pr_EspanaJan.models;
using NHibernate.Criterion;

namespace cat.itb.M6UF2Pr_EspanaJan.Connection
{
    public class SessionFactoryCloud
    {
        private static string ConnectionString = "Server=flora.db.elephantsql.com;Port=5432;Database=mgzmmbwh;User Id=mgzmmbwh;Password=Ycx0FKBYQtUhDpD-glvDEp0m6yxMTKT3;";
        private static ISessionFactory session;

        public static ISessionFactory CreateSession()
        {
            if (session != null)
                return session;

            IPersistenceConfigurer configDB = PostgreSQLConfiguration.PostgreSQL82.ConnectionString(ConnectionString);
            var configMap =
                Fluently.Configure().Database(configDB)
                .Mappings(c => c.FluentMappings.AddFromAssemblyOf<Employee>())
                .Mappings(c => c.FluentMappings.AddFromAssemblyOf<Order>())
                .Mappings(c => c.FluentMappings.AddFromAssemblyOf<Supplier>())
                .Mappings(c => c.FluentMappings.AddFromAssemblyOf<Product>());

            session = configMap.BuildSessionFactory();

            return session;
        }

        public static ISession Open()
        {
            return CreateSession().OpenSession();
        }
    }
}