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
    }
}
