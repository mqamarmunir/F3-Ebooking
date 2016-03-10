using EBooking.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using itextSharpPDFController;
using System.IO;
using System.Net;
using BusinessRules;
using System.Net.Mail;
using System.Configuration;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.Data;
using EBooking.Models;

namespace EBooking.Controllers
{
    [Authorize]
    public class JourneyController : PdfViewController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Journey()
        {
            return View();
        }
        public ActionResult JourneyView()
        {
            return View();
        }

        public ActionResult TransportSelection()
        {
            return View();
        }
        public ActionResult EscortTravelling()
        {
            return View();
        }
        public ActionResult PopUp()
        {
            return View();
        }

        public ActionResult JourneyGrid()
        {
            return View();
        }

        public ActionResult JourneyPDF()
        {
           
            return View();
        }

        public ActionResult FlowChart()
        {
            return View();
        }
        public ActionResult SortableGridTest()
        {
            return View();
        }

        public bool SendPDFEmail(string refNumber)
        {
            try
            {
                string emailto = String.Empty;
                EBooking.Models.jouneyCompositePDF journey = new Journey().GetJourneyPDF(refNumber);
                /// this.GetPDF()
                /// 



                FileContentResult PDFFile = this.GetPDF("", "JourneyPDF", journey);
                MemoryStream pdfMS = new MemoryStream(PDFFile.FileContents);

                //    clsEBooking objEBooking = new clsEBooking();
                //DataSet ds1 = objEBooking.GetUserDetails(Convert.ToInt32(User.Identity.Name));
                //if (ds1.Tables[0].Rows.Count > 0)
                //{
                //    emailto = ds1.Tables[0].Rows[0]["Email"].ToString().Trim();
                //}
                emailto = journey.EmailAddress;
                //System.IO.File.WriteAllBytes(HttpContext.Server.MapPath(@"~\App_Data\Journey-EBooking.pdf"), PDFFile.FileContents);

                using (MailMessage mail = new MailMessage())
                {
                    mail.To.Add(emailto);
                    //mail.To.Add("asif_rauf@my.web.pk");
                   // mail.To.Add("qamar.nust@gmail.com");
                  //  mail.Bcc.Add("asif_rauf@my.web.pk");
                    //mail.Bcc.Add("qamar.nust@gmail.com");
                    mail.From = new MailAddress(ConfigurationManager.AppSettings["JourneyDetailPDFEmailFrom"]);
                    mail.Subject = "DO-NOT-REPLY";
                    string Body = "Journey Details - Please Find The Attachment Above.";
                    mail.Body = Body;
                    mail.IsBodyHtml = true;

                    Attachment attachedFile = new Attachment(pdfMS, new System.Net.Mime.ContentType("application/pdf"));

                    mail.Attachments.Add(attachedFile);
                    mail.Attachments.Last().ContentDisposition.FileName = "JourneyDetails.pdf";

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = ConfigurationManager.AppSettings["SmtpServerName"];
                        smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpServerPortNo"]);
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SmtpNetworkCredentialEmail"], ConfigurationManager.AppSettings["SmtpNetworkCredentialPassword"]);
                        try
                        {
                            smtp.Send(mail);
                        }
                        catch (Exception ee)
                        {

                        }
                        finally
                        {
                            pdfMS.Dispose();
                         
                        }
                    }
                }

                Response.StatusCode = 200;
                return true;

            }
            catch
            {
                Response.StatusCode = 400;
                return false;
            }
            
        }
    }
}
