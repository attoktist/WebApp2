using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp2.Models
{
    public class OperationsContext : DbContext
    {
        public OperationsContext():base("dbConnection")
        {

        }
        //DbSet<Operations> Operations { get; set; }

        public DbSet<Operations> Operations { get; set; }
    }
}
