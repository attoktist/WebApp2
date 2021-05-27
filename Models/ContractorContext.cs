using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp2.Models
{
    public class ContractorContext : DbContext
    {
        public ContractorContext() : base("dbConnection")
            {

            }

        public DbSet<Contractor> Contractors { get; set; }
    }
}
