using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace EBooking.Models
{
    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class SignupModel
    {


        [Required]
        public string ConfirmEmailAddress { get; set; }
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string ContactTelNo { get; set; }
        [RequiredIf("facilityTypeID=4")]
        public int DepartmentId { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public int FacilityId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public int GenderTitleId { get; set; }
        public int Id { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        public int Position { get; set; }
        [Required]
        public string PostalAddress { get; set; }
        [Required]
        public int ServiceTypeId { get; set; }
        [Required]
        public string Surname { get; set; }
        [Display(Name = "User name")]
        [Required]
        public string Username { get; set; }
        [Required]
        public int facilityTypeID { get; set; }

        public string Extension { get; set; }
        

    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
