using JourneyToTheWest.Models;
using JourneyToTheWest.Models.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JourneyToTheWest.Controllers
{
    [RoutePrefix("api/cast")]
    public class CastAPIController : ApiController
    {

        [Route("getAll")]
        [HttpGet]
        public List<Cast> GetAllCasts()
        {
            return new CastDAO().GetAllCast();
        }
        [Route("getById")]

        [HttpGet]
        public Cast GetCastById(string username)
        {
            return new CastDAO().GetCastById(username);
        }
        [Route("addNew")]
        [HttpPost]
        public HttpResponseMessage AddNew(Cast cast)
        {

            var rs = new CastDAO().AddNewCast(cast);
            return rs == true ? new HttpResponseMessage(HttpStatusCode.OK) : new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [Route("update")]
        [HttpPost]
        public HttpResponseMessage Update(Cast cast)
        {

            var rs = new CastDAO().UpdateCast(cast);
            return rs == true ? new HttpResponseMessage(HttpStatusCode.OK) : new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
        [Route("delete")]
        [HttpPost]
        public HttpResponseMessage Delete(string username)
        {
          
            var rs = new CastDAO().DeleteCast(username);
            return rs == true ? new HttpResponseMessage(HttpStatusCode.OK) : new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
        

    }
}
