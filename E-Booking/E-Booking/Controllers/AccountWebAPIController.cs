using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EBooking.Models;
using System.Web.Security;
using EBooking.Utilities;
using WebMatrix.WebData;
using BusinessRules;
using System.Data;
using System.Globalization;
using System.Net.Mail;
using System.Configuration;
//using E_Booking.Models;

namespace EBooking.Controllers
{
    [Authorize]
    public class AccountWebAPIController : ApiController
    {
        private static int _userid = 0;
        private static int _roleid = 0;


        [AllowAnonymous]
        public dynamic LogInUser([FromBody]LoginModel model)
        {
            //Check if AntiForgeryToken is valid
            if (!Common.IsValidAntiForgeryToken(Request))
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized attempt to E-Booking server.");

            //Check if model is valid
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data to E-Booking server.");

            if (Validateuser(model.UserName, model.Password))
            {
                //WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe);
                //FormsAuthentication.SetAuthCookie(model.UserName, false);
               
                if (_roleid != 10)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "User Not Authorized");
                }

                FormsAuthentication.SetAuthCookie(_userid.ToString(), false);

                return _userid;
            }
            else
            {
                if (_userid == -1 && _roleid == -1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Invalid Username or Password.");
                }
                return Request.CreateResponse(HttpStatusCode.OK, false);
            }
        }

        private bool Validateuser(string username, string pasword)
        {
            RoleBased rb = new RoleBased();

            bool validate = rb.ValidateUserWeb(username, pasword, out _userid, out _roleid);

            return validate;
            //return true;
            //throw new NotImplementedException();
        }

        public dynamic LogOutUser()
        {
            //Check if AntiForgeryToken is valid
            if (!Common.IsValidAntiForgeryToken(Request))
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized attempt to E-Booking server.");

            //Check if model is valid
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data to E-Booking server.");

            FormsAuthentication.SignOut();

            return true;
        }

        [AllowAnonymous]
        [HttpGet]
        public dynamic ValidateUserName(string username) 
         {
            clsEBooking objEbooking = new clsEBooking();
            bool userexists = objEbooking.DoUserExists(username);
            return userexists;
        }
        
        [HttpGet]
        public dynamic GetUserDetails(string userid)
        {

            //Check if AntiForgeryToken is valid
            if (!Common.IsValidAntiForgeryToken(Request))
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized attempt to E-Booking server.");

            //Check if model is valid
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data to E-Booking server.");

            clsEBooking objEBooking = new clsEBooking();
            DataSet ds = objEBooking.GetUserDetails(Convert.ToInt32(userid));
            if (ds.Tables[0].Rows.Count > 0)
            {
                RequestertModel _requester = new RequestertModel();
                _requester.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["UserID"]);
                _requester.RequestDate = System.DateTime.Now;//.ToString("dd/MM/yyyy");//_//DateTime.Parse(System.DateTime.Now.ToString("dd/MM/yyyy"), new CultureInfo("ur-pk", false));
                _requester.GenderTitleId = Convert.ToInt32(ds.Tables[0].Rows[0]["GenderID"]);
                _requester.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                _requester.Surname = ds.Tables[0].Rows[0]["SurName"].ToString();
                _requester.StaffPosition = ds.Tables[0].Rows[0]["PositionName"].ToString();
                _requester.DepartmentId = ds.Tables[0].Rows[0]["DepartmentID"].ToString();
                _requester.ServiceTypeId = ds.Tables[0].Rows[0]["ServiceID"].ToString();
                _requester.CostCentre = "";//; ds.Tables[0].Rows[0]["SurName"].ToString();
                _requester.SubjectiveCode = "";// ds.Tables[0].Rows[0]["SurName"].ToString();
                _requester.EmailAddress = ds.Tables[0].Rows[0]["Email"].ToString();
                _requester.PostalAddress = ds.Tables[0].Rows[0]["PostalAddress"].ToString();
                _requester.FacilityId = ds.Tables[0].Rows[0]["FacilityID"].ToString().Trim();
                
                _requester.ContactTelNo = ds.Tables[0].Rows[0]["TelephoneNo"].ToString().Trim();// ds.Tables[0].Rows[0]["SurName"].ToString();
                _requester.AuthorisingClinician = "";// ds.Tables[0].Rows[0]["SurName"].ToString();
                _requester.AuthorisingRoleId = "";// ds.Tables[0].Rows[0]["SurName"].ToString();
                _requester.FacilityName = ds.Tables[0].Rows[0]["FacilityName"].ToString().Trim();
                _requester.DepartmentName = ds.Tables[0].Rows[0]["FacilityDepartmentName"].ToString().Trim();
                _requester.facilityType = ds.Tables[0].Rows[0]["FacilityTypeName"].ToString().Trim();
                _requester.Extension = ds.Tables[0].Rows[0]["Extension"].ToString().Trim();


                return _requester;



            }


            return true;
        }

        [AllowAnonymous]
        public dynamic SaveUserDetails([FromBody]SignupModel model)
        {
            //Check if AntiForgeryToken is valid
            if (!Common.IsValidAntiForgeryToken(Request))
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized attempt to E-Booking server.");

            //Check if model is valid
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data to E-Booking server.");

            clsEBooking objEBooking = new clsEBooking();
            try
            {

                objEBooking.AddorUpdateWebUser(model.FirstName, model.Username, model.Password, model.EmailAddress, model.Id, true, false, model.Position, model.GenderTitleId, model.FirstName, model.Surname, model.DepartmentId, model.PostalAddress, model.ServiceTypeId, model.ContactTelNo, model.FacilityId,model.Extension);

            }
            catch (Exception ee)
            {
                if (ee.ToString().ToLower().Contains("login already exists"))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.OK, "This Email is already registered. Please try to login, if you do not remember Password Please click on the Forgot Password link on Home Page.");
                }
                return ee.ToString();
            }
            return true;
        }
        
        [AllowAnonymous]
        public dynamic GetAddress(string FacilityId, string isFacilityDepartment)
        {
            if (!Common.IsValidAntiForgeryToken(Request))
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized attempt to E-Booking server.");

            //Check if model is valid
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data to E-Booking server.");

            try
            {

                BLFacilityAddress fa = new BLFacilityAddress();
                return fa.GetFacilityAddress(Convert.ToInt32(FacilityId), Convert.ToBoolean(isFacilityDepartment));

                //return fa.FullAddress;
                // string Address=
            }
            catch (Exception ee)
            {
                return ee.ToString();
            }

            //return true;
        }

        [AllowAnonymous]
        [HttpGet]
        public dynamic ValidateandSendEmail(string EmailAddress)
        {
            clsEBooking objEbooking = new clsEBooking();
            string _userName = "";
            string _password = "";
            if (objEbooking.CheckEmailAddress(EmailAddress,out _userName,out _password))
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(EmailAddress);
               // mail.To.Add("mufassar.rashid@my.web.pk");
                mail.From = new MailAddress(ConfigurationManager.AppSettings["JourneyDetailPDFEmailFrom"]);
                mail.Subject = "Your User Credentials";
                string Body = "User Name: "+_userName+"<br />Password: "+_password;
                mail.Body = Body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["SmtpServerName"];
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpServerPortNo"]);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SmtpNetworkCredentialEmail"], ConfigurationManager.AppSettings["SmtpNetworkCredentialPassword"]);
                try
                {
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, ex.ToString().Length > 500 ? ex.ToString().Substring(0, 500) : ex.ToString());
                }
                finally {
                    smtp.Dispose();
                    mail.Dispose();
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Email Not Registered in Our Database.");
            }
            return "true";
        }

        public dynamic GetAppointmentTime(int id)
        {
            Dropdown ddlhelper=new Dropdown();
            StaticCache cache = new StaticCache();
            DataSet ds = new DataSet();
            ds = cache.GetDropDowns(false);
            int tableindex = ddlhelper.GetTableIndex("tblAppointmentTime", ds);
            DataView dv = new DataView(ds.Tables[tableindex], "AppointmentTimeID=" + id,"AppointmentTimeID", DataViewRowState.CurrentRows);
            return dv[0]["Time"];

            
            //return true;
        }

    }
}
