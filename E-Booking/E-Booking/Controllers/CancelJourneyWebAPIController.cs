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


namespace EBooking.Controllers
{
    public class CancelJourneyWebAPIController : ApiController
    {
        Journey journeyObj = new Journey();
        // GET api/canceljourneywebapi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/canceljourneywebapi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/canceljourneywebapi
        public dynamic Post(List<string> data)
        {
            //Check Forgery Token
            if (!Common.IsValidAntiForgeryToken(Request))
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized attempt to E-Booking server.");

            //Check if model is valid
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data to E-Booking server.");

            return journeyObj.Deletejourney(data[0], data[1], Convert.ToInt32(User.Identity.Name),data[3]=="0"?false:true);
        }

        // PUT api/canceljourneywebapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/canceljourneywebapi/5
        public void Delete(int id)
        {
        }
    }
}
