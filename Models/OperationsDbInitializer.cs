using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp2.Domain.Core;
using WebApp2.Infrastructure.Data;

namespace WebApp2.Models
{
    public class OperationsDbInitializer : DropCreateDatabaseIfModelChanges<OperationsContext>
    {

        protected override void Seed(OperationsContext db)
        {
            //контрагенты
            Random rand = new Random();
            int count = 10;
            List<Contractor> contractors = new List<Contractor>();
            for (int i = 0; i < count; i++)
            {
                contractors.Add(new Contractor { Name = "Cont" + (i + 1).ToString() });
            }

            foreach (Contractor contractor in contractors)
            {
                db.Contractors.Add(contractor);
                db.SaveChanges();
            }
            
            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (Exception ex)
            //{

            //}


            //статьи
            count = 10;
            List<Article> articles = new List<Article>();
            for (int i = 0; i < count; i++)
            {
                Article tmp = new Article { Name = "Article" + (i + 1).ToString(), ParentArticle = null };
                articles.Add(tmp);
            }

            //Вложенные статьи 1 уровня
            int art = rand.Next(0, count);
            int parentArt = rand.Next(0, count);
            int k = 0;
            while (k < 3)
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
                if (k >= 1) break;
            }

            foreach (Article article in articles)
            {
                db.Articles.Add(article);      
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }



            count = 10;
            
            List<Operation> operations = new List<Operation>();

            
            DateTime date = DateTime.Now;
            int minYear = date.Year - 2;
            int cc = contractors.Count;
            int cont1 = rand.Next(1, cc);
            int ca = articles.Count;
            int art1 = rand.Next(1, ca);
            int year = rand.Next(minYear, date.Year);
            int month = rand.Next(1, 12);
            int day = rand.Next(1, 28);
            int sum = rand.Next(0, 10000);
            int tp = rand.Next(0, 1);
            Domain.Core.Type type = Domain.Core.Type.Admission;

            for (int i=0;i<count;i++)
            {
               
                
                Operation tmp = new Operation
                {                    
                    Date = new DateTime(year, month, day),
                    Sum = sum,
                    Type = type,
                    Article = articles[art1],
                    Contractor = contractors[cont1]
                };

                operations.Add(tmp);
                cont1 = rand.Next(1, cc);
                art1 = rand.Next(1, ca);
                year = rand.Next(minYear, date.Year);
                 month = rand.Next(1, 12);
                 day = rand.Next(1, 28);
                 sum = rand.Next(0, 10000);                             
                tp = rand.Next(0, 2);
                if (tp == 0) type = Domain.Core.Type.Admission;
                else if (tp == 1) type = Domain.Core.Type.Pay;
            }

            
           

            foreach (Operation operation in operations)
            {
                //cont1 = rand.Next(1, contractors.Count);
                //art1 = rand.Next(1, articles.Count);
                //operation.Article = articles[art1];
                //operation.Contractor = contractors[cont1];
                db.Operations.Add(operation);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
            
        }
    }
}

