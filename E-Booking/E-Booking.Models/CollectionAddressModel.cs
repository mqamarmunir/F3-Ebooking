using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ExpressiveAnnotations.Attributes;

namespace EBooking.Models
{
    public class CollectionAddressModel
    {
        public bool IsThisPatientHomeAddress { get; set; }

        [RequiredIf("IsThisPatientHomeAddres == false", ErrorMessage = "Facility Type is required.")]
        public int FacilityTypeId { get; set; }

        [RequiredIf("IsThisPatientHomeAddres == false && FacilityTypeId!=5", ErrorMessage = "Facility is required.")]
        public int FacilityId { get; set; }

        [RequiredIf("IsThisPatientHomeAddres == false && FacilityId && FacilityTypeId==4 ", ErrorMessage = "Department is required.")]
        public int DepartmentId { get; set; }

        public string LineOne { get; set; }

        [Required(ErrorMessage = "Line Two is required.")]
        public string LineTwo { get; set; }

        [Required(ErrorMessage = "Line Three is required.")]
        public string LineThree { get; set; }

        [Required(ErrorMessage = "Line Four is required.")]
        public string LineFour { get; set; }

        //[Required(ErrorMessage = "Post code is required.")]
        public string PostCode { get; set; }
        
        //[RequiredIf("IsThisPatientHomeAddres == false && FacilityTypeId", ErrorMessage = "Address Easting is Required.")]
        public string Easting { get; set; }
       // [RequiredIf("IsThisPatientHomeAddres == false && FacilityTypeId", ErrorMessage = "Address Northing is Required.")]
        public string Northing { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string GridReference { get; set; }
        public string UPRN { get; set; }

        [Required(ErrorMessage = "Contact Number is required.")]
        public string ContactTelNo { get; set; }
        
        public string ExtensionNo { get; set; }
    }
}