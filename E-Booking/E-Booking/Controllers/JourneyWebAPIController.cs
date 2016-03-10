using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EBooking.Models;
using EBooking.Utilities;
using BusinessRules;
using System.Data.SqlClient;
using System.Text;
//using System.Web.Mvc;

namespace EBooking.Controllers
{
    [Authorize]    
    public class JourneyWebAPIController : ApiController
    {
        private string refNumber = String.Empty;
        private string BookingNumber = String.Empty;
        Journey journeyObj = new Journey();

        #region Default WebAPI Methods

        // POST api/<controller>
        
        public dynamic Post([FromBody]CompositeModel journey)
        {
            //Check Forgery Token
            if (!Common.IsValidAntiForgeryToken(Request))
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized attempt to E-Booking server Post.");

            //if (journey.Patient.WeighingDate.Year == 1)
            //{
            //    journey.Patient.WeighingDate = DateTime.Now;
            //}
            //Check if model is valid
            if (!ModelState.IsValid)
            {
                string validationErrors = string.Join(",",
                      ModelState.Values.Where(E => E.Errors.Count > 0)
                      .SelectMany(E => E.Errors)
                      .Select(E => E.ErrorMessage)
                      .ToArray());
                Common.ErrorLog(validationErrors);
                Common.LogInvalidBookingRequests(journey);
                StringBuilder sb = new StringBuilder();
                sb.Append("<ul>");
                string[] _errorArray=validationErrors.Length>1?validationErrors.Substring(1).Split(new char[] { ',' }):new string[]{"Unknown Error"};
                for (int i = 0; i < _errorArray.Length; i++)
                {
                    sb.Append("<li>");
                    sb.Append(_errorArray[i]);
                    sb.Append("</li>");
                }
                sb.Append("</ul>");
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "<br />Please Correct following error(s).<br />" + sb.ToString());
            }

          //  var result=0;
            try
            {
                journeyObj.CreateJourney(journey, Convert.ToInt16(User.Identity.Name), out refNumber, out BookingNumber);

                var result = new { refNum = refNumber, BookingNo = BookingNumber };
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ee)
            {
                var result = ee.ToString();
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            }

            
            //return Json(result, JsonRequestBehavior.AllowGet);
           // return Request.CreateResponse(HttpStatusCode.OK, result);

           // return Request.CreateResponse(HttpStatusCode.OK, journeyObj.CreateJourney(journey,Convert.ToInt16(User.Identity.Name)));
        }
        
        public dynamic Get(string referenceNumber)
        {
            //Check Forgery Token
            //string currentUsername = AntiForgeryData.GetUsername(context.User);
            
            if (!Common.IsValidAntiForgeryToken(Request))
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized attempt to E-Booking server.");


            //Check if model is valid
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data to E-Booking server.");

            return journeyObj.GetJourney(referenceNumber);
        }

        public dynamic GetList(string qdateFrom, string qdateTo)
        {
            //Check Forgery Token
            if (!Common.IsValidAntiForgeryToken(Request))
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized attempt to E-Booking server.");

            //Check if model is valid
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data to E-Booking server.");

            return journeyObj.GetJourneyListFiltered("1,2,3,4,5", qdateFrom, Convert.ToDateTime(qdateTo).AddDays(1).ToString("dd MMM yyyy"), "");
        }
        [AcceptVerbs("GET")]
        public dynamic SearchList(string refNumber)
        {
            //Check Forgery Token
            if (!Common.IsValidAntiForgeryToken(Request))
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized attempt to E-Booking server.");

            //Check if model is valid
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data to E-Booking server.");

            return journeyObj.SearchJourneyByRefNumber(refNumber);
        }

        [AcceptVerbs("GET")]
        public dynamic SearchNHS(string nhsNumber)
        {
            if (!Common.IsValidAntiForgeryToken(Request))
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized attempt to E-Booking server.");

            //Check if model is valid
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data to E-Booking server.");

            return journeyObj.SearchPatientbyNHSnumber(nhsNumber);
        }

        //[HttpPost]
        //[ActionName("CancelJourney")]
        //public dynamic CancelJourney(string refNumber, string cancelReason)
        //{
        //    //Check Forgery Token
        //    if (!Common.IsValidAntiForgeryToken(Request))
        //        return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized attempt to E-Booking server.");

        //    //Check if model is valid
        //    if (!ModelState.IsValid)
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data to E-Booking server.");
            
        //    return journeyObj.Deletejourney(refNumber);
        //}
        public dynamic GetListjourneyDate(string qdateFrom, string qdateTo)
        {
            //Check Forgery Token
            if (!Common.IsValidAntiForgeryToken(Request))
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized attempt to E-Booking server.");

            //Check if model is valid
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data to E-Booking server.");

            return journeyObj.GetJourneyListFilteredJourneyDate("1,2,3,4,5", qdateFrom, qdateTo , "");//Convert.ToDateTime(qdateTo).AddDays(1).ToString("dd MMM yyyy")
        }
        #endregion
    }
}