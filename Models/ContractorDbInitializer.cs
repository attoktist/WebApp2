using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace WebApp2.Models
{
    public class ContractorDbInitializer : DropCreateDatabaseIfModelChanges<ContractorContext>
    {

        public override void InitializeDatabase(ContractorContext context)
        {         
            base.InitializeDatabase(context);
        }
        protected override void Seed(ContractorContext db)
        {
            Random rand = new Random();
            int count = rand.Next(5001, 10000);
            List<Contractor> contractors = new List<Contractor>();
            for (int i = 0; i < count; i++)
            {
                contractors.Add(new Contractor { Name = "Cont" + (i + 1).ToString() });
            }

            foreach (Contractor contractor in contractors)
            {
                db.Contractors.Add(contractor);
            }          
            
            

            base.Seed(db);
        }

    }
}