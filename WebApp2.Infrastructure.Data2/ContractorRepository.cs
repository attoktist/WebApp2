

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Text;
using WebApp2.Domain.Core;
using WebApp2.Domain.Interfaces;


namespace WebApp2.Infrastructure.Data
{
   public  class ContractorRepository : IRepository<Contractor>
    {
        private ContractorContext db;

        public ContractorRepository()
        {
            this.db = new ContractorContext();
        }

        public void Create(Contractor contractor)
        {
            if(contractor!=null) db.Contractors.Add(contractor);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!ContractorExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }
        }

        public void Delete(int id)
        {
            Contractor contractor = db.Contractors.Find(id);
            if (contractor != null)
            {
                db.Contractors.Remove(contractor);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                }
            }

            
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Contractor Get(int id)
        {
            Contractor contractor = db.Contractors.Find(id);
            return contractor;
        }

        public IEnumerable<Contractor> GetList()
        {
            return db.Contractors;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Contractor contractor)
        {
            if(contractor!=null) db.Entry(contractor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
               
            }
        }
    }
}
