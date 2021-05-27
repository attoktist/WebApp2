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
using WebApp2.Models;

namespace WebApp2.Controllers
{
    public class OperationsController : ApiController
    {
        private OperationsContext db = new OperationsContext();

        // GET: api/Operations
        public IQueryable<Operations> GetOperations()
        {
            return db.Operations;
        }

        // GET: api/Operations/5
        [ResponseType(typeof(Operations))]
        public IHttpActionResult GetOperations(int id)
        {
            Operations operations = db.Operations.Find(id);
            if (operations == null)
            {
                return NotFound();
            }

            return Ok(operations);
        }

        // PUT: api/Operations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOperations(int id, Operations operations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != operations.ID)
            {
                return BadRequest();
            }

            db.Entry(operations).State = EntityState.Modified;

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
        [ResponseType(typeof(Operations))]
        public IHttpActionResult PostOperations(Operations operations)
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
        [ResponseType(typeof(Operations))]
        public IHttpActionResult DeleteOperations(int id)
        {
            Operations operations = db.Operations.Find(id);
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