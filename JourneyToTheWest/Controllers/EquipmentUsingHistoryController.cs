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
    [RoutePrefix("api/equipmenthistory")]

    public class EquipmentUsingHistoryController : ApiController
    {
        [Route("getBySceneId")]
        [HttpGet]
        public List<EquipmentUsingHistory> GetImpersonationById(int id)
        {
            return new EquipmentUsingHistoryDAO().GetEquipmentBySceneId(id);
        }
        [Route("getByDate2")]
        [HttpGet]
        public List<EquipmentUsingHistory> GetByDate(DateTime dayFrom, DateTime dayTo)
        {
            return new EquipmentUsingHistoryDAO().GetByDate(dayFrom, dayTo);
        }


        [Route("getByDate")]
        [HttpGet]
        [ResponseType(typeof(List<EquipmentUsingHistory>))]
        public IHttpActionResult GetByDate(string dayFrom, string dayTo)
        {
            List <EquipmentUsingHistory>  list = new EquipmentUsingHistoryDAO().GetByDate(DateTime.Parse(dayFrom) , DateTime.Parse(dayTo));
           // List <EquipmentUsingHistory>  list = new EquipmentUsingHistoryDAO().GetAll();

            var rs = (list != null);

            if (!rs) return Conflict();
            return Ok(list);
        }
       


        [Route("addNew")]
        [HttpPost]
        public HttpResponseMessage AddNew(EquipmentUsingHistory rc)
        {
            var rs = new EquipmentUsingHistoryDAO().AddNew(rc);
            return rs == true ? new HttpResponseMessage(HttpStatusCode.OK) : new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
        [Route("update")]
        [HttpPost]
        public HttpResponseMessage Update(EquipmentUsingHistory rc)
        {
            var rs = new EquipmentUsingHistoryDAO().Update(rc);
            return rs == true ? new HttpResponseMessage(HttpStatusCode.OK) : new HttpResponseMessage(HttpStatusCode.BadRequest);
        }



        [HttpPost]
        [Route("delete/{sceneId}/{equipId}")]

        public IHttpActionResult Delete([FromUri] int sceneId, [FromUri] int equipId)
        {
            var rs = new EquipmentUsingHistoryDAO().Delete(sceneId, equipId);

            if (rs) return Ok();

            return Conflict();
        }








    }
}
