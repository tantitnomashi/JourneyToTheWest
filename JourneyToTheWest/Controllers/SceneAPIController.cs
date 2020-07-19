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
    [RoutePrefix("api/scene")]

    public class SceneAPIController : ApiController
    {
        [Route("getAll")]
        [HttpGet]
        public List<Scene> GetAll()
        {
            return new SceneDAO().GetAllScene();
        }
        [Route("getById")]

        [HttpGet]
        public Scene GetById(string id)
        {
            return new SceneDAO().GetSceneById(int.Parse(id));
        }

        [Route("addNew")]
        [HttpPost]
        public HttpResponseMessage AddNew(Scene scene)
        {
            var rs = new SceneDAO().AddNewScene(scene);
            return rs == true ? new HttpResponseMessage(HttpStatusCode.OK) : new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [Route("update")]
        [HttpPost]
        public HttpResponseMessage Update(Scene scene)
        {

            var rs = new SceneDAO().UpdateScene(scene);
            return rs == true ? new HttpResponseMessage(HttpStatusCode.OK) : new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
       
        [HttpPost]
        [Route("{id}")]

        public IHttpActionResult Delete([FromUri] int id)
        {


            var rs = new SceneDAO().DeleteScene(id);
            if (rs) return Ok();

            return Conflict();
        }
    }
}
