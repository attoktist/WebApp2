using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp2.Domain.Core;
using WebApp2.Domain.Interfaces;
using WebApp2.Infrastructure.Data;

namespace WebApp2.Controllers
{
    public class ContractorsController : ApiController
    {
        // private ContractorContext db = new ContractorContext();
        private ContractorRepository repo;

        public ContractorsController()
        {
            repo = new ContractorRepository();
        }

        // GET: api/Contractors
        public List<Contractor> GetContractors()
        {
            return repo.GetList().ToList();
        }

        // GET: api/Contractors/5
        [ResponseType(typeof(Contractor))]
        public IHttpActionResult GetContractor(int id)
        {
            Contractor contractor = repo.Get(id);
            if (contractor == null)
            {
                return NotFound();
            }

            return Ok(contractor);
        }

        // PUT: api/Contractors/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateContractor(int id, Contractor contractor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contractor.ID)
            {
                return BadRequest();
            }

            repo.Update(contractor);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Contractors
        [HttpPost]
        [ResponseType(typeof(Contractor))]
        public IHttpActionResult CreateContractor(Contractor contractor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repo.Create(contractor);

            return CreatedAtRoute("DefaultApi", new { id = contractor.ID }, contractor);
        }

        // DELETE: api/Contractors/5
        [HttpDelete]
        [ResponseType(typeof(Contractor))]
        public IHttpActionResult DeleteContractor(int id)
        {
            Contractor contractor = repo.Get(id);
            if (contractor == null)
            {
                return NotFound();
            }

            repo.Delete(id);

            return Ok(contractor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }

        //private bool ContractorExists(int id)
        //{
        //    return db.Contractors.Count(e => e.ID == id) > 0;
        //}
    }
}