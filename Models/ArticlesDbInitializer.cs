using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApp2.Models
{
    public class ArticlesDbInitializer : DropCreateDatabaseIfModelChanges<ArticleContext>
    {
        
        protected override void Seed(ArticleContext db)
        {
            Random rand = new Random();
            int count = rand.Next(1001, 1500);
            List<Article> articles = new List<Article>();
            for (int i = 0; i < count; i++)
            {
                Article tmp = new Article { Name = "Article" + (i + 1).ToString(), ParentArticle=null };
                articles.Add(tmp);
            }

            //Вложенные статьи 1 уровня
            int art = rand.Next(0, count);
            int parentArt = rand.Next(0, count);
            int k = 0;
            while (k < 300)
            {
                articles[art].ParentArticle = articles[parentArt];
                k++;
                art = rand.Next(0, count);
                parentArt = rand.Next(0, count);
            }
            //вложенные статьи 2 уровня
            parentArt = rand.Next(0, count);
            k = 0;
            foreach (Article article in articles)
            {
                if (article.ParentArticle != null)
                {
                    article.ParentArticle.ParentArticle = articles[parentArt];
                    k++;
                    parentArt = rand.Next(0, count);
                }
                if (k >= 100) break;
            }

            foreach (Article article in articles)
            {
                db.Articles.Add(article);
            }

            base.Seed(db);
        }
    }
}