using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EBooking.Filters;
using EBooking.Models;
using BusinessRules;
using System.Data;


namespace EBooking.Controllers
{
    //[Authorize]
    public class DropdownController : Controller
    {
        [HttpGet]
        public JsonResult GetAllDropdowns()
        {

            var dropdown = new Dropdown();
            // var x = ;
            return Json(dropdown.GetAllDropdowns(false), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetFilteredDropdowns(string tableNames)
        {

            var dropdown = new Dropdown();
            Dropdown.TableNames = tableNames;
            // var x = ;
            return Json(dropdown.GetAllDropdowns(true), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetFacilityDepartments(int FacilityId)
        {
            
            
            var dropdown = new Dropdown();
            // var x = ;
            return Json(dropdown.GetFacilityDepartments(FacilityId), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetFacilities(int FacilityTypeId)
        {
            var dropdown = new Dropdown();
            return Json(dropdown.GetFacilities(FacilityTypeId), JsonRequestBehavior.AllowGet);
        }

    }
}
