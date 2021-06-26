
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using WebApp2.Domain.Core;
using WebApp2.Domain.Interfaces;

namespace WebApp2.Infrastructure.Data
{
    public class OperationsRepository : IRepository<Operation>
    {
        private OperationsContext db;
        private string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
        public OperationsRepository()
        {
            this.db = new OperationsContext();
        }

        public void RandomBD()
        {

            db.Operations.RemoveRange(db.Operations);

             db.Contractors.RemoveRange(db.Contractors);

             db.Articles.RemoveRange(db.Articles);
            
            Random rand = new Random();
            int count = 55;
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
            count = 13;
            List<Article> articles = new List<Article>();
            for (int i = 0; i < count; i++)
            {
                Article tmp = new Article { Name = "Article" + (i + 1).ToString(), ParentArticle = null };
                articles.Add(tmp);
            }

            foreach (Article article in articles)
            {
                db.Articles.Add(article);
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

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }

            count = 101;
            List<Operation> operations = new List<Operation>();
            DateTime date = DateTime.Now;
            int minYear = date.Year - 2;
            int ca = 13;
            int art1 = rand.Next(1, ca);
            int cc = 55;
            int cont1 = rand.Next(1, cc);
            int year = rand.Next(minYear, date.Year);
            int month = rand.Next(1, 12);
            int day = rand.Next(1, 28);
            int sum = rand.Next(0, 10000);
            int tp = rand.Next(0, 1);
            Domain.Core.Type type = Domain.Core.Type.Admission;

            for (int i = 0; i < count; i++)
            {


                Operation tmp = new Operation
                {
                    Date = new DateTime(year, month, day),
                    Sum = sum,
                    Type = type,                    
                    Contractor = contractors[cont1],
                    Article = articles[art1]
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

        public List<Operation> GetOperations(FilterOperation filter)
        {
            if (filter != null)
            {
                List<Operation> operations = db.Operations
                    .Include(a => a.Article)
                    .Include(c => c.Contractor)
                    .ToList();
                List<Operation> result = operations;
                if (filter.Articles != null)
                {
                    List<Article> id = new List<Article>();
                    foreach (int a in filter.Articles)
                    {
                        id.Add(db.Articles.Find(a));
                    }

                    List<Article> articles = db.Articles.ToList();
                    List<Article> filterArt = new List<Article>();

                    filterArt = FilterOperation.getAllParentArticle(articles, id);
                    filterArt = filterArt.Distinct().ToList();

                    foreach (Article article in filterArt)
                    {
                        result.AddRange(operations.Where(a => a.Article!=null && a.Article.ID == article.ID).ToList());
                    }
                }

                if (filter.Contractors != null)
                {
                    List<Operation> tmp = new List<Operation>();

                    foreach (int a in filter.Contractors)
                    {
                        tmp.AddRange(result.Where(c => c.Contractor!=null && c.Contractor.ID == a));
                    }


                    result = tmp;
                }

                if (filter.StartDate.Year != 1)
                {
                    result = result.Where(op => op.Date > filter.StartDate)
                    .ToList();
                }
                if (filter.EndDate.Year != 1)
                {
                    result = result.Where(op => op.Date < filter.EndDate)
                    .ToList();
                }


                return result;
            }

            return null;
        }

        public void Create(Operation operation)
        {
            if(operation!=null)
            {               
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    operation.Article_ID = operation.Article.ID;
                    operation.Contractor_ID = operation.Contractor.ID;
                    db.Insert<OperationBase>(operation);
                }

            }
        }

        public void Delete(int id)
        {           
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                Operation operation = Get(id);
                if(operation!=null)
                {
                    db.Delete<OperationBase>(operation);
                }
            }

        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Operation Get(int id)
        {
            Operation operation = db.Operations.Find(id);
            return operation;
        }

        public IEnumerable<Operation> GetList()
        {
            
            return db.Operations
                .Include(a => a.Article)
                .Include(c => c.Contractor)
                .ToList();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Operation operation)
        {           
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                operation.Article_ID = operation.Article.ID;
                operation.Contractor_ID = operation.Contractor.ID;
                db.Update<OperationBase>(operation);
            }
        }

        public int[] GetInformationByOperations(FilterOperation filter)
        {
            if (filter != null)
            {
                List<Operation> operations = db.Operations
                    .Include(a => a.Article)
                    .Include(c => c.Contractor)
                    .ToList();
                List<Operation> result = null;
                if (filter.Articles != null)
                {
                    List<Article> id = new List<Article>();
                    foreach (int a in filter.Articles)
                    {
                        id.Add(db.Articles.Find(a));
                    }

                    List<Article> articles = db.Articles.ToList();
                    List<Article> filterArt = new List<Article>();

                    filterArt = FilterOperation.getAllParentArticle(articles, id);
                    filterArt = filterArt.Distinct().ToList();

                    foreach (Article article in filterArt)
                    {
                        result.AddRange(operations.Where(a => a.Article.ID == article.ID).ToList());
                    }
                }

                if (filter.Contractors != null)
                {
                    List<Operation> tmp = new List<Operation>();

                    foreach (int a in filter.Contractors)
                    {
                        tmp.AddRange(result.Where(c => c.Contractor.ID == a));
                    }


                    result = tmp;
                }
                if (result == null) result = operations;
                if (filter.StartDate.Year != 1)
                {
                    result = result.Where(op => op.Date > filter.StartDate)
                    .ToList();
                }
                if (filter.EndDate.Year != 1)
                {
                    result = result.Where(op => op.Date < filter.EndDate)
                    .ToList();
                }

                int countOperations = 0;
                int sumOperation = 0;
                int countAdmission = 0;
                int sumAdmission = 0;
                int countPay = 0;
                int sumPay = 0;

                foreach (Operation op in result)
                {
                    countOperations++;
                    sumOperation += op.Sum;
                    if (op.Type == Domain.Core.Type.Admission)
                    {
                        countAdmission++;
                        sumAdmission += op.Sum;
                    }
                    if (op.Type == Domain.Core.Type.Pay)
                    {
                        countPay++;
                        sumPay += op.Sum;
                    }
                }
                int[] resFilter = new int[6];
                resFilter[0] = countOperations;
                resFilter[1] = sumOperation;
                resFilter[2] = countAdmission;
                resFilter[3] = sumAdmission;
                resFilter[4] = countPay;
                resFilter[5] = sumPay;

                return resFilter;
            }

            return null;
        }

        public List<int[]> GetInformationsByContractors(int[] contractors)
        {
            if (contractors != null)
            {
                List<Operation> operations = db.Operations
                    .Include(a => a.Article)
                    .Include(c => c.Contractor)
                    .ToList();


                int countOperations = 0;
                int sumOperation = 0;
                int countAdmission = 0;
                int sumAdmission = 0;
                int countPay = 0;
                int sumPay = 0;
                List<int[]> resfilter = new List<int[]>();

                for (int i = 0; i < contractors.Count(); i++)
                {
                    List<Operation> tmpk = new List<Operation>();
                    tmpk = operations.Where(c => c.Contractor.ID == contractors[i]).ToList();

                    foreach (Operation a in tmpk)
                    {
                        countOperations++;
                        sumOperation += a.Sum;
                        if (a.Type == Domain.Core.Type.Admission)
                        {
                            countAdmission++;
                            sumAdmission += a.Sum;
                        }
                        if (a.Type == Domain.Core.Type.Pay)
                        {
                            countPay++;
                            sumPay += a.Sum;
                        }
                    }

                    int[] resFilter = new int[7];
                    resFilter[0] = contractors[i];
                    resFilter[1] = countOperations;
                    resFilter[2] = sumOperation;
                    resFilter[3] = countAdmission;
                    resFilter[4] = sumAdmission;
                    resFilter[5] = countPay;
                    resFilter[6] = sumPay;
                    resfilter.Add(resFilter);

                    countOperations = 0;
                    sumOperation = 0;
                    countAdmission = 0;
                    sumAdmission = 0;
                    countPay = 0;
                    sumPay = 0;
                }



                return resfilter;
            }
            return null;
        }
    }
}
