

using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Text;
using WebApp2.Domain.Core;
using WebApp2.Domain.Interfaces;


namespace WebApp2.Infrastructure.Data
{
   public  class ContractorRepository : IRepository<Contractor>
    {
        private ContractorContext db;
        private string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
        public ContractorRepository()
        {
            this.db = new ContractorContext();
        }

        public void Create(Contractor contractor)
        {           
            using (IDbConnection db = new SqlConnection(connectionString))
            {              
                db.Insert<Contractor>(contractor);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                Contractor contractor = Get(id);
                if (contractor != null)
                {
                    db.Delete<Contractor>(contractor);                    
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
            using (IDbConnection db = new SqlConnection(connectionString))
            {               
                db.Update<Contractor>(contractor);
            }
        }
    }
}
