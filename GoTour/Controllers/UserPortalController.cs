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

namespace GoTour.Controllers
{
    public class UserPortalController : ApiController
    {

        #region Inserts
        // POST: api/UserPortal
        public async Task<IHttpActionResult> Post([FromBody]UserPortalAdmin valUser) {
            string vResponse = "4";
            IUserPortalAdminData vUserData = new UserPortalAdminData();
            try {
                bool vVerifyIsNull = VerifyNullFielUserPortal(valUser);
                if (vVerifyIsNull) {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.NotAcceptable, Message = Messages.vNotInserted, Result = vResponse }));
                }
                vResponse = await vUserData.InsertUserPortal(valUser);
                if (!string.IsNullOrEmpty(vResponse) && vResponse == "1") {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.OK, Message = Messages.vOkInserted, Result = vResponse }));
                }
                if (!string.IsNullOrEmpty(vResponse) && vResponse == "2") {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.NotAcceptable, Message = Messages.vEmailDuplicated, Result = vResponse }));
                }
            } catch (Exception) {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResponse }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.NotAcceptable, Message = Messages.vNotInserted, Result = vResponse }));

        }
        #endregion

        #region ValidateLoginUser

        [HttpPost]
        public IHttpActionResult ValidateUserPortal([FromBody]UserPortalAdmin valUserPortal) {
            bool vResult = false;

            IUserPortalAdminData vUserData = new UserPortalAdminData();

            try {
                string vUserName = valUserPortal.UserName;
                string vPassword = valUserPortal.Password;
                string vResponse = string.Empty;
                vResponse = vUserData.VerifyUserPortal(vUserName, vPassword);
                if (!string.IsNullOrEmpty(vResponse)) {
                    vResult = true;
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.OK, Message = Messages.vLoginSuccessfully, Result = vResponse }));
                }
            } catch (Exception) {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResult.ToString() }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.NotAcceptable, Message = Messages.vLoginUnsuccessfully, Result = vResult.ToString() }));
        }
        #endregion

        #region UpdateUserPortal
        // PUT: api/UserPortal/Put
        [HttpPost]
        public IHttpActionResult Put([FromBody]UserPortalAdmin valUserPortal) {
            string vResponse = "4";
            IUserPortalAdminData vUserData = new UserPortalAdminData();
            try {
                vResponse = vUserData.UpdateUserPortal(valUserPortal);
                if (!string.IsNullOrEmpty(vResponse) && vResponse=="1") {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.OK, Message = Messages.vOkUpdated, Result = vResponse }));
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResponse }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.NotAcceptable, Message = Messages.vNotInserted, Result = vResponse }));

        }

        [HttpPost]
        public IHttpActionResult UpdateStateUserPortal([FromBody]UserPortalAdmin valUserPortal) {
            string vResponse = "4";
            IUserPortalAdminData vUserData = new UserPortalAdminData();

            try {
                vResponse = vUserData.UpdateStateUserPortal(valUserPortal);
                if (!string.IsNullOrEmpty(vResponse) && vResponse == "1") {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.OK, Message = Messages.vOkUpdated, Result = vResponse }));
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResponse }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.NotAcceptable, Message = Messages.vNotInserted, Result = vResponse }));
        }

        #endregion

        #region SelectUserPortal

        [HttpGet]
        public IHttpActionResult SelectUserPortalAll() {
            string vResult = "4";

            IUserPortalAdminData vUserDAta = new UserPortalAdminData();
            try {

                string vResponse = vUserDAta.SelectUserPortalAll();

                if (!string.IsNullOrEmpty(vResponse) && vResponse!="3" && vResponse != "4") {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.OK, Message = Messages.vOkListed, Result = vResponse }));
                }

            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResult }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new { Code = HttpStatusCode.NotAcceptable, Message = Messages.vNotInserted, Result = vResult }));
        }

        
        public IHttpActionResult Get(Guid id) {
            string vResult = "4";
            IUserPortalAdminData vUserData = new UserPortalAdminData();
            
            try {
                string vResponse = vUserData.SelectUserPortalById(id);

                if (!string.IsNullOrEmpty(vResponse) && vResponse!="3" && vResponse!="4") {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { code = HttpStatusCode.OK, Message = Messages.vOkListed, Result = vResponse }));
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResult }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new { code = HttpStatusCode.NotAcceptable, Message = Messages.vNotListed, Result = vResult }));
        }


       

        #endregion

        #region Common


        public bool VerifyNullFielUserPortal(UserPortalAdmin valUserPortalAdmin) {
            bool vResult = false;
            if (valUserPortalAdmin.UserName==string.Empty) {
                vResult = true;
                return vResult;
            }
            if (valUserPortalAdmin.Password==string.Empty) {
                vResult = true;
                return vResult;
            }
            if (valUserPortalAdmin.FirstName==string.Empty) {
                vResult = true;
                return vResult;
            }
            if (valUserPortalAdmin.FirstLastName==string.Empty) {
                vResult = true;
                return vResult;
            }
            if (valUserPortalAdmin.Phone==string.Empty) {
                vResult = true;
                return vResult;
            }
            if (valUserPortalAdmin.IdCompany==Guid.Empty) {
                vResult = true;
                return vResult;
            }
            if (valUserPortalAdmin.State>1) {
                vResult = true;
                return vResult;
            }
            
            return vResult;
        }

        #endregion



    }
}
