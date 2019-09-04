using GoTour.DataAccess.Implementation;
using GoTour.DataAccess.Interfaces;
using GoTour.Helper;
using GoTour.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GoTour.Controllers {
    public class GeneralSearchController : ApiController {


        // GET: api/GeneralSearch/5
        [HttpGet]
        public IHttpActionResult Get([FromBody]GeneralSearch valSearchText) {
            string vResult = "4";

            IGeneralSearchData vSearchData = new GeneralSearchData();

            try 
                {
                bool vNullField = VerifyNullFiledsSearch(valSearchText);
                if (vNullField) {
                    vResult = "4";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new {
                        code = HttpStatusCode.NotAcceptable,
                        Message = Messages.vNotListed,
                        Result = vResult
                    }));
                }
                string vResponse = vSearchData.SearchText(valSearchText);

                if (!string.IsNullOrEmpty(vResponse)) {
                    vResult = "2";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new {
                        code = HttpStatusCode.OK,
                        Message = Messages.vOkListed,
                        Result = vResponse
                    }));
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new {
                    Code = HttpStatusCode.InternalServerError,
                    Message = Messages.vInternalServerError,
                    Result = vResult
                }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new {
                Code = HttpStatusCode.OK,
                Message = Messages.vNotListed,
                Result = vResult
            }));

        }

        // POST: api/GeneralSearch
        public void Post([FromBody]string value) {
        }

        // PUT: api/GeneralSearch/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE: api/GeneralSearch/5
        public void Delete(int id) {
        }


        //verifica si los datos que trae del frontend no sean nulos
        private bool VerifyNullFiledsSearch(GeneralSearch valSearchText) {
            bool vResult = false;
            if (valSearchText.IdLanguage == Guid.Empty) {
                vResult = true;
                return vResult;
            }
            if (string.IsNullOrEmpty(valSearchText.SearchText)) {
                vResult = true;
                return vResult;
            }

            return vResult;
        }


    }
}
