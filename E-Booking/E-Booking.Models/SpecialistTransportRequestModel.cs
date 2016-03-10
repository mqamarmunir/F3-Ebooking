using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EBooking.Models
{
    public class SpecialistTransportRequestModel
    {
        public bool IsHandledByProfessional { get; set; }

        public bool IsWhilstLayingDown { get; set; }

        public bool IsOxygenTheropy { get; set; }

        public bool IsPrecludesTravelling { get; set; }

        public bool IsAdmission { get; set; }

        public bool IsVisitOrAdmitted { get; set; }

        public bool IsVisit { get; set; }

        public string AuthorisingConsultantOrGP { get; set; }

        public string AuthorisingPracticeName { get; set; }
    }
}