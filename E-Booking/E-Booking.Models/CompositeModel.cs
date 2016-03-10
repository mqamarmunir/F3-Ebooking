using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBooking.Models
{
    public class CompositeModel
    {        
        public PatientModel Patient { get; set; }

        public HomeAddressModel HomeAddress { get; set; }

        public CollectionAddressModel CollectionAddress { get; set; }

        public DestinationAddressModel DestinationAddress { get; set; }

        public TransportRequirementModel TransportRequirement { get; set; }

        public RiskAssessmentModel RiskAssessment { get; set; }

        public SpecialistTransportRequestModel SpecialistTransportRequest { get; set; }

      
    }
}