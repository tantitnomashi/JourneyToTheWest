using JourneyToTheWest.Models;
using JourneyToTheWest.Models.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace JourneyToTheWest.Controllers
{
    [RoutePrefix("api/cast")]
    public class CastAPIController : ApiController
    {

        [Route("getAll")]
        [HttpGet]
        [ResponseType(typeof(List<Cast>))]
        public IHttpActionResult GetAllCasts()
        {
            var list = new CastDAO().GetAllCast();

            return Ok(new CastDAO().GetAllCast());
        }


        [Route("getById/{username}")]
        [HttpGet]
        [ResponseType(typeof(Cast))]

        public IHttpActionResult GetCastById(string username)
        {
            Cast cast = new CastDAO().GetCastById(username);
            if (cast != null) return Ok(cast);

            return Conflict();
            
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
       
        [HttpPost]
        [Route("{username}")]

        public IHttpActionResult Delete([FromUri] string username)
        {


           var  rs = new CastDAO().DeleteCast(username);
            if (rs) return Ok();

            return Conflict();
        }


    }
}
