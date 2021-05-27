using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp2.Models
{
    public class ArticleContext : DbContext
    {
        public ArticleContext() : base("dbConnection")
        {

        }
        public DbSet<Article> Articles { get; set; }
    }
}
