using FluentNHibernate.Mapping;
using cat.itb.M6UF2Pr_EspanaJan.models;
using NHibernate;

namespace cat.itb.M6UF2Pr_EspanaJan.Maps
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Table("product");
            Id(x => x.Id).Column("id");
            Map(x => x.Code).Column("code");
            Map(x => x.Description).Column("description");
            Map(x => x.CurrentStock).Column("currentstock");
            Map(x => x.MinStock).Column("minstock");
            Map(x => x.Price).Column("price");
            References(x => x.Employee).Column("empno");
            HasOne(x => x.Supplier)
                .PropertyRef(nameof(Supplier.Product))
                .Not.LazyLoad()
                .Cascade.AllDeleteOrphan()
                .Fetch.Join();
        }
    }
}
