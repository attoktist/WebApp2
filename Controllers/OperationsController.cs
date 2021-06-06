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
    public class OperationsController : ApiController
    {
        private OperationsContext db = new OperationsContext();

       

        // GET: api/Operations
        public List<Operation> GetOperations()
        {
            //var a = db.Operations;
            return db.Operations
                .Include(a => a.Article)
                .Include(c => c.Contractor)
                .ToList(); 
        }

        [HttpPost]
        [Route("api/Operations/randomDB")]
        public void RandomBD()
        {
            // db.Configuration.AutoDetectChangesEnabled = false;
            // db.Configuration.ValidateOnSaveEnabled = false;
            //контрагенты
            db.Operations.RemoveRange(db.Operations);
            db.Contractors.RemoveRange(db.Contractors);
            db.Articles.RemoveRange(db.Articles);
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
            Models.Type type = Models.Type.Admission;


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
                if (tp == 0) type = Models.Type.Admission;
                else if (tp == 1) type = Models.Type.Pay;
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

        [HttpPost]
        [Route("api/Operations/filter")]
        public List<Operation> GetOperations(FilterOperation filter)
        {
          if(filter!=null)
            {                
                List<Operation> operations = db.Operations
                    .Include(a => a.Article)
                    .Include(c => c.Contractor)
                    .ToList();
                List<Operation> result = new List<Operation>();
                if (filter.Articles != null)
                {
                    List<Article> id = new List<Article>();
                    foreach(int a in filter.Articles)
                    {
                        id.Add(db.Articles.Find(a));
                    }
                    
                    List<Article> articles = db.Articles.ToList();
                    List<Article> filterArt = new List<Article>();

                    filterArt = FilterOperation.getAllParentArticle(articles, id);
                    filterArt = filterArt.Distinct().ToList();

                    foreach(Article article in filterArt)
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
        

        // GET: api/Operations/5
        [ResponseType(typeof(Operation))]
        public IHttpActionResult GetOperations(int id)
        {
            Operation operation = db.Operations.Find(id);
            if (operation == null)
            {
                return NotFound();
            }

            return Ok(operation);
        }

        // PUT: api/Operations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOperations(int id, Operation operation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != operation.ID)
            {
                return BadRequest();
            }

            Operation tmp = db.Operations.Find(id);
            tmp.Article = operation.Article;
            tmp.Contractor = operation.Contractor;
            tmp.Date = operation.Date;
            tmp.Sum = operation.Sum;
            tmp.Type = operation.Type;
            db.Entry(tmp).State = EntityState.Modified;
            
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperationsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Operations
        [ResponseType(typeof(Operation))]
        public IHttpActionResult PostOperations(Operation operations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Operations.Add(operations);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = operations.ID }, operations);
        }

        // DELETE: api/Operations/5
        [ResponseType(typeof(Operation))]
        public IHttpActionResult DeleteOperations(int id)
        {
            Operation operations = db.Operations.Find(id);
            if (operations == null)
            {
                return NotFound();
            }

            db.Operations.Remove(operations);
            db.SaveChanges();

            return Ok(operations);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OperationsExists(int id)
        {
            return db.Operations.Count(e => e.ID == id) > 0;
        }
    }
}