using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using EBooking.Filters;
using EBooking.Models;

namespace EBooking.Controllers
{
    [Authorize]
    //[InitializeSimpleMembership]
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult RequesterDetails()
        {
            return View();
        }
        
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }
    }
}
