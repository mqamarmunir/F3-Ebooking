using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Helpers;
using System.Data.SqlClient;
using System.Text;
using System.Web.Http.Filters;
using System.Web.Http;
using System.Diagnostics;
using System.IO;
using EBooking.Models;
using System.Web.Script.Serialization;


namespace EBooking.Utilities
{
    public class ApplicationAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext context)
        {
            if (!((System.Web.HttpContext.Current.User).Identity).IsAuthenticated)
            {
            }
            base.HandleUnauthorizedRequest(context);
        }
    }
    public class LogExceptionFilterAttribute : ExceptionFilterAttribute, System.Web.Mvc.IExceptionFilter
    {
        public void OnException(System.Web.Mvc.ExceptionContext context)
        {
            Common.ErrorLog(context.Exception);
        }

        public override void OnException(System.Web.Http.Filters.HttpActionExecutedContext context)
        {           
            Common.ErrorLog(context.Exception);

            using (var msg = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError))
            {
                msg.Content = new StringContent("An internal server error occurred, the operation failed. Please contact your system administrator");
                msg.ReasonPhrase = "An internal server error occurred, the operation failed. Please contact your system administrator";
                context.Response = msg;
            }
        }
    }

    public class Common
    {
        public static void ErrorLog(dynamic content)
        {
            var tracemessage = string.Empty;

            if(content is string)
             tracemessage = string.Format("{0}\t[{1}]", DateTime.Now.ToString("MM/dd/yy HH:mm:ss"),  content);            
            else if (content is SqlException)            
                foreach (SqlError error in content.Errors)
                    tracemessage = string.Format("{0}\t{1}\n[{2}]\n{3}\n{4}\n{5}\n{6}", DateTime.Now.ToString("MM/dd/yy HH:mm:ss"), "SQL Exception: " + error.State, "Exception Details: " + error.Message, "Details: " + content.InnerException != null ? content.InnerException.ToString() : string.Empty, "LineNumber: " + error.LineNumber, "Source: " + error.Source, "Procedure: " + error.Procedure);            
              else
            {
                var ex = ((System.Exception)(content));
                tracemessage = string.Format("{0}\t{1}\n{2}", DateTime.Now.ToString("MM/dd/yy HH:mm:ss"), "Exception Details: " + ex.ToString(), "Source: " + ex.Source);
            }

            var path = System.Web.Hosting.HostingEnvironment.MapPath("~/Exception Logs") + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + " Exception.log";

            using (var listener = new TextWriterTraceListener(new FileStream(path, FileMode.Append, FileAccess.Write)))
            {
                //Add file/trace listener to collection.
                Trace.Listeners.Add(listener);
                Trace.WriteLine(tracemessage);

                //Remove and close the file/trace listener.
                Trace.Listeners.Remove(listener);
                listener.Close();
            }
            
        }

        public static bool IsValidAntiForgeryToken(HttpRequestMessage request)
        {
            
            IEnumerable<string> tokenHeaders;
            if (request.Headers.TryGetValues("RequestVerificationToken", out tokenHeaders))
            {
                string[] tokens = tokenHeaders.First().Split(':');
                if (tokens.Length == 2)
                {
                    try
                    {
                        
                        //var x=AntiForgeryConfig.CookieName;
                        //AntiForgeryData.GetUserName(request);
                        AntiForgery.Validate(tokens[0].Trim(), tokens[1].Trim());
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }

                }
                else
                    return false;
            }
            else
                return false;
        }

        public static void LogInvalidBookingRequests(CompositeModel journey)
        {
            var tracemessage = string.Empty;
            var jsontext = new JavaScriptSerializer().Serialize(journey);
            if (!Directory.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/InvalidRequests Log")))
                Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath("~/InvalidRequests Log"));

            var path = System.Web.Hosting.HostingEnvironment.MapPath("~/InvalidRequests Log") + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".json";
            tracemessage = string.Format("{0}\n{1}", DateTime.Now.ToString("MM/dd/yy HH:mm:ss"), "Details: " +jsontext);
            using (var listener = new TextWriterTraceListener(new FileStream(path, FileMode.Append, FileAccess.Write)))
            {
                //Add file/trace listener to collection.
                Trace.Listeners.Add(listener);
                Trace.WriteLine(tracemessage);

                //Remove and close the file/trace listener.
                Trace.Listeners.Remove(listener);
                listener.Close();
            }
            

        }
    }

}