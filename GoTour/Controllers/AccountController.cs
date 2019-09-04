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
    public class AccountController : ApiController {
        // GET: api/Account
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Account/5
        public string Get(int id) {
            

            return "VALUE";

        }

        // POST: api/Account
        public IHttpActionResult Post([FromBody]User vUser) {
            bool vResult = false;

            IAccountData vAccountData = new AccountData();
            try {
                string vUserName = vUser.UserName;
                string vPassword = vUser.Password;
                string vResponse = string.Empty;
                vResponse = vAccountData.Verify(vUserName, vPassword);
               
                if (!string.IsNullOrEmpty(vResponse)) {
                    vResult = true;
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.OK, Message = Messages.vLoginSuccessfully, Result = vResponse }));
                }

            } catch (Exception) {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResult.ToString() }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.OK, Message = Messages.vLoginUnsuccessfully, Result = vResult.ToString() }));
        }

        // PUT: api/Account/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE: api/Account/5
        public void Delete(int id) {
        }
    }
}