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
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
        //DbSet<Operations> Operations { get; set; }

        public DbSet<Operation> Operations { get; set; }
        public DbSet<Article> Articles { get; set; }

        public DbSet<Contractor> Contractors { get; set; }
    }
}
