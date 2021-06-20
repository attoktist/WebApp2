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
using WebApp2.Domain.Core;
using WebApp2.Domain.Interfaces;
using WebApp2.Infrastructure.Data;

namespace WebApp2.Controllers
{
    public class OperationsController : ApiController
    {
        //private OperationsContext db = new OperationsContext();
         private OperationsRepository repo;

        
       
        public OperationsController()
        {
            repo = new OperationsRepository();
        }

        // GET: api/Operations
        public List<Operation> GetOperations()
        {
            //var a = db.Operations;
            return repo.GetList().ToList(); 
        }

        [HttpPost]
        [Route("api/operations/randomDB")]
        public void RandomBD()
        {            
            repo.RandomBD();
        }

        [HttpPost]
        [Route("api/Operations/filter")]
        public List<Operation> Filter(FilterOperation filter)
        {
            return repo.GetOperations(filter);
        }
        

        // GET: api/Operations/5
        [ResponseType(typeof(Operation))]
        public IHttpActionResult GetOperations(int id)
        {
            Operation operation = repo.Get(id);
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

            repo.Update(operation);

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

            repo.Create(operations);

            return CreatedAtRoute("DefaultApi", new { id = operations.ID }, operations);
        }

        // DELETE: api/Operations/5
        [ResponseType(typeof(Operation))]
        public IHttpActionResult DeleteOperations(int id)
        {
            repo.Delete(id);

            return Ok(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }

        //private bool OperationsExists(int id)
        //{
        //    return db.Operations.Count(e => e.ID == id) > 0;
        //}
    }
}