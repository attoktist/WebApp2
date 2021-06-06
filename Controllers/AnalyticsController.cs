using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.UI;
using WebApp2.Models;

namespace WebApp2.Controllers
{
    public class AnalyticsController : ApiController
    {
        private OperationsContext db = new OperationsContext();

        [HttpPost]
        [Route("Analytics/Operations")]
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

                foreach(Operation op in result)
                {
                    countOperations++;
                    sumOperation += op.Sum;
                    if(op.Type==Models.Type.Admission)
                    {
                        countAdmission++;
                        sumAdmission += op.Sum;
                    }
                    if (op.Type == Models.Type.Pay)
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

        [HttpPost]
        [Route("Analytics/Contractors")]
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
                
                for(int i=0;i<contractors.Count();i++)
                {
                    List<Operation> tmpk = new List<Operation>();
                    tmpk = operations.Where(c => c.Contractor.ID == contractors[i]).ToList();

                    foreach(Operation a in tmpk)
                    {
                        countOperations++;
                        sumOperation += a.Sum;
                        if (a.Type == Models.Type.Admission)
                        {
                            countAdmission++;
                            sumAdmission += a.Sum;
                        }
                        if (a.Type == Models.Type.Pay)
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
