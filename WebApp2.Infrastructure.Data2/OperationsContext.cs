﻿
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;
using WebApp2.Domain.Core;

namespace WebApp2.Infrastructure.Data
{
    public class OperationsContext : DbContext
    {
        public OperationsContext():base("dbConnection")
        {
            //Configuration.LazyLoadingEnabled = false;
            //Configuration.ProxyCreationEnabled = false;
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Operation>()
        //     .HasOptional(x => x.Contractor);

        //}
        //DbSet<Operations> Operations { get; set; }

        public DbSet<Operation> Operations { get; set; }
        public DbSet<Article> Articles { get; set; }

        public DbSet<Contractor> Contractors { get; set; }
    }
}
