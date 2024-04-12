using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Criterion;

namespace cat.itb.M6UF2Pr_EspanaJan.models
{
    public class Supplier
    {
        public virtual int Id { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? Address { get; set; }
        public virtual string? City { get; set; }
        public virtual string? StCode { get; set; }
        public virtual string? ZipCode { get; set; }
        public virtual double? Area { get; set; }
        public virtual string? Phone { get; set; }
        public virtual double? Amount { get; set; }
        public virtual double? Credit { get; set; }
        public virtual string? Remark { get; set; }
        public virtual Product? Product { get; set; }
        public virtual ISet<Orderp>? Orders { get; set; }

        public override string ToString()
        {
            return $"Supplier: {Id}, {Name}, {Address}, {City}, {StCode}, {ZipCode}, {Area}, {Phone}, {Amount}, {Credit}, {Remark}";
        }

    }
}
