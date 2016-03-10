using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EBooking.Models
{
    public class DepartmentModel
    {
        public DepartmentModel()
        {

        }

        public DepartmentModel(int Id, string Name)
        {
            this.DepartmentId = Id;
            this.DepartmentName = Name;
           
        }

        [Display(Name = "Department Id")]
        public int DepartmentId { get; set; }

        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }



    }

    public class FacilityModel
    {
        public FacilityModel() { }

        public FacilityModel(int Id, string Name, int TypeId)
        {
            this.FacilityId = Id;
            this.FacilityName = Name;
            this.FacilityTypeId = TypeId;
        }

        [Display(Name = "Facility Id")]
        public int FacilityId { get; set; }

        [Display(Name = "Facility Name")]
        public string FacilityName { get; set; }

        public int FacilityTypeId { get; set; }
    }

    public class FacilityTypeModel
    {
        public FacilityTypeModel()
        {

        }

        public FacilityTypeModel(int Id, string Name)
        {
            this.FacilityTypeId = Id;
            this.FacilityTypeName = Name;
        }

        [Display(Name = "Facility Id")]
        public int FacilityTypeId { get; set; }

        [Display(Name = "Facility Name")]
        public string FacilityTypeName { get; set; }
    }

    public class GPPracticeModel
    {
        public GPPracticeModel()
        {

        }

        public GPPracticeModel(int Id, string Name)
        {
            this.GPPracticeId = Id;
            this.GPPracticeName = Name;
        }

        [Display(Name = "GP Patient Id")]
        public int GPPracticeId { get; set; }

        [Display(Name = "GP Patient Name")]
        public string GPPracticeName { get; set; }
    }

    public class GPPracticeAddressModel
    {
        public GPPracticeAddressModel()
        {

        }

        public GPPracticeAddressModel(int Id, string Name)
        {
            this.GPPracticeAddressId = Id;
            this.GPPracticeAddressName = Name;
        }

        [Display(Name = "GP Practice Address Id")]
        public int GPPracticeAddressId { get; set; }

        [Display(Name = "GP Practice Address Name")]
        public string GPPracticeAddressName { get; set; }
    }

    public class ServiceTypeModel
    {
        public ServiceTypeModel()
        {

        }

        public ServiceTypeModel(int Id, string Name)
        {
            this.ServiceTypeId = Id;
            this.ServiceTypeName = Name;
        }

        [Display(Name = "ServiceType Id")]
        public int ServiceTypeId { get; set; }

        [Display(Name = "ServiceType Name")]
        public string ServiceTypeName { get; set; }
    }

    public class AuthorisingRoleModel
    {
        public AuthorisingRoleModel()
        {

        }

        public AuthorisingRoleModel(int Id, string Name)
        {
            this.AuthorisingRoleId = Id;
            this.AuthorisingRoleName = Name;
        }

        [Display(Name = "Role Id")]
        public int AuthorisingRoleId { get; set; }

        [Display(Name = "Role Name")]
        public string AuthorisingRoleName { get; set; }
    }

    public class TitleModel
    {
        public TitleModel()
        {

        }

        public TitleModel(int Id, string Name)
        {
            this.TitleId = Id;
            this.TitleName = Name;
        }

        [Display(Name = "Title Id")]
        public int TitleId { get; set; }

        [Display(Name = "Title Name")]
        public string TitleName { get; set; }
    }

    public class YourPositionModel
    {
        public YourPositionModel()
        {

        }

        public YourPositionModel(int Id, string Name)
        {
            this.YourPositionId = Id;
            this.YourPositionName = Name;
        }

        [Display(Name = "Position Id")]
        public int YourPositionId { get; set; }

        [Display(Name = "Position Name")]
        public string YourPositionName { get; set; }
    }
    public class CancelReasonModel
    {
        public CancelReasonModel()
        {

        }

        public CancelReasonModel(int Id, string Name)
        {
            this.CancelReasonId = Id;
            this.CancelReasonName = Name;
        }

        [Display(Name = "Cancel Reason Id")]
        public int CancelReasonId { get; set; }

        [Display(Name = "Cancel Reason Name")]
        public string CancelReasonName { get; set; }
    }


    public class AppointmentTimeModel
    {
        public AppointmentTimeModel()
        {

        }

        public AppointmentTimeModel(int Id, string Name)
        {
            this.AppointmentTimeId = Id;
            this.AppointmentTimeName = Name;
        }

        [Display(Name = "AppointmentTime Id")]
        public int AppointmentTimeId { get; set; }

        [Display(Name = "AppointmentTime Name")]
        public string AppointmentTimeName { get; set; }
    }
    
     public class EstimatedAppointmentDurationModel
    {
        public EstimatedAppointmentDurationModel()
        {

        }

        public EstimatedAppointmentDurationModel(int Id, string Name)
        {
            this.EstimatedAppointmentDurationId = Id;
            this.EstimatedAppointmentDurationName = Name;
        }

        [Display(Name = "EstimatedTime Id")]
        public int EstimatedAppointmentDurationId { get; set; }

        [Display(Name = "EstimatedTime Name")]
        public string EstimatedAppointmentDurationName { get; set; }
    }
    public class RelationshipToPatientModel
    {
        public RelationshipToPatientModel()
        {

        }

        public RelationshipToPatientModel(int Id, string Name)
        {
            this.RelationshipToPatientId = Id;
            this.RelationshipToPatientName = Name;
        }

        [Display(Name = "Relationship To Patient Id")]
        public int RelationshipToPatientId { get; set; }

        [Display(Name = "Relationship To Patient Name")]
        public string RelationshipToPatientName { get; set; }
    }

}



public class PatientTypeModel
{

    public PatientTypeModel()
    {

    }

    public PatientTypeModel(int Id, string Name)
    {
        this.PatientTypeId = Id;
        this.PatientTypeName = Name;
    }

    [Display(Name = "Relationship To Patient Id")]
    public int PatientTypeId { get; set; }

    [Display(Name = "Relationship To Patient Name")]
    public string PatientTypeName { get; set; }
}

public class RequestTypeModel
{
    public RequestTypeModel() { }

    public RequestTypeModel(int Id, string Name)
    {
        this.RequestTypeId = Id;
        this.RequestTypeName = Name;
    }

    [Display(Name = "Relationship To Patient Id")]
    public int RequestTypeId { get; set; }

    [Display(Name = "Relationship To Patient Name")]
    public string RequestTypeName { get; set; }
}

public class EscortTypeModel
{
    public EscortTypeModel() { }

    public EscortTypeModel(int Id, string Name)
    {
        this.EscortTypeId = Id;
        this.EscortTypeName = Name;
    }

    [Display(Name = "Escort Type Id")]
    public int EscortTypeId { get; set; }

    [Display(Name = "Escort Type Name")]
    public string EscortTypeName { get; set; }
}

public class EscortNumberModel
{
    public EscortNumberModel() { }

    public EscortNumberModel(int Id, string Name)
    {
        this.EscortNumberId = Id;
        this.EscortNumberName = Name;
    }

    [Display(Name = "Escort Number Id")]
    public int EscortNumberId { get; set; }

    [Display(Name = "Escort Number Name")]
    public string EscortNumberName { get; set; }
}

public class InfectiousModel
{
    public InfectiousModel() { }

    public InfectiousModel(int Id, string Name)
    {
        this.InfectiousId = Id;
        this.InfectiousName = Name;
    }

    [Display(Name = "Infectious Id")]
    public int InfectiousId { get; set; }

    [Display(Name = "Infectious Name")]
    public string InfectiousName { get; set; }
}

public class TransportRequestReasonModel
{
    public TransportRequestReasonModel() { }

    public TransportRequestReasonModel(int Id, string Name)
    {
        this.TransportRequestReasonId = Id;
        this.TransportRequestReasonName = Name;
    }

    [Display(Name = "Escort Number Id")]
    public int TransportRequestReasonId { get; set; }

    [Display(Name = "Escort Number Name")]
    public string TransportRequestReasonName { get; set; }
}

public class TransportSelectionModel
{
    public TransportSelectionModel() { }

    public TransportSelectionModel(int Id, string Name)
    {
        this.TransportSelectionId = Id;
        this.TransportSelectionName = Name;
    }

    [Display(Name = "Escort Number Id")]
    public int TransportSelectionId { get; set; }

    [Display(Name = "Escort Number Name")]
    public string TransportSelectionName { get; set; }
}