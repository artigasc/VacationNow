using GoTour.DataAccess.Implementation;
using GoTour.DataAccess.Interfaces;
using GoTour.Helper;
using GoTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GoTour.Controllers {
    public class CitiesController : ApiController {

        #region Selects
        // GET api/cities
        //public IHttpActionResult Get(string id)
        //{
        //    string vResult = string.Empty;
        //    string vIdLanguage = id;
        //    ICityData vCityData = new CityData();
        //    vResult = vCityData.SelectByLanguage(vIdLanguage);
        //    if (string.IsNullOrEmpty(vResult))
        //    {
        //        return InternalServerError(new Exception(Messages.vInternalServerError));
        //    }
        //    return Ok(new { results = vResult });
        //}

        // GET api/cities/5
        public IHttpActionResult Get(Guid id) {
            string vResult = string.Empty;
            ICityData vCityData = new CityData();
            try
            {
                vResult = vCityData.SelectById(id);
                if (vResult!=null && vResult!="3") {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { code = HttpStatusCode.OK, Message = Messages.vOkListed, Result = vResult }));
                }
            }
            catch (Exception vEx){
                string vMessage = vEx.Message;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResult }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new { code = HttpStatusCode.NotAcceptable, Message = Messages.vNotListed, Result = vResult }));
        }

        [HttpGet]
        public IHttpActionResult SelectLanguageAll() {
            string vResult = string.Empty;
            ILanguageData vLanguageData = new LanguageData();
            try {
                vResult = vLanguageData.SelectAll();
                if (!string.IsNullOrEmpty(vResult)) {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { code = HttpStatusCode.OK,Message=Messages.vOkListed,Result=vResult }));
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                vResult = "3";
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Return = vResult }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new { code = HttpStatusCode.NotAcceptable, Message = Messages.vNotListed, Result = vResult }));
        }

        [HttpGet]
        public IHttpActionResult SelectCityAll() {
            string vResponse = null;
            ICityData vCityData = new CityData();
            try {
                vResponse = vCityData.SelectCityAll();
                if (!string.IsNullOrEmpty(vResponse)) {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { code = HttpStatusCode.OK, Message = Messages.vOkListed, Result = vResponse }));
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResponse }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new { code = HttpStatusCode.NotAcceptable, Message = Messages.vNotListed, Result = vResponse }));
        }


        #endregion

        #region Inserts

        public async Task<IHttpActionResult> Post([FromBody]ListCityLanguage valCity) {
            string vResult = "4";
            ICityData vCityData = new CityData();
            try {
                vResult = await vCityData.Insert(valCity);
                if (vResult == "1" && !string.IsNullOrEmpty(vResult)) {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { code = HttpStatusCode.OK, Message = Messages.vOkInserted, Result = vResult }));
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResult }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new { code = HttpStatusCode.NotAcceptable, Message = Messages.vNotInserted, Result = vResult }));
        }
        #endregion

        #region Updates

        public async Task<IHttpActionResult> Put([FromBody]ListCityLanguage valCity) {
            string vResult = "4";
            ICityData vCityData = new CityData();
            try {
                vResult = await vCityData.Update(valCity);
                if (vResult=="1" && !string.IsNullOrEmpty(vResult)) {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { code = HttpStatusCode.OK, Message = Messages.vOkInserted, Result = vResult }));
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResult }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new { code = HttpStatusCode.NotAcceptable, Message = Messages.vNotInserted, Result = vResult }));
            
        }

        public IHttpActionResult UpdateState([FromBody]City vCity) {
            string vResult = "4";
            ICityData vCityData = new CityData();
            try {
                vResult = vCityData.UpdateState(vCity);
                if (vResult == "1" && !string.IsNullOrEmpty(vResult)) {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { code = HttpStatusCode.OK, Message = Messages.vOkInserted, Result = vResult }));
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError,new { code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResult }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new { code = HttpStatusCode.NotAcceptable, Message = Messages.vNotInserted, Result = vResult }));
        }

        #endregion

        // DELETE api/values/5
        public void Delete(int id) {

        }
    }
}
