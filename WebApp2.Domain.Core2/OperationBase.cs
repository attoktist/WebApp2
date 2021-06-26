using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp2.Domain.Core
{
    [Table("Operations")]
    public class OperationBase
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public Type Type { get; set; }
        public int Sum { get; set; }
        public int? Article_ID { get; set; }
        public int? Contractor_ID { get; set; }
    }
}
