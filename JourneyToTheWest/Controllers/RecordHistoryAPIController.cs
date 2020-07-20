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
    [RoutePrefix("api/recordhistory")]

    public class RecordHistoryAPIController : ApiController
    {
        [Route("getBySceneId")]
        [HttpGet]
        public List<RecordHistory> GetImpersonationById(int id)
        {
            return new RecordHistoryDAO().GetRecordBySceneId(id);
        }

        [Route("addNew")]
        [HttpPost]
        public HttpResponseMessage AddNew(RecordHistory rc)
        {
            var rs = new RecordHistoryDAO().AddNew(rc);
           return rs == true ? new HttpResponseMessage(HttpStatusCode.OK) : new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("{id}")]

        public IHttpActionResult Delete([FromUri] int id)
        {

            var rs = new RecordHistoryDAO().Delete(id);

            if (rs) return Ok();

            return Conflict();
        }



        [Route("getIncommingByCast/{username}")]
        [HttpGet]   
        [ResponseType(typeof(List<RecordHistory>))]
        public IHttpActionResult GetIcommingByCastUsername([FromUri] string username)
        {
           List<RecordHistory> list = new RecordHistoryDAO().GetListSceneByCast(username);

            var rs = (list != null);

            if (!rs) return Conflict();

            var res = list.Where(x => x.Scene.EndTime > DateTime.Now).ToList();

            return Ok(res);

         
        }
        [Route("getRecordedByCast/{username}")]
        [HttpGet]
        [ResponseType(typeof(List<RecordHistory>))]
        public IHttpActionResult GetRecordedByCastUsername([FromUri] string username)
        {
            List<RecordHistory> list = new RecordHistoryDAO().GetListSceneByCast(username);

            var rs = (list != null);

            if (!rs) return Conflict();
            var res = list.Where(x => x.Scene.EndTime <= DateTime.Now).ToList();

            return Ok(res);
        }



    }
}
