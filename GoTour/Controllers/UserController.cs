using GoTour.DataAccess.Implementation;
using GoTour.DataAccess.Interfaces;
using GoTour.Helper;
using GoTour.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GoTour.Controllers {
    public class UserController : ApiController {
        // GET: api/Account
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }
        #region Selects
        // GET: api/Account/5
        public IHttpActionResult Get(Guid id) {
            string vResponse = "4";
            IUserData vUserData = new UserData();
            
            try {
                vResponse = vUserData.SelectUserWebById(id);
                if (!string.IsNullOrEmpty(vResponse) && vResponse != "3" && vResponse != "4") {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { code = HttpStatusCode.OK, Message = Messages.vOkListed, Result = vResponse }));
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResponse }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new { code = HttpStatusCode.NotAcceptable, Message = Messages.vNotListed, Result = vResponse }));
        }

        [HttpGet]
        public IHttpActionResult SelectUserAll() {
            string vResponse = "4";
            IUserData vUserData = new UserData();
            try {
                vResponse = vUserData.SelectUserAll();
                if (!string.IsNullOrEmpty(vResponse) && vResponse!="3" && vResponse!="4") {
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
        // POST: api/Account
        public IHttpActionResult Post([FromBody]User valUser) {
            string vResult = "4";

            IUserData vUserData = new UserData();
            try {
                bool vNullField= VerifyNullFileds(valUser);
                
                if (vNullField) {
                    vResult = "2";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new { Code = HttpStatusCode.NotAcceptable, Message = Messages.vListContainNullValue, Result = vResult }));
                }
                valUser.State = GlobalValues.vDefaultValueState;
                valUser.TypeNumberDocument = GlobalValues.vTypeDocumentDefault;
                string vResponse = vUserData.Insert(valUser);
               
                if (!string.IsNullOrEmpty(vResponse)) {
                    if (vResponse == "1") {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.Created, new { Code = HttpStatusCode.Created, Message = Messages.vOkInserted, Result = vResponse }));
                    } else if (vResponse == "3") {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Code = HttpStatusCode.InternalServerError, Message = Messages.vEmailDuplicated, Result = vResponse }));
                    }
                }

            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResult }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Code = HttpStatusCode.BadRequest, Message = Messages.vNotInserted, Result = vResult}));
        }

        #endregion

        #region Updates
        [HttpPost]
        public IHttpActionResult UpdateUserWeb([FromBody]User valUser) {
            string vResponse = "4";
            IUserData vUserData = new UserData();
            try {
                bool vVerifyNull = VerifyNullData(valUser);
                if (!vVerifyNull) {
                    vResponse = vUserData.UpdateUser(valUser);
                    if (!string.IsNullOrEmpty(vResponse) && vResponse == "1") {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { code = HttpStatusCode.OK, Message = Messages.vOkUpdated, Result = vResponse }));
                    }
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResponse }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new { code = HttpStatusCode.NotAcceptable, Message = Messages.vNotUpdate, Result = vResponse }));
        }

        [HttpPost]
        public IHttpActionResult Put([FromBody]User valState) {
            string vResponse = "4";
            IUserData vUserData = new UserData();
            try {
                int vState = VerifyStateUser(valState.State);
                if (vState != 0) {
                    vResponse = vUserData.UpdateStateUser(valState.Id,vState);
                    if (!string.IsNullOrEmpty(vResponse) && vResponse == "1") {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { code = HttpStatusCode.OK, Message = Messages.vOkUpdated, Result = vResponse }));
                    }
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResponse }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new { code = HttpStatusCode.NotAcceptable, Message = Messages.vNotUpdate, Result = vResponse }));

        }

        #endregion


        #region Common

        private bool VerifyNullFileds(User valUser) {
            bool vResult = false;
            if (string.IsNullOrEmpty(valUser.UserName)) {
                vResult = true;
                return vResult;
            }
            if (string.IsNullOrEmpty(valUser.Password)) {
                vResult = true;
                return vResult;
            }
            if (string.IsNullOrEmpty(valUser.FirstName)) {
                vResult = true;
                return vResult;
            }
            if (string.IsNullOrEmpty(valUser.FirstLastName)) {
                vResult = true;
                return vResult;
            }
            if (string.IsNullOrEmpty(valUser.Email)) {
                vResult = true;
                return vResult;
            }

            return vResult;
        }

        public int VerifyStateUser(int valState) {
            int vResult = 0;
            if (valState==1) {
                return vResult;
            }
            if (valState==0) {
                vResult = 1;
                return vResult;
            }
            return vResult;
        }

        public bool VerifyNullData(User valUser) {
            bool vResult = false;
            if (valUser.Password==string.Empty) {
                vResult = true;
                return vResult;
            }
            if (valUser.FirstName==string.Empty) {
                vResult = true;
                return vResult;
            }
            if (valUser.FirstLastName==string.Empty) {
                vResult = true;
                return vResult;
            }
            if (valUser.Id==Guid.Empty) {
                vResult = true;
                return vResult;
            }
            return vResult;
        }

        #endregion

    }
}