using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EBooking.Models
{
    public class AdditionalPatientInfoModel
    {
        [Required(ErrorMessage = "Additional Patient Info is required.")]
        public string AdditionalPatientInfo { get; set; }

        public bool IsInfectious { get; set; }

        public bool IsTravellingWithOwnOxygen { get; set; }

        public bool IsEscortTravelling { get; set; }

        public int EscortTypeId { get; set; }

        public int EscortNumberId { get; set; }

        public bool IsBariatric { get; set; }

        public bool IsFullLegPlasterPOP { get; set; }

        public bool IsElectricWheelchair { get; set; }

        public bool IsWheelchairAndLegExtension { get; set; }

        public bool IsWaterLow { get; set; }

        public bool IsDNACPR { get; set; }

        public bool IsDiabetic { get; set; }

        public bool IsNuclearMedicineRadioActiveRisk { get; set; }

        public bool IsNoneOfAbove { get; set; }
    }
}