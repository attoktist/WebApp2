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