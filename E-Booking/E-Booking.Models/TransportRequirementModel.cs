using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ExpressiveAnnotations.Attributes;

namespace EBooking.Models
{
    public class TransportRequirementModel
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Transport Request Reason is required.")]
        public int TransportRequestReasonId { get; set; }

        [Required(ErrorMessage = "Transport Selection is required.")]
        public int TransportSelectionId { get; set; }
        
        public bool IsInfectious { get; set; }

        [RequiredIf("IsInfectious == true", ErrorMessage = "Infectious is required.")]
        public int InfectiousId { get; set; }

        [RequiredIf("InfectiousId == 4 && IsInfectious == true", ErrorMessage = "Additional Patient Info is required.")]
        //[StringLength(200, ErrorMessage = "Additional Patient Info cannot be longer than 200 characters and Less than 10 characters.", MinimumLength = 10)]
        public string AdditionalPatientInfo { get; set; }

        public bool IsTravellingWithOwnOxygen { get; set; }
        
        public bool IsEscortTravelling { get; set; }

        [RequiredIf("IsEscortTravelling == true", ErrorMessage = "Escort Type is required.")]
        public int EscortTypeId { get; set; }

        [RequiredIf("IsEscortTravelling == true", ErrorMessage = "Escort Number is required.")]
        public int EscortNumberId { get; set; }

        public bool IsBariatric { get; set; }

        public bool IsFullLegPlasterPOP { get; set; }

        public bool IsElectricWheelchair { get; set; }

        public bool IsWheelchairAndLegExtension { get; set; }

        public bool IsWaterlow { get; set; }

        public bool IsDNACPR { get; set; }

        public bool IsDiabetic { get; set; }

        public bool IsNuclearMedicineRadioActiveRisk { get; set; }

        public bool IsNoneOfAbove { get; set; }

        public bool Is24HourAmendment { get; set; }

    }
}