using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebApp2.Domain.Core;

namespace WebApp2.Infrastructure.Data
{
    public class ContractorContext : DbContext
    {
        public ContractorContext() : base("dbConnection")
            {

            }

        public DbSet<Contractor> Contractors { get; set; }
    }
}
