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
    public class ContractorsController : ApiController
    {
        private ContractorContext db = new ContractorContext();

        // GET: api/Contractors
        public IQueryable<Contractor> GetContractors()
        {
            return db.Contractors;
        }

        // GET: api/Contractors/5
        [ResponseType(typeof(Contractor))]
        public IHttpActionResult GetContractor(int id)
        {
            Contractor contractor = db.Contractors.Find(id);
            if (contractor == null)
            {
                return NotFound();
            }

            return Ok(contractor);
        }

        // PUT: api/Contractors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContractor(int id, Contractor contractor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contractor.ID)
            {
                return BadRequest();
            }

            db.Entry(contractor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractorExists(id))
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

        // POST: api/Contractors
        [ResponseType(typeof(Contractor))]
        public IHttpActionResult PostContractor(Contractor contractor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contractors.Add(contractor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contractor.ID }, contractor);
        }

        // DELETE: api/Contractors/5
        [ResponseType(typeof(Contractor))]
        public IHttpActionResult DeleteContractor(int id)
        {
            Contractor contractor = db.Contractors.Find(id);
            if (contractor == null)
            {
                return NotFound();
            }

            db.Contractors.Remove(contractor);
            db.SaveChanges();

            return Ok(contractor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContractorExists(int id)
        {
            return db.Contractors.Count(e => e.ID == id) > 0;
        }
    }
}