using JourneyToTheWest.Models;
using JourneyToTheWest.Models.ApiDTO;
using JourneyToTheWest.Models.DAOs;
using System;
using System.Collections.Generic;
using System.IO;
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
        [Route("getAll")]

        [HttpGet]
        public List<Equipment> GetAllEquipments()
        {
            return new EquipmentDAO().GetAllEquipment();
        }
        [Route("getById")]

        [HttpGet]
        public Equipment GetEquipmentById(int id)
        {
            return new EquipmentDAO().GetEquipmentById(id);
        }


        [Route("checkQuantity")]
        [HttpGet]
        public IHttpActionResult CheckQuantityAvailble(int id, int quantity)
        {
            string messError = "";
            bool rs = new EquipmentDAO().isAvailbleQuantity( id,  quantity);
            Equipment e = new EquipmentDAO().GetEquipmentById(id);
            if(e != null)
            {
                messError = " Only " + e.Quantity + " items left in stock!";
            }
            if (rs) return Ok();


            return Content(HttpStatusCode.Conflict, messError);
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
      
        [HttpPost]
        [Route("{id}")]

        public IHttpActionResult Delete([FromUri] int id)
        {

          
            var rs = new EquipmentDAO().DeleteEquiptment(id);

            if (rs) return Ok();

            return Conflict();
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
