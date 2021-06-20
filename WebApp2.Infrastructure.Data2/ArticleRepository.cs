using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using WebApp2.Domain.Core;
using WebApp2.Domain.Interfaces;

namespace WebApp2.Infrastructure.Data
{
    public class ArticleRepository : IRepository<Article>
    {
        private ArticleContext db;

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
            
            db.Articles.Add(article);
            db.SaveChanges();
        }

        public void Update(Article article)
        {            
            if(article!=null) db.Entry(article).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
               
            }

           
        }

        public void Delete(int id)
        {
            Article article = db.Articles.Find(id);
            if (article != null)
            {
                // return NotFound();
                db.Articles.Remove(article);
                db.SaveChanges();
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
