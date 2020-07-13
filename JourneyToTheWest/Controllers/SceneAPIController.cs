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
        [Route("delete")]
        [HttpPost]
        public HttpResponseMessage Delete(string id)
        {
            int tmp;
            try
            {
                tmp = int.Parse(id);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
                throw;
            }
            var rs = new SceneDAO().DeleteScene(tmp);
            return rs == true ? new HttpResponseMessage(HttpStatusCode.OK) : new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}
