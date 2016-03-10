//using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessRules.wsEBooking;
using System.Web;

namespace BusinessRules
{
    public class clsEBooking
    {
        private string mstrWSBaseURL = string.Empty;
        private BusinessRules.wsEBooking.wsEBooking mobjWSEBooking = null;

        public clsEBooking()
        {
            if (mobjWSEBooking == null)
            {
                mobjWSEBooking = new wsEBooking.wsEBooking();
            }
            //mobjWSEBooking.UsernamePasswordHeader = new BusinessRules.wsEBooking.UsernamePasswordSoapHeader();

            //mobjWSEBooking.UsernamePasswordHeader.Token = BusinessRules.Credentials.Current.TokenXml;
            //mobjWSEBooking.Url = "http://localhost/CareMonXCADEmergencyV9_x/CADServices/wsEBooking.asmx";
            string myurl = System.Web.Configuration.WebConfigurationManager.AppSettings["WSBaseURL"] + "/CADServices/wsEBooking.asmx";
            mobjWSEBooking.Url = myurl;
        }


        #region -- Public Properties --

        public string WSBaseURLStr
        {
            get
            {
                return this.mstrWSBaseURL;
            }
            set
            {
                this.mstrWSBaseURL = value;
            }
        }

        #endregion

        //private BusinessRules.wsEBooking.wsEBooking pWSEBookingObj
        //{
        //    get
        //    {
        //        if (mobjWSEBooking == null)
        //        {
        //            mobjWSEBooking = new BusinessRules.wsEBooking.wsEBooking();
        //        }

        //        if (!clsGeneral.blnIsOfflineMode)
        //        {
        //            mobjWSEBooking.UsernamePasswordHeader = new BusinessRules.wsEBooking.UsernamePasswordSoapHeader();
        //            mobjWSEBooking.UsernamePasswordHeader.Token = Common.Credentials.Current.TokenXml;
        //        }



        //        if (WSBaseURLStr == string.Empty)
        //        {
        //            if (clsGeneral.WSBaseURL != null)
        //                mobjWSEBooking.Url = clsGeneral.WSBaseURL + "/CADServices/wsEBooking.asmx";
        //        }
        //        else
        //            mobjWSEBooking.Url = WSBaseURLStr + "/CADServices/wsCall.asmx";



        //        return mobjWSEBooking;
        //    }
        //}

        #region Appointment Time

        public DataSet SearchAppointementTime(String AppointmentTime)
        {
            return mobjWSEBooking.SearchAppointementTime(AppointmentTime);
            // return pWSEBookingObj.SearchAppointementTime(AppointmentTime);
        }

        //public void AddORUpdateAppointmentTime(int AppointmentID, String AppointmentTime, String ModifiedBy)
        //{
        //    mobjWSEBooking.AddORUpdateAppointmentTime(AppointmentID, AppointmentTime, ModifiedBy);
        //    //pWSEBookingObj.AddORUpdateAppointmentTime(AppointmentID, AppointmentTime, ModifiedBy);
        //}

        #endregion

        #region Appointment Duration
        //public void AddORUpdateAppointmentDuration(int AppointmentDurationID, String AppointmentDuration, String ModifiedBy)
        //{
        //    mobjWSEBooking.AddORUpdateAppointmentDuration(AppointmentDurationID, AppointmentDuration, ModifiedBy);
        //}

        public DataSet SearchAppointmentDuration(String AppointmentDuration)
        {
            return mobjWSEBooking.SearchAppointmentDuration(AppointmentDuration);
        }
        #endregion

        #region Authentication Role

        //public void AddORUpdateAuthenticationRole(int AuthenticationRoleID, String AuthenticationRole, String ModifiedBy)
        //{
        //    mobjWSEBooking.AddORUpdateAuthenticationRole(AuthenticationRoleID, AuthenticationRole, ModifiedBy);
        //}
        public DataSet SearchClinicianRole(String ClinicianRole)
        {
            return mobjWSEBooking.SearchClinicianRole(ClinicianRole);
        }

        #endregion

        #region Escort
        //public void AddORUpdateEscortType(int EscortTypeID, String EscortType, String NoOfEscort, String ModifiedBy)
        //{
        //    mobjWSEBooking.AddORUpdateEscortType(EscortTypeID, EscortType, NoOfEscort, ModifiedBy);
        //}



        public DataSet SearchEscortType(String EscortType)
        {
            return mobjWSEBooking.SearchEscortType(EscortType);
        }


        #endregion

        #region Facility Addresses
        public DataSet SearchFacilityAddress(String FacilityAddress)
        {
            return mobjWSEBooking.SearchFacilityAddress(FacilityAddress);
        }

        //public void AddORUpdateFacilityAddress(int FacilityAddressID, String FacilityType, String FacilityName, String Address1, String Address2, String Address3, String Address4, String PostCode, String TelePhone, String ModifiedBy)
        //{
        //    mobjWSEBooking.AddORUpdateFacilityAddress(FacilityAddressID, FacilityType, FacilityName, Address1, Address2, Address3, Address4, PostCode, TelePhone, ModifiedBy);
        //}

        #endregion

        #region Facility Type
        public DataSet SearchFacilityType(String FacilityType)
        {
            return mobjWSEBooking.SearchFacilityType(FacilityType);
        }

        //public void AddORUpdateFacilityType(int FacilityTypeID, String FacilityType, String ModifiedBy)
        //{
        //    mobjWSEBooking.AddORUpdateFacilityType(FacilityTypeID, FacilityType, ModifiedBy);
        //}

        #endregion

        #region GPPractice
        public DataSet SearchGPPractice(String GPPractice)
        {
            return mobjWSEBooking.SearchGPPractice(GPPractice);
        }

        //public void AddOrUpdateGPPractice(int GPPracticeID, String PracticeName, String Address1, String Address2, String Address3, String Address4, String PostCode, String Telephone, String ModifiedBy)
        //{
        //    mobjWSEBooking.AddOrUpdateGPPractice(GPPracticeID, PracticeName, Address1, Address2, Address3, Address4, PostCode, Telephone, ModifiedBy);
        //}

        #endregion

        #region Infectious

        //public void AddORUpdateInfectious(int InfectiousID, String Infectious, String ModifiedBy)
        //{
        //    mobjWSEBooking.AddORUpdateInfectious(InfectiousID, Infectious, ModifiedBy);
        //}



        public DataSet SearchInfectious(String Infectious)
        {
            return mobjWSEBooking.SearchInfectious(Infectious);
        }
        #endregion

        #region Reason for Cancelation

        //public void AddORUpdateCancelReason(int CancelReasonID, String CancelReason, String ModifiedBy)
        //{
        //    mobjWSEBooking.AddORUpdateCancelReason(CancelReasonID, CancelReason, ModifiedBy);
        //}



        public DataSet SearchReasonForCancellation(String ReasonForCancellation)
        {
            return mobjWSEBooking.SearchReasonForCancellation(ReasonForCancellation);
        }

        #endregion

        #region Reason For Rejection
        //public void AddORUpdateReasonForRejection(int RejectionID, String RejectionReason, String ModifiedBy)
        //{
        //    mobjWSEBooking.AddORUpdateReasonForRejection(RejectionID, RejectionReason, ModifiedBy);
        //}



        public DataSet SearchReasonForRejection(String ReasonForRejection)
        {
            return mobjWSEBooking.SearchReasonForRejection(ReasonForRejection);
        }

        #endregion

        #region Reason for Transport

        //public void AddORUpdateTransportReason(int TransportReasonID, String TransportReason, String ModifiedBy)
        //{
        //    mobjWSEBooking.AddORUpdateTransportReason(TransportReasonID, TransportReason, ModifiedBy);
        //}

        public DataSet SearchReasonForTransport(String TransportReason)
        {
            return mobjWSEBooking.SearchReasonForTransport(TransportReason);
        }
        #endregion

        #region Relationship
        public DataSet SearchRelationship(String Relationship)
        {
            return mobjWSEBooking.SearchRelationship(Relationship);
        }

        //public void AddORUpdateRelationship(int RealtionshipID, String Relationship, String ModifiedBy)
        //{
        //    mobjWSEBooking.AddORUpdateRelationship(RealtionshipID, Relationship, ModifiedBy);
        //}

        #endregion

        #region Title

        public DataSet SearchTitle(String Title)
        {
            return mobjWSEBooking.SearchTitle(Title);
        }

        //public void AddORUpdateTitle(int TitleID, String Title, String ModifiedBy)
        //{
        //    mobjWSEBooking.AddORUpdateTitle(TitleID, Title, ModifiedBy);
        //}

        #endregion

        #region Transport Selection

        //public void AddORUpdateTransportSelection(int TransportSelectionID, String TransportSelection, String ModifiedBy)
        //{
        //    mobjWSEBooking.AddORUpdateTransportSelection(TransportSelectionID, TransportSelection, ModifiedBy);
        //}



        public DataSet SearchTransportSelection(String TransportSelection)
        {
            return mobjWSEBooking.SearchTransportSelection(TransportSelection);
        }
        #endregion

        #region Load Cache
        public DataSet LoadCache(String TableNames)
        {
            return mobjWSEBooking.LoadCache(TableNames);
        }

        #endregion

        #region AddorUpdate web User
        public void AddorUpdateWebUser(string Name, string Login, string password, string Email, int UserID, bool Active, bool isDeleted, int PositionID, int GenderTitleID, string FName, string SurName, int DepartmentID, string PostalAddress, int SerrviceID, string ContactTelNo, int FacilityId,string Extension)
        {
            try
            {
                mobjWSEBooking.AddorUpdateWebUser(Name, Login, password, Email, UserID, Active, isDeleted, PositionID, GenderTitleID, FName, SurName, DepartmentID, PostalAddress, SerrviceID, ContactTelNo, FacilityId,Extension);
            }
            catch(Exception ee)
            {
                throw new Exception(ee.ToString());
            }
        }

        public DataSet GetUserDetails(int userid)
        {
            return mobjWSEBooking.GetUserDetails(userid);
        }

        #endregion

        #region Add or Update Journey
        public void AddorUpdateJourney(DataSet ds, out string refNumber,int userid,out string BookingNumber)
        {
            try
            {
                refNumber = mobjWSEBooking.AddorUpdateJourney(ds, userid, out BookingNumber);
            }
            catch (Exception ee)
            {
                refNumber = "-1";
                BookingNumber = "-1";
                throw new Exception(ee.ToString());
            }
            //mobjWSEBooking.AddorUpdateJourney(ds, out refNumber);
        }

        public DataSet GetJourney(int BookingId, string BookingStatus,string BookingDateFrom,string BookingDateTo,string BookingNumber)
        {
            return mobjWSEBooking.GetJourney(BookingId, BookingStatus, Convert.ToInt32(HttpContext.Current.User.Identity.Name), BookingDateFrom, BookingDateTo, BookingNumber);
            //return mobjWSEBooking.Getjourn
        }
        public DataSet GetJourneys_Filtered_On_JourneyDate(int BookingId, string BookingStatus, string JourneyDateFrom, string JourneyDateTo, string BookingNumber)
        {
            return mobjWSEBooking.GetJourneys_FilteredOn_JourneyDate(BookingId, BookingStatus, Convert.ToInt32(HttpContext.Current.User.Identity.Name), JourneyDateFrom, JourneyDateTo, BookingNumber);
            //return mobjWSEBooking.Getjourn
        }
        public bool ChangeJourneyStatus(int BookingId, string BookingNumber, string CancelReasonText, int statusID, int updatedby, bool is24HourAmendement )
        {
            return mobjWSEBooking.ChangeJourneyStatus(BookingId, BookingNumber, CancelReasonText, statusID, updatedby,null,is24HourAmendement);
            //return mobjWSEBooking.Getjourn
        }
        #endregion

        #region checkEmail
        public Boolean CheckEmailAddress(string EmailAddress,out string _username,out string _pasword)
        {
            try
            {
                return mobjWSEBooking.CheckEmailAddress(EmailAddress,out _username,out _pasword);
            }
            catch (Exception)
            {
                _username = String.Empty;
                _pasword = String.Empty;
                return false;
            }
        }
        #endregion

        #region SearchPatient
        public DataSet SearchPatient(string NhsNumber)
        {
            return mobjWSEBooking.SearchPatient(NhsNumber);
        }
        #endregion

        public DataSet GetJourneyPDF(string referenceNumber)
        {
            return mobjWSEBooking.GetJourneyPDF(referenceNumber);
        }

        public bool DoUserExists(string username)
        {
            return mobjWSEBooking.DoUserExists(username);
        }
    }
}
