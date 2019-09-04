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
    public class PaymentController : ApiController {

        [HttpGet]
        public IHttpActionResult Get([FromBody]PaymentSearch valPayment) {
            string vResult = "4";

            IPaymentData vPaymentData = new PaymentData();
            try {
                bool vNullField = VerifyNullFiledsSearch(valPayment);

                if (vNullField) {
                    vResult = "2";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new { Code = HttpStatusCode.NotAcceptable, Message = Messages.vListContainNullValue, Result = vResult }));
                }
               
                string vResponse = vPaymentData.SelectById(valPayment);

                if (!string.IsNullOrEmpty(vResponse)) {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.OK, Message = Messages.vOkListed, Result = vResponse }));
                    
                }

            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResult }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.OK, Message = Messages.vNotListed, Result = vResult }));
        }

        // POST: api/Payment
        public async Task<IHttpActionResult> Post([FromBody]Payment valPayment) {
            string vResult = "4";

            IPaymentData vPaymentData = new PaymentData();
            try {
                bool vNullField = VerifyNullFileds(valPayment);

                if (vNullField) {
                    vResult = "2";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new { Code = HttpStatusCode.NotAcceptable, Message = Messages.vListContainNullValue, Result = vResult }));
                }
                string vResponse = await vPaymentData.Insert(valPayment);
                if (!string.IsNullOrEmpty(vResponse)) {
                    if (vResponse == "1") {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.Created, new { Code = HttpStatusCode.Created, Message = Messages.vOkInserted, Result = vResponse }));
                    } else if (vResponse == "3") {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Code = HttpStatusCode.InternalServerError, Message = Messages.vInsertPaymentError, Result = vResponse }));
                    } else if (vResponse != "4") {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Code = HttpStatusCode.InternalServerError, Message = Messages.vPaymentGatewayError, Result = vResponse }));
                    }
                }

            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResult }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Code = HttpStatusCode.BadRequest, Message = Messages.vNotInserted, Result = vResult }));
        }

        private bool VerifyNullFileds(Payment valPayment) {
            bool vResult = false;
            if (string.IsNullOrEmpty(valPayment.FirstName.Trim())) {
                vResult = true;
                return vResult;
            }
            if (string.IsNullOrEmpty(valPayment.LastName.Trim())) {
                vResult = true;
                return vResult;
            }
            if (string.IsNullOrEmpty(valPayment.Email.Trim())) {
                vResult = true;
                return vResult;
            }
            if (string.IsNullOrEmpty(valPayment.Phone.Trim())) {
                vResult = true;
                return vResult;
            }
            if (string.IsNullOrEmpty(valPayment.NumberDocument.Trim())) {
                vResult = true;
                return vResult;
            }
            if (string.IsNullOrEmpty(valPayment.TypeNumberDocument.Trim())) {
                vResult = true;
                return vResult;
            }
            if (valPayment.IdActivity == Guid.Empty) {
                vResult = true;
                return vResult;
            }
            if (valPayment.IdCurrency == Guid.Empty) {
                vResult = true;
                return vResult;
            }
            if (valPayment.IdUser == Guid.Empty) {
                vResult = true;
                return vResult;
            }
            if (valPayment.Id == Guid.Empty) {
                vResult = true;
                return vResult;
            }
            return vResult;
        }

        private bool VerifyNullFiledsSearch(PaymentSearch valPayment) {
            bool vResult = false;
            if (valPayment.IdLanguage== Guid.Empty) {
                vResult = true;
                return vResult;
            }
            if (valPayment.IdPayment == Guid.Empty) {
                vResult = true;
                return vResult;
            }
            
            return vResult;
        }

        [HttpGet]
        public IHttpActionResult ByUser([FromBody]PaymentUserSearch valPayment) {
            string vResult = "4";

            IPaymentData vPaymentData = new PaymentData();
            try {
                bool vNullField = VerifyNullFiledsUserSearch(valPayment);

                if (vNullField) {
                    vResult = "2";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new { Code = HttpStatusCode.NotAcceptable, Message = Messages.vListContainNullValue, Result = vResult }));
                }

                string vResponse = vPaymentData.SelectByUser(valPayment);

                if (!string.IsNullOrEmpty(vResponse)) {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.OK, Message = Messages.vOkListed, Result = vResponse }));

                }

            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResult }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.OK, Message = Messages.vNotInserted, Result = vResult }));
        }

        private bool VerifyNullFiledsUserSearch(PaymentUserSearch valPayment) {
            bool vResult = false;
            if (valPayment.IdLanguage == Guid.Empty) {
                vResult = true;
                return vResult;
            }
            if (valPayment.IdUser == Guid.Empty) {
                vResult = true;
                return vResult;
            }

            return vResult;
        }

        [HttpPost]
        public IHttpActionResult UpdateRanking([FromBody]Payment valPayment) {
            string vResult = "4";

            IPaymentData vPaymentData = new PaymentData();
            try {
                bool vNullField = VerifyNullFiledsRanking(valPayment);

                if (vNullField) {
                    vResult = "2";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new { Code = HttpStatusCode.NotAcceptable, Message = Messages.vListContainNullValue, Result = vResult }));
                }
                
                string vResponse = vPaymentData.UpdateRanking(valPayment);

                if (!string.IsNullOrEmpty(vResponse)) {
                    if (vResponse == "1") {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.Created, new { Code = HttpStatusCode.Created, Message = Messages.vOkInserted, Result = vResponse }));
                    }
                }

            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResult }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Code = HttpStatusCode.BadRequest, Message = Messages.vNotInserted, Result = vResult }));
        }

        private bool VerifyNullFiledsRanking(Payment valPayment) {
            bool vResult = false;
            if (valPayment.Id == Guid.Empty) {
                vResult = true;
                return vResult;
            }
            if (valPayment.IdActivity == Guid.Empty) {
                vResult = true;
                return vResult;
            }

            return vResult;
        }

        [HttpPost]
        public async Task<IHttpActionResult> CancelAndRefund([FromBody]Payment valPayment) {
            string vResult = "4";

            IPaymentData vPaymentData = new PaymentData();
            try {
                bool vNullField = VerifyNullFiledsCancelAndRefund(valPayment);
                if (vNullField) {
                    vResult = "2";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new { Code = HttpStatusCode.NotAcceptable, Message = Messages.vListContainNullValue, Result = vResult }));
                }
                string vResponse = await vPaymentData.CancelAndRefund(valPayment);
                if (!string.IsNullOrEmpty(vResponse)) {
                    if (vResponse == "1") {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.OK, Message = Messages.vOkUpdated, Result = vResponse }));
                    } else if (vResponse == "3") {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Code = HttpStatusCode.InternalServerError, Message = Messages.vCancelPaymentError, Result = vResponse }));
                    } else if (vResponse != "4") {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Code = HttpStatusCode.InternalServerError, Message = Messages.vPaymentGatewayError, Result = vResponse }));
                    }
                }

            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResult }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Code = HttpStatusCode.BadRequest, Message = Messages.vNotInserted, Result = vResult }));

        }
        private bool VerifyNullFiledsCancelAndRefund(Payment valPayment) {
            bool vResult = false;
            if (valPayment.Id == Guid.Empty) {
                vResult = true;
                return vResult;
            }
            if (string.IsNullOrEmpty(valPayment.GatewayJsonData.Trim())) {
                vResult = true;
                return vResult;
            }
           
            return vResult;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Cancel([FromBody]Payment valPayment) {
            string vResult = "4";

            IPaymentData vPaymentData = new PaymentData();
            try {
                bool vNullField = VerifyNullFileds(valPayment);

                if (vNullField) {
                    vResult = "2";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new { Code = HttpStatusCode.NotAcceptable, Message = Messages.vListContainNullValue, Result = vResult }));
                }
                string vResponse = await vPaymentData.Insert(valPayment);
                if (!string.IsNullOrEmpty(vResponse)) {
                    if (vResponse == "1") {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.Created, new { Code = HttpStatusCode.Created, Message = Messages.vOkInserted, Result = vResponse }));
                    } else if (vResponse == "3") {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Code = HttpStatusCode.InternalServerError, Message = Messages.vInsertPaymentError, Result = vResponse }));
                    } else if (vResponse != "4") {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Code = HttpStatusCode.InternalServerError, Message = Messages.vPaymentGatewayError, Result = vResponse }));
                    }
                }

            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Code = HttpStatusCode.InternalServerError, Message = Messages.vInternalServerError, Result = vResult }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Code = HttpStatusCode.BadRequest, Message = Messages.vNotInserted, Result = vResult }));

        }

    }
}
