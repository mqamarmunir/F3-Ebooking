using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EBooking.Models
{
    public class RequestertModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Requested Date is required.")]
        
        public DateTime RequestDate { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public int GenderTitleId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Position")]
        public string StaffPosition { get; set; }

        [Required(ErrorMessage = "Facility ID is required.")]
        [Display(Name = "Facility Id")]
        public string FacilityId { get; set; }
        
        [Required(ErrorMessage = "Department ID is required.")]
        [Display(Name = "DepartmentId")]
        public string DepartmentId { get; set; }

        [Required(ErrorMessage= "Service Type is required.")]
        [Display(Name = "Service Type")]
        public string ServiceTypeId { get; set; }

        [Required(ErrorMessage = "Cost Center is required.")]
        public string CostCentre { get; set; }

        [Required(ErrorMessage = "Budget Code is required.")]
        [Display(Name = "Budget Code")]
        public string SubjectiveCode { get; set; }

        [Required(ErrorMessage = "Postal Address is required.")]
        [Display(Name = "Postal Address")]
        public string PostalAddress { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"/^[a-zA-Z]+@nhs\.net$/", ErrorMessage = "Valid email required (example@abc.com)")]
        [Display(Name = "EmailAddress")]
        public string EmailAddress { get; set; }
                
        [Required(ErrorMessage = "Telephone No is required.")]
        [Display(Name = "Telephone No")]
        public string ContactTelNo { get; set; }

        [Required(ErrorMessage = "Authorising Clinician is required.")]
        [Display(Name = "Authorising Clinician")]
        public string AuthorisingClinician { get; set; }

        [Required(ErrorMessage = "Authorising Role is required." )]
        [Display(Name = "Authorising Role")]
        public string AuthorisingRoleId { get; set; }

        public string FacilityName { get; set; }

        public string DepartmentName { get; set; }

        public string facilityType { get; set; }

        public string Extension { get; set; }

        public string UserName { get; set; }



    }
}