using FluentNHibernate.Mapping;
using cat.itb.M6UF2Pr_EspanaJan.models;
using NHibernate;

namespace cat.itb.M6UF2Pr_EspanaJan.Maps
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Table("employee");
            Id(x => x.Id).Column("id");
            Map(x => x.Surname).Column("surname");
            Map(x => x.Job).Column("job");
            Map(x => x.ManagerNo).Column("managerno");
            Map(x => x.StartDate).Column("startdate");
            Map(x => x.Salary).Column("salary");
            Map(x => x.Commission).Column("commission");
            Map(x => x.DeptNo).Column("deptno");
            HasMany(x => x.Products)
                .KeyColumn("empno")
                .Not.LazyLoad()
                .Fetch.Join()
                .AsSet();

        }
    }
}
