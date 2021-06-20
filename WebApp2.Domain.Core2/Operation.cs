using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp2.Domain.Core
{

    public class Operation
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public Type Type { get; set; }
        public int Sum { get; set; }
        public virtual Article Article { get; set; }
        public virtual Contractor Contractor { get; set; }
        
    }
}
