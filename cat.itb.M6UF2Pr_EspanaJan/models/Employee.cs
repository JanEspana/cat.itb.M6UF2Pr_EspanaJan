using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6UF2Pr_EspanaJan.models
{
    public class Employee
    {
        public virtual int Id { get; set; }
        public virtual string? Surname { get; set; }
        public virtual string? Job { get; set; }
        public virtual int? ManagerNo { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual double? Salary { get; set; }
        public virtual double? Commission { get; set; }
        public virtual int? DeptNo { get; set; }
        public virtual ISet<Product>? Products { get; set; }

        public override string ToString()
        {
            return $"Employee: {Id}, {Surname}, {Job}, {ManagerNo}, {StartDate}, {Salary}, {Commission}, {DeptNo}";
        }

    }
}
