using JourneyToTheWest.Models;
using JourneyToTheWest.Models.ApiDTO;
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
    [RoutePrefix("api/login")]

    public class AuthController : ApiController
    {
        [Route("auth")]
        [HttpGet]
        public List<String> CheckLogin(string username, string password)
        {
            List<String> list = new List<string>();
            try
            {
                Admin ad = new AdminDAO().CheckLogin(username, password);
                if (ad != null)
                {
                    list.Add("successfull");
                    list.Add("admin");
                    list.Add(ad.Username);
                }

                Cast cast = new CastDAO().CheckLogin(username, password);
                if (cast != null)
                {
                    list.Add("successfull");
                    list.Add("cast");
                    list.Add(cast.Username);
                }

                if(list.Count == 0)
                {
                    list.Add("fail");
                }
            }
            catch (Exception)
            {

                throw;
            }

            return list;

        }
        [Route("duplicate")]
        [HttpGet]
        public IHttpActionResult CheckDuplicate(string username)
        {
            bool rs = true;
            try
            {
                Admin ad = new AdminDAO().GetAdminById(username);
                if (ad != null)
                {
                    rs = false;
                }

                Cast cast = new CastDAO().GetCastById(username);
                if (cast != null)
                {
                    rs = false;
                }
              
            }
            catch (Exception)
            {

                throw;
            }
            if (rs) return Ok();

            return Conflict();

        }
        [Route("getById/{username}")]
        [HttpGet]
        [ResponseType(typeof(Admin))]

        public IHttpActionResult GetAdminById(string username)
        {
            Admin cast = new AdminDAO().GetAdminById(username);
            if (cast != null) return Ok(cast);

            return Conflict();

        }

    }
}
