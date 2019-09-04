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
    public class CompaniesController : ApiController {
        // GET api/cities
        public IHttpActionResult Get() {
            string vResult = string.Empty;
            ICompanyData vCompanyData = new CompanyData();
            vResult = vCompanyData.SelectAll();

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.OK, Message = Messages.vOkListed, Result = vResult }));
        }

        // GET api/cities/5
        public IHttpActionResult Get(Guid  id) {
            string vResult = string.Empty;
            ICompanyData vCompanyData = new CompanyData();
            vResult = vCompanyData.SelectById(id);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.OK, Message = Messages.vOkInserted, Result = vResult }));

        }

        // POST api/cities
        public IHttpActionResult Post([FromBody]Company valCompany) {
            bool vResult = false;
            ICompanyData vCompanyData = new CompanyData();
            try {
                vResult = vCompanyData.Insert(valCompany);
                if (!vResult) {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.OK, Message = Messages.vOkInserted, Result = "false" }));
                }
            } catch (Exception vEx) {
                string vMessage = vEx.Message;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Code = HttpStatusCode.InternalServerError, Message = vMessage, Result = "false" }));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { Code = HttpStatusCode.OK, Message = Messages.vOkInserted, Result = "true" }));
        }

        // PUT api/cities/5
        public IHttpActionResult Put(Guid id, [FromBody]Company valCompany) {
            bool vResult = false;
            ICompanyData vCompanyData = new CompanyData();
            valCompany.Id = id;
            vResult = vCompanyData.Update(valCompany);

            if (!vResult) {
                return InternalServerError(new Exception(Messages.vInternalServerError));
            }
            return Ok(new { results = Messages.vOkUpdated });
        }

        // DELETE api/values/5
        public void Delete(int id) {

        }

        






    }
}
