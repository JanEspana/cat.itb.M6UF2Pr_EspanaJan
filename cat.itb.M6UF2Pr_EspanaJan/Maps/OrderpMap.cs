using FluentNHibernate.Mapping;
using NHibernate;
using cat.itb.M6UF2Pr_EspanaJan.models;

namespace cat.itb.M6UF2Pr_EspanaJan.Maps
{
    public class OrderpMap : ClassMap<Orderp>
    {
        public OrderpMap()
        {
            Table("orderp");
            Id(x => x.Id).Column("id");
            References(x => x.Supplier).Column("supplierno").Not.LazyLoad();
            Map(x => x.OrderDate).Column("orderdate");
            Map(x => x.Amount).Column("amount");
            Map(x => x.DeliveryDate).Column("deliverydate");
            Map(x => x.Cost).Column("cost");
        }
    }
}
