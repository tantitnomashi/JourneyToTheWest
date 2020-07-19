using JourneyToTheWest.Models;
using JourneyToTheWest.Models.DAOs;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JourneyToTheWest.Controllers
{
    [RoutePrefix("api/impersonation")]
    public class ImpersonationAPIController : ApiController
    {

      
        [Route("getAll")]
        [HttpGet]
        public List<Impersonation> GetAllImpersonations()
        {
            return new ImpersonationDAO().GetAllImpersonation();
        }
        [Route("getById")]

        [HttpGet]
        public Impersonation GetImpersonationById(string id)
        {
            return new ImpersonationDAO().GetImpersonationById(int.Parse(id));
        }


        [Route("addNew")]
        [HttpPost]
        public HttpResponseMessage AddNew(Impersonation impersonation)
        {
            var rs = new ImpersonationDAO().AddNewImpersonation(impersonation);
            return rs == true ? new HttpResponseMessage(HttpStatusCode.OK) : new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [Route("update")]
        [HttpPost]
        public HttpResponseMessage Update(Impersonation impersonation)
        {

            var rs = new ImpersonationDAO().UpdateImpersonation(impersonation);
            return rs == true ? new HttpResponseMessage(HttpStatusCode.OK) : new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
        
        [HttpPost]
        [Route("{id}")]

        public IHttpActionResult Delete([FromUri] int id)
        {

            var rs = new ImpersonationDAO().DeleteImpersonation(id);

            if (rs) return Ok();

            return Conflict();
        }

    }

}