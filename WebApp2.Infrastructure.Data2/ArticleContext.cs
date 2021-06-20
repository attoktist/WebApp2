using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebApp2.Domain.Core;

namespace WebApp2.Infrastructure.Data
{
    public class ArticleContext : DbContext
    {
        public ArticleContext() : base("dbConnection")
        {

        }
        public DbSet<Article> Articles { get; set; }
    }
}
