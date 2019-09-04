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
    public class TourController : ApiController {
        // GET: api/Tour
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Tour/5
        public IHttpActionResult Get(Guid id) {

            string vResult = "3";
            ITourData vTourData = new TourData();
                if (VerifyNullGuid(id)) {
                    vResult = "1";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new { Code = HttpStatusCode.NotAcceptable, Message = Messages.vListContainNullValue, Result = vResult }));
                }
                string vResponse = vTourData.OrderByRanking(id);

                if (string.IsNullOrEmpty(vResponse)) {
                    vResult = "2";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.NoContent, Message = Messages.vNotListed, Result = vResult }));
                }
            
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.OK, Message = Messages.vOkListed, Result = vResponse }));
        }

        // POST: api/Tour
        public IHttpActionResult Post([FromBody]TourSearch valFilter) {
            string vResult = "3";
            ITourData vTourData = new TourData();
            try {
                bool vNullField = VerifyNullFiledsSearch(valFilter);

                if (vNullField) {
                    vResult = "1";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.NotAcceptable, Message = Messages.vListContainNullValue, Result = vResult }));
                }
                string vResponse = vTourData.SelectByCity(valFilter);

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

        private bool VerifyNullFiledsSearch(TourSearch valTourSearch) {
            bool vResult = false;
            if (valTourSearch.IdCity == Guid.Empty || valTourSearch.IdCity == null) {
                vResult = true;
                return vResult;
            }
            if (valTourSearch.IdLanguage == Guid.Empty || valTourSearch.IdLanguage == null) {
                vResult = true;
                return vResult;
            }
            if (valTourSearch.IdCurrency == Guid.Empty || valTourSearch.IdCurrency == null) {
                vResult = true;
                return vResult;
            }
            return vResult;
        }

        private bool VerifyNullGuid(Guid valIdLang) {
            bool vResult = false;

            if (valIdLang == Guid.Empty || valIdLang == null) {
                vResult = true;
            }

            return vResult;
        }
    }
}