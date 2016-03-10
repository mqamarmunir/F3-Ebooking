using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ExpressiveAnnotations.Attributes;

namespace EBooking.Models
{
    public class HomeAddressModel
    {
        public bool IsNoFixAbode { get; set; }

        [RequiredIf("IsNoFixAbode == false", ErrorMessage = "Line One is required.")]
        public string LineOne { get; set; }
        
        [RequiredIf("IsNoFixAbode == false", ErrorMessage = "Line One is required.")]
        public string LineTwo { get; set; }

        [RequiredIf("IsNoFixAbode == false", ErrorMessage = "Line Three is required.")]
        public string LineThree { get; set; }

        [RequiredIf("IsNoFixAbode == false", ErrorMessage = "Line Four is required.")]
        public string LineFour { get; set; }

        //[RequiredIf("IsNoFixAdobe == false", ErrorMessage = "Postal Code is required.")]
        public string PostCode { get; set; }

        //[RequiredIf("IsThisPatientHomeAddres == false && FacilityTypeId", ErrorMessage = "Address Easting is Required.")]
        public string Easting { get; set; }
        //[RequiredIf("IsThisPatientHomeAddres == false && FacilityTypeId", ErrorMessage = "Address Northing is Required.")]
        public string Northing { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string GridReference { get; set; }
        public string UPRN { get; set; }


        [RequiredIf("IsNoFixAbode == false", ErrorMessage = "Contact Number is required.")]
        public string ContactTelNo { get; set; }

        [RequiredIf("IsNoFixAbode == false", ErrorMessage = "Alternate Contact Number is required.")]
        public string AlternateContactTelNo { get; set; }

        [RequiredIf("IsNoFixAbode == false", ErrorMessage = "Relationship is required.")]
        public int RelationshipId { get; set; }
    }

}