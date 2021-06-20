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
//using WebApp2.Models;

namespace WebApp2.Controllers
{
    public class AnalyticsController : ApiController
    {
        //private OperationsContext db = new OperationsContext();
        private OperationsRepository repo;

        public AnalyticsController(OperationsRepository r)
        {
            repo = r;
        }
        [HttpPost]
        [Route("Analytics/Operations")]
        public int[] GetInformationByOperations(FilterOperation filter)
        {
            return repo.GetInformationByOperations(filter);
        }

        [HttpPost]
        [Route("Analytics/Contractors")]
        public List<int[]> GetInformationsByContractors(int[] contractors)
        {
            return repo.GetInformationsByContractors(contractors);
        }
    }
}
