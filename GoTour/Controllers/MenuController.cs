using GoTour.DataAccess.Implementation;
using GoTour.DataAccess.Interfaces;
using GoTour.Helper;
using GoTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GoTour.Controllers {
    public class MenuController : ApiController {
        // GET: api/Menu
   
        public IHttpActionResult Get(string id) {
            string vResult = string.Empty;
            string vLanguage = id;
            IMenuData vMenuData = new MenuData();
            vResult = vMenuData.SelectContentMenu(vLanguage);

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.OK, Message = Messages.vOkListed, Result = vResult }));
        }

        //// GET: api/Menu/5
        //public string Get(int id) {
        //    return "value";
        //}

        // POST: api/Menu
        public void Post([FromBody]string value) {
        }

        // PUT: api/Menu/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE: api/Menu/5
        public void Delete(int id) {
        }
    }
}
