using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using WebApp2.Domain.Core;
using WebApp2.Domain.Interfaces;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;



//using Dapper.Contrib.Extensions;

namespace WebApp2.Infrastructure.Data
{
    public class ArticleRepository : IRepository<Article>
    {
        private ArticleContext db;
        private string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

        public ArticleRepository()
        {
            this.db = new ArticleContext();
        }

        public IEnumerable<Article> GetList()
        {
            return db.Articles;
        }

        public Article Get(int id)
        {
            Article article = db.Articles.Find(id);
            return article;
        }

        public void Create(Article article)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {               
                article.ParentArticle_ID = article.ParentArticle.ID;
                 db.Insert<ArticleBase>(article);
            }

        }

        public void Update(Article article)
        {   
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                article.ParentArticle_ID = article.ParentArticle.ID;
                db.Update<ArticleBase>(article);
            }

        }

        public void Delete(int id)
        {
            

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                Article art = Get(id);
                if(art!=null)
                db.Delete<ArticleBase>(art);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
