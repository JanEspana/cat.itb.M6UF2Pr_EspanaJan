using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6UF2Pr_EspanaJan.models
{
    public class Orderp
    {
        public virtual int Id { get; set; }
        public virtual DateTime OrderDate { get; set; }
        public virtual double Amount { get; set; }
        public virtual DateTime DeliveryDate { get; set; }
        public virtual double Cost { get; set; }
        public virtual Supplier Supplier { get; set; }

        public override string ToString()
        {
            return $"Order: {Id}, {OrderDate}, {Amount}, {DeliveryDate}, {Cost}";
        }


    }
}
