using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApp2.Models
{
    public class OperationsDbInitializer : DropCreateDatabaseIfModelChanges<OperationsContext>
    {

        protected override void Seed(OperationsContext db)
        {
            Random rand = new Random();
            int count = rand.Next(100000, 110000);

            List<Operations> operations = new List<Operations>();

            
            DateTime date = DateTime.Now;
            int minYear = date.Year - 2;
            

            for (int i=0;i<count;i++)
            {
                int cont = rand.Next(1, 10000);
                int art = rand.Next(1, 1500);
                int year = rand.Next(minYear, date.Year);
                int month = rand.Next(1, 12);
                int day = rand.Next(1, 31);
                int sum = rand.Next(0, 10000);
                int tp = rand.Next(0, 1);
                Type type=Type.Admission;
                if (tp == 0) type = Type.Admission;
                else if (tp == 1) type = Type.Pay;
                Operations tmp = new Operations
                {
                    Article = null,
                    Contractor = null,
                    Date = new DateTime(year, month, day),
                    Sum = sum,
                    Type = type
                };
            }
        }
    }
}

