using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EBooking.Models;
using BusinessRules;
using EBooking.Utilities;
using System.Data;

namespace EBooking.Controllers
{
    public class SignupWebAPIController : ApiController
    {
        // GET api/signupwebapi
        public dynamic Get()
        {
            return true;
        }

        // GET api/signupwebapi/5
        [HttpGet]
        public dynamic Get(int id)
        {
            //if (!Common.IsValidAntiForgeryToken(Request))
            //    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized attempt to E-Booking server.");

            //Check if model is valid
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data to E-Booking server.");

            clsEBooking objEBooking = new clsEBooking();
            DataSet ds = objEBooking.GetUserDetails(id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                SignupModel _signupmodel = new SignupModel();
                _signupmodel.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["UserID"].ToString().Trim());
                _signupmodel.Username = ds.Tables[0].Rows[0]["Login"].ToString().Trim();
                _signupmodel.Password = ds.Tables[0].Rows[0]["Password"].ToString().Trim();
                _signupmodel.ConfirmPassword = ds.Tables[0].Rows[0]["Password"].ToString().Trim();
                _signupmodel.GenderTitleId = Convert.ToInt32(ds.Tables[0].Rows[0]["GenderID"].ToString().Trim());
                _signupmodel.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString().Trim();
                _signupmodel.Surname = ds.Tables[0].Rows[0]["SurName"].ToString().Trim();
                _signupmodel.DepartmentId =  Convert.ToInt32(ds.Tables[0].Rows[0]["DepartmentID"].ToString().Trim());
                _signupmodel.PostalAddress = ds.Tables[0].Rows[0]["PostalAddress"].ToString().Trim();
                _signupmodel.ServiceTypeId = Convert.ToInt32(ds.Tables[0].Rows[0]["ServiceID"].ToString().Trim());
                _signupmodel.EmailAddress = ds.Tables[0].Rows[0]["Email"].ToString().Trim();
                _signupmodel.ConfirmEmailAddress = ds.Tables[0].Rows[0]["Email"].ToString().Trim();
                _signupmodel.ContactTelNo = ds.Tables[0].Rows[0]["TelephoneNo"].ToString().Trim(); ;// ds.Tables[0].Rows[0]["Login"].ToString().Trim();
                _signupmodel.Position = Convert.ToInt32(ds.Tables[0].Rows[0]["PositionID"].ToString().Trim());
                _signupmodel.FacilityId = Convert.ToInt32(ds.Tables[0].Rows[0]["FacilityId"].ToString().Trim());
                _signupmodel.facilityTypeID = Convert.ToInt16(ds.Tables[0].Rows[0]["FacilityTypeID"].ToString().Trim());
                _signupmodel.Extension = ds.Tables[0].Rows[0]["Extension"].ToString().Trim();
                return _signupmodel;
            }
            return true;
        }

        // POST api/signupwebapi
        public void Post([FromBody]string value)
        {
        }

        // PUT api/signupwebapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/signupwebapi/5
        public void Delete(int id)
        {
        }
    }
}
