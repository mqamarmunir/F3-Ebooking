using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ExpressiveAnnotations.Attributes;

namespace EBooking.Models
{
    public class PatientModel
    {

        public bool IsRiskAssessmentRequired { get; set; }

        //Appointment Details
        public bool IsMainlandRepatriation { get; set; }
        
        
        
        [RequiredIf("IsMainlandRepatriation == false", ErrorMessage = "Journey Date is required.")]
        public DateTime JourneyDate { get; set; }

        [RequiredIf("IsMainlandRepatriation == false", ErrorMessage = "Appointment Time is required.")]
        public int AppointmentTimeId { get; set; }

        public int EstimatedAppointmentDurationId { get; set; }

        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Valid time required e-g (10:00)")]
        public string ActualAppointmentTime { get; set; }

        public int PatientID { get; set; }
        //Patient Details
        [Required(ErrorMessage = "Gender Title is required.")]
        public int GenderTitleId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime BirthDate { get; set; }

        [RegularExpression(@"^\d{3}\s\d{3}\s\d{4}$", ErrorMessage = "Valid NHS Number required e-g 000 000 0001")]
        public string NHSNumber { get; set; }

        // [Required(ErrorMessage = "Isle Of WightNo is required.")]//Change Made:09/12/2015 No more required. Change Ref.: E Booking Testing (live) - v2.1 - 2015.12.8 Emailed On: 09/12/2015
        [RegularExpression(@"^[IW]{2}[0-9]{6}", ErrorMessage = "Valid Isle of Wight Number required e-g IW000001")]
        public string IsleOfWightNo { get; set; }

        [Required(ErrorMessage = "Name Of GP is required.")]
        public string NameOfGP { get; set; }

        //[Required(ErrorMessage = "Last Recorded Patient Weight is required.")] //Change Made:09/12/2015 No more required. Change Ref.: E Booking Testing (live) - v2.1 - 2015.12.8 Emailed On: 09/12/2015
        public double LastRecordedPatientWeight { get; set; }

       // [Required(ErrorMessage = "Weighing Date is required.")]//Change Made:09/12/2015 No more required. Change Ref.: E Booking Testing (live) - v2.1 - 2015.12.8 Emailed On: 09/12/2015
        public DateTime? WeighingDate { get; set; }

        [Required(ErrorMessage = "GP Practice is required.")]
        public int GPPracticeId { get; set; }

        //[Required(ErrorMessage = "Line One  is required.")]
        public string GPPracticeAddressLineOne { get; set; }
       
        [Required(ErrorMessage = "Line Two  is required.")]
        public string GPPracticeAddressLineTwo { get; set; }

        [Required(ErrorMessage = "Line Three  is required.")]
        public string GPPracticeAddressLineThree { get; set; }

        [Required(ErrorMessage = "Line Four  is required.")]
        public string GPPracticeAddressLineFour { get; set; }

        //[Required(ErrorMessage = "Post Code  is required.")]
        public string GPPracticeAddressPostCode { get; set; }

        public string GPPracticeEasting { get; set; }
        public string GPPracticeNorthing { get; set; }
        public string GPPracticeLatitude { get; set; }
        public string GPPracticeLongitude { get; set; }
        public string GPPracticeGridReference { get; set; }
        public string GPPracticeURPN { get; set; }



        //[RequiredIf("IsThisPatientHomeAddres == false && FacilityTypeId", ErrorMessage = "Address Easting is Required.")]
        public string Easting { get; set; }
        //[RequiredIf("IsThisPatientHomeAddres == false && FacilityTypeId", ErrorMessage = "Address Northing is Required.")]
        public string Northing { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string GridReference { get; set; }
        public string UPRN { get; set; }
        [Required(ErrorMessage = "Contact Number  is required.")]
        public string ContactTelephoneNo { get; set; }

        public string BookingStatusName { get; set; }

        public DateTime BookingDateTime { get; set; }
        public string BookingNo { get; set; }

        [Required(ErrorMessage = "Subjective Code  is required.")]
        public string RequesterSubjectiveCode { get; set; }
        [Required(ErrorMessage = "Cost Center is required.")]
        public string RequesterCostCenter  { get; set; }
        [Required(ErrorMessage = "Authorizing Clinician  is required.")]
        public string RequesterAuthorizingClinician { get; set; }
        [Required(ErrorMessage = "ClinicianRoleID is required.")]
        public int RequesterAuthorizingRoleId  { get; set; }

        public bool ComplexJourney { get; set; }

     
       
        [RequiredIf("ComplexJourney == true", ErrorMessage = "2nd Journey Date is required.")]
        public DateTime JourneyDate2 { get; set; }

        public string RejectionReason { get; set; }

        public DateTime LastStatusAt { get; set; }

        public Int32 CADCaseID { get; set; }

        public Boolean isPrivatePatient { get; set; }
    }
}