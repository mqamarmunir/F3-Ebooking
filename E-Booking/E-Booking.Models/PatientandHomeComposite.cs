using EBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EBooking.Models
{
    public class PatientandHomeComposite
    {


        public PatientModel Patient { get; set; }

        public HomeAddressModel HomeAddress { get; set; }
    }
}
