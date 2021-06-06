using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApp2.Models
{
    public class OperationsDbInitializer : DropCreateDatabaseIfModelChanges<OperationsContext>
    {

        protected override void Seed(OperationsContext db)
        {
           // db.Configuration.AutoDetectChangesEnabled = false;
           // db.Configuration.ValidateOnSaveEnabled = false;
            //контрагенты
            Random rand = new Random();
            int count = 5500;
            List<Contractor> contractors = new List<Contractor>();
            for (int i = 0; i < count; i++)
            {
                contractors.Add(new Contractor { Name = "Cont" + (i + 1).ToString() });
            }

            
                foreach (Contractor contractor in contractors)
                {
                    db.Contractors.Add(contractor);
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            




            //статьи
            count = 1300;
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

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                }
           



            count = 10100;

            List<Operation> operations = new List<Operation>();

            
                DateTime date = DateTime.Now;
                int minYear = date.Year - 2;
            int ca = db.Articles.Count();

            int art1 = rand.Next(1, ca);
            int cc = db.Contractors.Count();
                
                int cont1 = rand.Next(1, cc);
               
                int year = rand.Next(minYear, date.Year);
                int month = rand.Next(1, 12);
                int day = rand.Next(1, 28);
                int sum = rand.Next(0, 10000);
                int tp = rand.Next(0, 1);
                Type type = Type.Admission;

            
                for (int i = 0; i < count; i++)
                {


                    Operation tmp = new Operation
                    {
                        Date = new DateTime(year, month, day),
                        Sum = sum,
                        Type = type,
                        //Contractor = contractors[cont1],
                        //Article = articles[art1]
                        Contractor = db.Contractors.Find(cont1),
                        Article = db.Articles.Find(art1)
                    };

                    operations.Add(tmp);
                    cont1 = rand.Next(1, cc);
                    art1 = rand.Next(1, ca);
                    year = rand.Next(minYear, date.Year);
                    month = rand.Next(1, 12);
                    day = rand.Next(1, 28);
                    sum = rand.Next(0, 10000);
                    tp = rand.Next(0, 2);
                    if (tp == 0) type = Type.Admission;
                    else if (tp == 1) type = Type.Pay;
                }

            //for (int j = 0; j <operations.Count/100; j++)
            //{
            //    for(int i=j*100;i<100;i++)
            //    {
            //        db.Operations.Add(operations[i]);

            //        try
            //        {
            //            db.SaveChanges();
            //        }
            //        catch (Exception ex)
            //        {

            //        }
            //    }
            //}


            foreach (Operation operation in operations)
            {

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }

            //db.Configuration.AutoDetectChangesEnabled = true;
            // db.Configuration.ValidateOnSaveEnabled = true;
        }
    }
}

