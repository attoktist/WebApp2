using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace WebApp2.Domain.Core
{

    public class Operation : OperationBase
    {
        
        [Write(false)]
        [ForeignKey("Article_ID")]
        public virtual Article Article { get; set; }
        [Write(false)]
        [ForeignKey("Contractor_ID")]
        public virtual Contractor Contractor { get; set; }
        
    }
}
