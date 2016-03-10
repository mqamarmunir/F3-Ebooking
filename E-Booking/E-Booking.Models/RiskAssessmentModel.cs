using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ExpressiveAnnotations.Attributes;

namespace EBooking.Models
{
    
    public class RiskAssessmentModel
    {
        //[StringLength(200, ErrorMessage = "Property Risk Assessment cannot be longer than 200 characters and Less than 10 characters.", MinimumLength = 10)]
        public string PropertyRiskAssessment { get; set; }

       // [StringLength(200, ErrorMessage = "Patient Risk Assessment cannot be longer than 200 characters and Less than 10 characters.", MinimumLength = 10)]
        public string PatientRiskAssessment { get; set; }

        public bool IsManualHandlingProfileCarriedOutYes { get; set; }

        public bool IsManualHandlingProfileCarriedOutNo { get; set; }

    }
}