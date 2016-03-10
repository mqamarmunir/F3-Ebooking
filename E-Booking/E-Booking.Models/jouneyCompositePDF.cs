using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EBooking.Models
{
    public class jouneyCompositePDF
    {

        public string BookingNumber { get; set; }
        public string BookingStatusName { get; set; }
        public DateTime LastStatusAt { get; set; }
        public DateTime JourneyDate { get; set; }
        public string StandardAppointmentTime { get; set; }
        public string ActualAppointmentTime { get; set; }
        public string AppointmentDuration { get; set; }
        public bool IsMainlandRepatriation { get; set; }
        public bool IsRiskAssessmentRequired { get; set; }
        public string PropertyRiskAssessment { get; set; }
        public string PatientRiskAssessment { get; set; }
        public bool IsManualHandlingProfileCarriedOutYes { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string NHSNumber { get; set; }
        public string IsleOfWightNo { get; set; }
        public double LastRecordedPatientWeight { get; set; }
        public DateTime WeighingDate { get; set; }
        public Boolean isPrivatePatient { get; set; }
        
        public string NameOfGP { get; set; }
        public string GpPracticeName { get; set; }
        public string GPPracticeAddressLineOne { get; set; }
        public string GPPracticeAddressLineTwo { get; set; }
        public string GPPracticeAddressLineThree { get; set; }
        public string GPPracticeAddressLineFour { get; set; }
        public string GPPracticeAddressPostCode { get; set; }
        public string ContactTelephoneNo { get; set; }
        
        public bool IsNoFixAbode { get; set; }
        public string HomeLineOne { get; set; }
        public string HomeLineTwo { get; set; }
        public string HomeLineThree { get; set; }
        public string HomeLineFour { get; set; }
        public string HomePostCode { get; set; }
        public string HomePhone { get; set; }
        public string AlternatePhone { get; set; }
        public string RelationshiptoPatient { get; set; }
        
        public bool PickIsThisPatientHomeAddress { get; set; }
        public string PickFacilityType { get; set; }
        public string PickFacility { get; set; }
        public string PickFacilityDepartment { get; set; }
        public string PickLineOne { get; set; }
        public string PickLineTwo { get; set; }
        public string PickLineThree { get; set; }
        public string PickLineFour  { get; set; }
        public string PickPostCode { get; set; }
        public string PickContactTelNo { get; set; }
        public string PickExtensionNo { get; set; }

        public bool DropIsThisPatientHomeAddress { get; set; }
        public string DropFacilityType { get; set; }
        public string DropFacility { get; set; }
        public string DropFacilityDepartment { get; set; }
        public string DropLineOne { get; set; }
        public string DropLineTwo { get; set; }
        public string DropLineThree { get; set; }
        public string DropLineFour { get; set; }
        public string DropPostCode { get; set; }
        public string DropContactTelNo { get; set; }
        public string DropExtensionNo { get; set; }

        public string TransportRequestReason { get; set; }
        public string TransportSelection { get; set; }
        public string AdditionalPatientInfo { get; set; }
        
        public bool IsInfectious { get; set; }
        public string InfectionName { get; set; }
        public bool IsTravellingWithOwnOxygen { get; set; }
        public bool IsEscortTravelling { get; set; }
        public string EscortType { get; set; }
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

        public string UserName { get; set; }
        public string StaffPosition { get; set; }
        public string RequesterFirstName { get; set; }
        public string RequesterSurName{get;set;}
        public string RequesterFacility{get;set;}
        public string ServiceType { get; set; }
        public string RequesterCostCenter { get; set; }
        public string RequesterSubjectiveCode { get; set; }
        public string EmailAddress { get; set; }
        public string RequesterContactTelNo { get; set; }
        public string RequesterAuthorizingClinician { get; set; }
        public string ClinicianRoleName { get; set; }
                      
    }
}
