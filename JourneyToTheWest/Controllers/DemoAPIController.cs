using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JourneyToTheWest.Controllers
{
    [RoutePrefix("api/demo")]

    public class DemoAPIController : ApiController
    {
        [Route("getAll")]
        [HttpGet]
        public string GetAllCasts()
        {
            return "values";
        }
    }
}
