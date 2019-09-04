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
    public class ActivityController : ApiController {
        // GET: api/Tour
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        

        // POST: api/Tour
        public IHttpActionResult Post([FromBody]ActivitySearch valFilter) {
            string vResult = "3";
            IActivityData vActivityData = new ActivityData();
            try {
                bool vNullField = VerifyNullFiledsSearch(valFilter);

                if (vNullField) {
                    vResult = "1";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.NotAcceptable, Message = Messages.vListContainNullValue, Result = vResult }));
                }
                string vResponse = vActivityData.SelectByTour(valFilter);

                if (!string.IsNullOrEmpty(vResponse)) {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.OK, Message = Messages.vOkListed, Result = vResponse }));
                }
                vResult = "2";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.NoContent, Message = Messages.vNotListed, Result = vResult }));
            } catch (Exception) {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Code = HttpStatusCode.BadRequest, Message = Messages.vInternalServerError, Result = vResult }));
            }
        }

        // PUT: api/Tour/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE: api/Tour/5
        public void Delete(int id) {
        }

        private bool VerifyNullFiledsSearch(ActivitySearch valActivitySearch) {
            bool vResult = false;
            if (valActivitySearch.IdTour == Guid.Empty || valActivitySearch.IdTour == null) {
                vResult = true;
                return vResult;
            }
            if (valActivitySearch.IdLanguage == Guid.Empty || valActivitySearch.IdLanguage == null) {
                vResult = true;
                return vResult;
            }
            if (valActivitySearch.IdCurrency == Guid.Empty || valActivitySearch.IdCurrency == null) {
                vResult = true;
                return vResult;
            }
            return vResult;
        }

        
    }
}