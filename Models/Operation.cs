using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp2.Models
{

    public class Operation
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public Type Type { get; set; }
        public int Sum { get; set; }
        public Contractor Contractor { get; set; }
        public Article Article { get; set; }
    }
}
