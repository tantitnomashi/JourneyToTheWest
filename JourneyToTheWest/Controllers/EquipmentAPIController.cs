using JourneyToTheWest.Models;
using JourneyToTheWest.Models.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace JourneyToTheWest.Controllers
{
    [RoutePrefix("api/equipment")]
    public class EquipmentAPIController : ApiController
    {
        [Route("getAllEquipments")]

        [HttpGet]
        public List<Equipment> GetAllEquipments()
        {
            return new EquipmentDAO().GetAllEquipment();
        }
        [Route("getEquipmentById")]

        [HttpGet]
        public Equipment GetEquipmentById(string id)
        {
            return new EquipmentDAO().GetEquipmentById(int.Parse(id));
        }


        [Route("addNew")]
        [HttpPost]
        public HttpResponseMessage AddNew(Equipment equipment)
        {

              var rs =   new EquipmentDAO().AddNewEquipment(equipment);
             return rs == true ?  new HttpResponseMessage(HttpStatusCode.OK) : new HttpResponseMessage(HttpStatusCode.BadRequest);           
        } 
        
        [Route("update")]
        [HttpPost]
        public HttpResponseMessage Update(Equipment equipment)
        {

              var rs =   new EquipmentDAO().UpdateEquipment(equipment);
              return rs == true ?  new HttpResponseMessage(HttpStatusCode.OK) : new HttpResponseMessage(HttpStatusCode.BadRequest);           
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
              var rs = new EquipmentDAO().DeleteEquiptment(tmp);
              return rs == true ?  new HttpResponseMessage(HttpStatusCode.OK) : new HttpResponseMessage(HttpStatusCode.BadRequest);           
        }



        [Route("upload")]
        [HttpPost]
        public HttpResponseMessage Upload()
        {
            try
            {
                var request = HttpContext.Current.Request;
                var description = request.Form["description"];
                var photo = request.Files["photo"];
                photo.SaveAs(HttpContext.Current.Server.MapPath("~/Content/Uploads/" + photo.FileName));
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

                throw;
            }
        }
    }
}
