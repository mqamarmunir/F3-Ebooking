using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EBooking.Models;
using System.Reflection;
using System.Data;
using System.Globalization;
using System.Web;
//using E_Booking.Models;


namespace BusinessRules
{
    public class Journey
    {
        private int userid = -1;
        #region Create
        private string refNumber = String.Empty;
        private string BookingNumber = String.Empty;
        int tableindex = 0;

        public void CreateJourney(CompositeModel journey, int userid, out string refNumber, out string BookingNumber)
        {
            try
            {
                PatientModel patient = journey.Patient;

                HomeAddressModel homeAddress = journey.HomeAddress;

                CollectionAddressModel collectionAddress = journey.CollectionAddress;

                DestinationAddressModel destinationAddress = journey.DestinationAddress;

                TransportRequirementModel transportRequirement = journey.TransportRequirement;

                RiskAssessmentModel additionalInfoRiskAssessment = journey.RiskAssessment;

                SpecialistTransportRequestModel specialistTransportRequest = journey.SpecialistTransportRequest;
                DataSet ds = new DataSet();



                DataTable dtPatient = new DataTable("Patient");
                DataTable dthomeAddress = new DataTable("HomeAddress");
                DataTable dtCollectionaddress = new DataTable("CollectionAddress");
                DataTable dtdestinationAddress = new DataTable("DestinationAddress");
                DataTable dttransportRequirement = new DataTable("TransportRequirement");
                DataTable dtadditionalInfoRiskAssessment = new DataTable("AdditionalInfoRiskAssessment");
                DataTable dtspecialistTransportRequest = new DataTable("SpecialistTransportRequest");





                #region setting Parameters in a Dictionary
                Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

                PropertyInfo[] propertiesPatient = patient.GetType().GetProperties();
                PropertyInfo[] propertieHomeAddress = homeAddress.GetType().GetProperties();

                PropertyInfo[] propertiescollectionAddress = collectionAddress.GetType().GetProperties();
                PropertyInfo[] propertiesadditionalInfoRiskAssessment = additionalInfoRiskAssessment.GetType().GetProperties();
                PropertyInfo[] propertiesdestinationAddress = destinationAddress.GetType().GetProperties();
                PropertyInfo[] propertietransportRequirement = transportRequirement.GetType().GetProperties();

                PropertyInfo[] propertiespecialistTransportRequest = specialistTransportRequest.GetType().GetProperties();

                #region patient
                foreach (var property in propertiesPatient)
                {
                    string propertyName = property.Name;
                    //var x= property.GetValue(property.Name,new object[]{property.GetIndexParameters()});
                    var propertyValue = property.GetValue(patient, null);
                    if (propertyValue == null)
                    {
                        propertyValue = "";
                    }
                    dtPatient.Columns.Add(propertyName);
                    param.Add("@ppatient" + propertyName, propertyValue);
                }

                DataRow dr = dtPatient.NewRow();
                for (int i = 0; i < dtPatient.Columns.Count; i++)
                {
                    dr[dtPatient.Columns[i].ColumnName] = param["@ppatient" + dtPatient.Columns[i].ColumnName];

                }
                if (dr["Easting"].ToString() == "")
                {
                    //StaticCache cache = new StaticCache();
                    //DataSet dsSystemSettings = new DataSet();
                    //dsSystemSettings = cache.GetDropDowns(false);
                    //tableindex = GetTableIndex("SystemSetting", dsSystemSettings);
                    dr["Easting"] = "";// dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultEasting"].ToString();
                    dr["Northing"] = "";//        dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultNorthing"].ToString();
                    dr["Latitude"] = "";//       dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultLatitude"].ToString();
                    dr["Longitude"] = "";//     dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultLongitude"].ToString();
                    dr["GridReference"] = "";//   dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultGridReference"].ToString();
                }
                else
                {
                    clsAddress objAddress = new clsAddress();
                    clsAddress.structAddress _address = new clsAddress.structAddress();
                    _address = objAddress.GetLatLongAndGridReference(Convert.ToDouble(dr["Easting"].ToString()), Convert.ToDouble(dr["Northing"].ToString()));
                    dr["Latitude"] = _address.Latitude;
                    dr["Longitude"] = _address.Longitude;
                    dr["GridReference"] = _address.OSGridReference;

                }

                ////////For Gp Practice Address//////////////////////
                if (dr["GPPracticeEasting"].ToString() == "")
                {
                    //StaticCache cache = new StaticCache();
                    //DataSet dsSystemSettings = new DataSet();
                    //dsSystemSettings = cache.GetDropDowns(false);
                    //tableindex = GetTableIndex("SystemSetting", dsSystemSettings);
                    dr["GPPracticeEasting"] = "";// dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultEasting"].ToString();
                    dr["GPPracticeNorthing"] = "";//        dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultNorthing"].ToString();
                    dr["GPPracticeLatitude"] = "";//       dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultLatitude"].ToString();
                    dr["GPPracticeLongitude"] = "";//     dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultLongitude"].ToString();
                    dr["GPPracticeGridReference"] = "";//   dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultGridReference"].ToString();
                }
                else
                {
                    clsAddress objAddress = new clsAddress();
                    clsAddress.structAddress _address = new clsAddress.structAddress();
                    _address = objAddress.GetLatLongAndGridReference(Convert.ToDouble(dr["GPPracticeEasting"].ToString()), Convert.ToDouble(dr["GPPracticeNorthing"].ToString()));
                    dr["GPPracticeLatitude"] = _address.Latitude;
                    dr["GPPracticeLongitude"] = _address.Longitude;
                    dr["GPPracticeGridReference"] = _address.OSGridReference;

                }
                ///////////////-------------//////////////////
                dtPatient.Columns.Add("BookingId");

                dr["BookingId"] = 0;
                dtPatient.Rows.Add(dr);
                #endregion

                #region Home Address
                ///////dt Home Address
                foreach (var property in propertieHomeAddress)
                {
                    string propertyName = property.Name;
                    //var x= property.GetValue(property.Name,new object[]{property.GetIndexParameters()});
                    var propertyValue = property.GetValue(homeAddress, null);
                    if (propertyValue == null)
                    {
                        propertyValue = "";
                    }
                    dthomeAddress.Columns.Add(propertyName);
                    param.Add("@pHome" + propertyName, propertyValue);
                }

                dr = dthomeAddress.NewRow();
                for (int i = 0; i < dthomeAddress.Columns.Count; i++)
                {
                    dr[dthomeAddress.Columns[i].ColumnName] = param["@pHome" + dthomeAddress.Columns[i].ColumnName];

                }
                if (dr["Easting"].ToString() == "")
                {
                    //StaticCache cache = new StaticCache();
                    //DataSet dsSystemSettings = new DataSet();
                    //dsSystemSettings = cache.GetDropDowns(false);
                    //tableindex = GetTableIndex("SystemSetting", dsSystemSettings);
                    dr["Easting"] = "";//dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultEasting"].ToString();
                    dr["Northing"] = "";// dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultNorthing"].ToString();
                    dr["Latitude"] = "";//dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultLatitude"].ToString();
                    dr["Longitude"] = "";// dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultLongitude"].ToString();
                    dr["GridReference"] = "";// dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultGridReference"].ToString();
                }
                else
                {
                    clsAddress objAddress = new clsAddress();
                    clsAddress.structAddress _address = new clsAddress.structAddress();
                    _address = objAddress.GetLatLongAndGridReference(Convert.ToDouble(dr["Easting"].ToString()), Convert.ToDouble(dr["Northing"].ToString() == "" ? "0" : dr["Northing"].ToString()));
                    dr["Latitude"] = _address.Latitude;
                    dr["Longitude"] = _address.Longitude;
                    dr["GridReference"] = _address.OSGridReference;

                }
                dthomeAddress.Columns.Add("BookingId");
                dr["BookingId"] = 0;
                dthomeAddress.Rows.Add(dr);
                #endregion

                #region CollectionAddress
                ///////dtCollectionAddress
                foreach (var property in propertiescollectionAddress)
                {
                    string propertyName = property.Name;
                    //var x= property.GetValue(property.Name,new object[]{property.GetIndexParameters()});
                    var propertyValue = property.GetValue(collectionAddress, null);
                    if (propertyValue == null)
                    {
                        propertyValue = "";
                    }
                    dtCollectionaddress.Columns.Add(propertyName);
                    param.Add("@pcollection" + propertyName, propertyValue);
                }

                dr = dtCollectionaddress.NewRow();
                for (int i = 0; i < dtCollectionaddress.Columns.Count; i++)
                {
                    dr[dtCollectionaddress.Columns[i].ColumnName] = param["@pcollection" + dtCollectionaddress.Columns[i].ColumnName];

                }
                //dtCollectionaddress.Columns.Add("AddressType");
                //dr["AddressType"] = "C";//For Collection Address
                if (dr["Easting"].ToString() == "")
                {
                    //StaticCache cache = new StaticCache();
                    //DataSet dsSystemSettings = new DataSet();
                    //dsSystemSettings = cache.GetDropDowns(false);
                    //tableindex = GetTableIndex("SystemSetting", dsSystemSettings);
                    dr["Easting"] = "";//dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultEasting"].ToString();
                    dr["Northing"] = "";// dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultNorthing"].ToString();
                    dr["Latitude"] = "";//dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultLatitude"].ToString();
                    dr["Longitude"] = "";//dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultLongitude"].ToString();
                    dr["GridReference"] = "";//dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultGridReference"].ToString();
                }
                else
                {
                    clsAddress objAddress = new clsAddress();
                    clsAddress.structAddress _address = new clsAddress.structAddress();
                    _address = objAddress.GetLatLongAndGridReference(Convert.ToDouble(dr["Easting"].ToString()), Convert.ToDouble(dr["Northing"].ToString()));
                    dr["Latitude"] = _address.Latitude;
                    dr["longitude"] = _address.Longitude;
                    dr["GridReference"] = _address.OSGridReference;

                }
                dtCollectionaddress.Columns.Add("BookingId");
                dr["BookingId"] = 0;

                dtCollectionaddress.Rows.Add(dr);
                #endregion

                #region Additional Info Request
                foreach (var property in propertiesadditionalInfoRiskAssessment)
                {
                    string propertyName = property.Name;
                    //var x= property.GetValue(property.Name,new object[]{property.GetIndexParameters()});
                    var propertyValue = property.GetValue(additionalInfoRiskAssessment, null);
                    if (propertyValue == null)
                    {
                        propertyValue = "";
                    }
                    dtadditionalInfoRiskAssessment.Columns.Add(propertyName);
                    param.Add("@priskassess" + propertyName, propertyValue);
                }

                dr = dtadditionalInfoRiskAssessment.NewRow();
                for (int i = 0; i < dtadditionalInfoRiskAssessment.Columns.Count; i++)
                {
                    dr[dtadditionalInfoRiskAssessment.Columns[i].ColumnName] = param["@priskassess" + dtadditionalInfoRiskAssessment.Columns[i].ColumnName];

                }
                dtadditionalInfoRiskAssessment.Columns.Add("BookingId");

                dr["BookingId"] = 0;

                dtadditionalInfoRiskAssessment.Rows.Add(dr);
                #endregion

                #region destinationAddress
                foreach (var property in propertiesdestinationAddress)
                {
                    string propertyName = property.Name;
                    //var x= property.GetValue(property.Name,new object[]{property.GetIndexParameters()});
                    var propertyValue = property.GetValue(destinationAddress, null);
                    if (propertyValue == null)
                    {
                        propertyValue = "";
                    }
                    dtdestinationAddress.Columns.Add(propertyName);
                    param.Add("@pdestination" + propertyName, propertyValue);
                }

                dr = dtdestinationAddress.NewRow();
                for (int i = 0; i < dtdestinationAddress.Columns.Count; i++)
                {
                    dr[dtdestinationAddress.Columns[i].ColumnName] = param["@pdestination" + dtdestinationAddress.Columns[i].ColumnName];

                }
                //dr["AddressType"] = "D";//For Destination Address
                if (dr["Easting"].ToString() == "")
                {
                    //StaticCache cache = new StaticCache();
                    ////DataSet dsSystemSettings = new DataSet();
                    ////dsSystemSettings = cache.GetDropDowns(false);
                    //tableindex = GetTableIndex("SystemSetting", dsSystemSettings);
                    dr["Easting"] = "";// dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultEasting"].ToString();
                    dr["Northing"] = "";// dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultNorthing"].ToString();
                    dr["Latitude"] = "";//dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultLatitude"].ToString();
                    dr["Longitude"] = "";//dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultLongitude"].ToString();
                    dr["GridReference"] = "";// dsSystemSettings.Tables[tableindex].Rows[0]["AddressDefaultGridReference"].ToString();
                }
                else
                {
                    clsAddress objAddress = new clsAddress();
                    clsAddress.structAddress _address = new clsAddress.structAddress();
                    _address = objAddress.GetLatLongAndGridReference(Convert.ToDouble(dr["Easting"].ToString()), Convert.ToDouble(dr["Northing"].ToString()));
                    dr["Latitude"] = _address.Latitude;
                    dr["Longitude"] = _address.Longitude;
                    dr["GridReference"] = _address.OSGridReference;

                }
                dtdestinationAddress.Columns.Add("BookingId");
                dr["BookingId"] = 0;
                dtdestinationAddress.Rows.Add(dr);

                #endregion

                #region transport Requirement
                foreach (var property in propertietransportRequirement)
                {
                    string propertyName = property.Name;
                    //var x= property.GetValue(property.Name,new object[]{property.GetIndexParameters()});
                    var propertyValue = property.GetValue(transportRequirement, null);
                    if (propertyValue == null)
                    {
                        propertyValue = "";
                    }
                    dttransportRequirement.Columns.Add(propertyName);
                    param.Add("@ptransportreq" + propertyName, propertyValue);
                }

                dr = dttransportRequirement.NewRow();
                for (int i = 0; i < dttransportRequirement.Columns.Count; i++)
                {
                    dr[dttransportRequirement.Columns[i].ColumnName] = param["@ptransportreq" + dttransportRequirement.Columns[i].ColumnName];

                }
                dttransportRequirement.Columns.Add("BookingId");
                dr["BookingId"] = 0;
                dttransportRequirement.Rows.Add(dr);

                #endregion

                #region Specialist Transport Request
                foreach (var property in propertiespecialistTransportRequest)
                {
                    string propertyName = property.Name;
                    //var x= property.GetValue(property.Name,new object[]{property.GetIndexParameters()});
                    var propertyValue = property.GetValue(specialistTransportRequest, null);
                    if (propertyValue == null)
                    {
                        propertyValue = "";
                    }
                    dtspecialistTransportRequest.Columns.Add(propertyName);
                    param.Add("@pspecialtrans" + propertyName, propertyValue);
                }

                dr = dtspecialistTransportRequest.NewRow();
                for (int i = 0; i < dtspecialistTransportRequest.Columns.Count; i++)
                {
                    dr[dtspecialistTransportRequest.Columns[i].ColumnName] = param["@pspecialtrans" + dtspecialistTransportRequest.Columns[i].ColumnName];

                }
                dtspecialistTransportRequest.Columns.Add("BookingId");
                dr["BookingId"] = 0;
                dtspecialistTransportRequest.Rows.Add(dr);
                #endregion
                //dtPatient.Merge(dthomeAddress);
                //ds.Tables.Add(dtPatient);
                //ds.Tables.Add(dthomeAddress);
                //ds.Tables.Add(dtCollectionaddress);
                //ds.Tables.Add(dtdestinationAddress);
                //ds.Tables.Add(dtadditionalInfoRiskAssessment);
                //ds.Tables.Add(dttransportRequirement);
                //ds.Tables.Add(dtspecialistTransportRequest);
                #endregion

                #region Table Creation for Database
                #region Patient Table
                DataTable dtPatientdb = new DataTable("ptblPatient");
                dtPatientdb.Columns.Add("PatientId");
                dtPatientdb.Columns.Add("TitleId");
                dtPatientdb.Columns.Add("FirstName");
                dtPatientdb.Columns.Add("SurName");
                dtPatientdb.Columns.Add("DOB", typeof(DateTime));
                dtPatientdb.Columns.Add("NHSNumber");
                dtPatientdb.Columns.Add("IOWNumber");
                dtPatientdb.Columns.Add("HasnoAdobe");
                dtPatientdb.Columns.Add("AddressLine1");
                dtPatientdb.Columns.Add("AddressLine2");
                dtPatientdb.Columns.Add("AddressLine3");
                dtPatientdb.Columns.Add("AddressLine4");
                dtPatientdb.Columns.Add("PostCode");
                dtPatientdb.Columns.Add("URPN");
                dtPatientdb.Columns.Add("Latitude");
                dtPatientdb.Columns.Add("Longitude");
                dtPatientdb.Columns.Add("Easting");
                dtPatientdb.Columns.Add("Northing");
                dtPatientdb.Columns.Add("GridReference");
                dtPatientdb.Columns.Add("HomePhone");
                dtPatientdb.Columns.Add("AlternatePhone");
                dtPatientdb.Columns.Add("RelationShipID");
               


                //dtPatientdb.Columns.Add("UserID");
                dtPatientdb.Columns.Add("IsDeleted");
                dtPatientdb.Columns.Add("isPrivatePatient");

                dr = dtPatientdb.NewRow();
                dr["PatientId"] = Convert.ToInt32(dtPatient.Rows[0]["PatientID"].ToString());
                dr["TitleID"] = Convert.ToInt16(dtPatient.Rows[0]["GenderTitleID"].ToString());
                dr["FirstName"] = dtPatient.Rows[0]["FirstName"].ToString().Trim();
                dr["SurName"] = dtPatient.Rows[0]["SurName"].ToString().Trim();
                dr["DOB"] = Convert.ToDateTime(dtPatient.Rows[0]["BirthDate"].ToString().Trim());//, new CultureInfo("ur-pk", false));
                dr["NHSNumber"] = dtPatient.Rows[0]["NHSNumber"].ToString().Trim();
                dr["IOWNumber"] = dtPatient.Rows[0]["IsleOfWightNo"].ToString().Trim();
                dr["HasNoAdobe"] = dthomeAddress.Rows[0]["IsNoFixAbode"].ToString().Trim();
                dr["AddressLine1"] = dthomeAddress.Rows[0]["LineOne"].ToString().Trim();
                dr["AddressLine2"] = dthomeAddress.Rows[0]["LineTwo"].ToString().Trim();
                dr["AddressLine3"] = dthomeAddress.Rows[0]["LineThree"].ToString().Trim();
                dr["AddressLine4"] = dthomeAddress.Rows[0]["LineFour"].ToString().Trim();
                dr["PostCode"] = dthomeAddress.Rows[0]["PostCode"].ToString().Trim();
                dr["URPN"] = dthomeAddress.Rows[0]["UPRN"].ToString().Trim();
                dr["Latitude"] = dthomeAddress.Rows[0]["Latitude"].ToString().Trim();
                dr["Longitude"] = dthomeAddress.Rows[0]["Longitude"].ToString().Trim();
                dr["Easting"] = dthomeAddress.Rows[0]["Easting"].ToString().Trim();
                dr["Northing"] = dthomeAddress.Rows[0]["Northing"].ToString().Trim();
                dr["GridReference"] = dthomeAddress.Rows[0]["GridReference"].ToString().Trim();
                dr["HomePhone"] = dthomeAddress.Rows[0]["ContactTelNo"].ToString().Trim();
                dr["AlternatePhone"] = dthomeAddress.Rows[0]["AlternateContactTelNo"].ToString().Trim();
                dr["RelationShipID"] = dthomeAddress.Rows[0]["RelationshipId"].ToString().Trim();
                
                dr["IsDeleted"] = false;
                dr["isPrivatePatient"] = dtPatient.Rows[0]["isPrivatePatient"].ToString().Trim();
                dtPatientdb.Rows.Add(dr);
                dtPatientdb.AcceptChanges();
                #endregion

                #region Patient Booking Table
                DataTable dtPatientBookingdb = new DataTable("ptblPatientBooking");
                dtPatientBookingdb.Columns.Add("BookingId");
                dtPatientBookingdb.Columns.Add("PatientId");
                dtPatientBookingdb.Columns.Add("IsFormainLand");
                dtPatientBookingdb.Columns.Add("IsRiskAssessmentRequired");
                dtPatientBookingdb.Columns.Add("PropertyRiskAssessment");
                dtPatientBookingdb.Columns.Add("PatientRiskAssessment");
                dtPatientBookingdb.Columns.Add("IsFullManualHandling");
                dtPatientBookingdb.Columns.Add("BookingDate", typeof(DateTime));

                dtPatientBookingdb.Columns.Add("JourneyDate", typeof(DateTime));
                dtPatientBookingdb.Columns.Add("StandardAppointmentTimeID");
                dtPatientBookingdb.Columns.Add("ActualAppointmentTime");
                dtPatientBookingdb.Columns.Add("EstimatedDurationofAppointment");
                dtPatientBookingdb.Columns.Add("Weight");
                dtPatientBookingdb.Columns.Add("WeightDate", typeof(DateTime));
                dtPatientBookingdb.Columns.Add("TransportRequestReasonID");
                dtPatientBookingdb.Columns.Add("TransportSelectionID");
                //dtPatientBookingdb.Columns.Add("WeightDate");

                dtPatientBookingdb.Columns.Add("GPName");

                dtPatientBookingdb.Columns.Add("GPPracticeID");
                

                dtPatientBookingdb.Columns.Add("GPPhoneNo");
                dtPatientBookingdb.Columns.Add("IsPickHomeAddress");
                dtPatientBookingdb.Columns.Add("PickFacilityID");
                dtPatientBookingdb.Columns.Add("PickDepartmentID");
                dtPatientBookingdb.Columns.Add("PickAddressLine1");
                dtPatientBookingdb.Columns.Add("PickAddressLine2");
                dtPatientBookingdb.Columns.Add("PickAddressLine3");
                dtPatientBookingdb.Columns.Add("PickAddressLine4");
                dtPatientBookingdb.Columns.Add("PickPostCode");
                dtPatientBookingdb.Columns.Add("PickURPN");
                dtPatientBookingdb.Columns.Add("PickLatitude");

                dtPatientBookingdb.Columns.Add("PickLongitude");
                dtPatientBookingdb.Columns.Add("PickEasting");
                dtPatientBookingdb.Columns.Add("PickNorthing");
                dtPatientBookingdb.Columns.Add("PickGridReference");
                dtPatientBookingdb.Columns.Add("PickContactPhone");
                dtPatientBookingdb.Columns.Add("PickExtention");
                dtPatientBookingdb.Columns.Add("IsDropHomeAddress");
                dtPatientBookingdb.Columns.Add("DropFacilityID");
                dtPatientBookingdb.Columns.Add("DropDepartmentID");
                dtPatientBookingdb.Columns.Add("DropAddressLine1");
                dtPatientBookingdb.Columns.Add("DropAddressLine2");
                dtPatientBookingdb.Columns.Add("DropAddressLine3");
                dtPatientBookingdb.Columns.Add("DropAddressLine4");
                dtPatientBookingdb.Columns.Add("DropPostCode");
                dtPatientBookingdb.Columns.Add("DropURPN");
                dtPatientBookingdb.Columns.Add("DropLatitude");

                dtPatientBookingdb.Columns.Add("DropLongitude");
                dtPatientBookingdb.Columns.Add("DropEasting");
                dtPatientBookingdb.Columns.Add("DropNorthing");
                dtPatientBookingdb.Columns.Add("DropGridReference");
                dtPatientBookingdb.Columns.Add("DropContactPhone");
                dtPatientBookingdb.Columns.Add("DropExtention");


                dtPatientBookingdb.Columns.Add("IsEligible");
                dtPatientBookingdb.Columns.Add("SendEmailtoUser");
                dtPatientBookingdb.Columns.Add("IsPatientInfectious");
                dtPatientBookingdb.Columns.Add("InfectionID");
                dtPatientBookingdb.Columns.Add("TravellingWithOwnOxygen");
                dtPatientBookingdb.Columns.Add("EscortTravelling");
                dtPatientBookingdb.Columns.Add("EscortType");
                dtPatientBookingdb.Columns.Add("EscortNumber");
                dtPatientBookingdb.Columns.Add("Bariatric");
                dtPatientBookingdb.Columns.Add("FullLegPlaster");
                dtPatientBookingdb.Columns.Add("ElectricWheelChair");
                dtPatientBookingdb.Columns.Add("WheelChairLegExtention");
                dtPatientBookingdb.Columns.Add("WaterFlow");
                dtPatientBookingdb.Columns.Add("DNACPR");
                dtPatientBookingdb.Columns.Add("Diabetic");
                dtPatientBookingdb.Columns.Add("NuclearMedicine");
                dtPatientBookingdb.Columns.Add("Non");
                dtPatientBookingdb.Columns.Add("IsDeleted");
                dtPatientBookingdb.Columns.Add("BookingStatus");
                dtPatientBookingdb.Columns.Add("StatusDateTime", typeof(DateTime));
                dtPatientBookingdb.Columns.Add("SubjectiveCode");
                dtPatientBookingdb.Columns.Add("CostCenter");
                dtPatientBookingdb.Columns.Add("AuthoirizingClinican");
                dtPatientBookingdb.Columns.Add("ClinicianRole");
                dtPatientBookingdb.Columns.Add("PatientAdditionalInfo");
                dtPatientBookingdb.Columns.Add("TwentyFourHourUpdate");
                dtPatientBookingdb.Columns.Add("GPPracticeAddressLine1");
                dtPatientBookingdb.Columns.Add("GPPracticeAddressLine2");
                dtPatientBookingdb.Columns.Add("GPPracticeAddressLine3");
                dtPatientBookingdb.Columns.Add("GPPracticeAddressLine4");
                dtPatientBookingdb.Columns.Add("GPPracticePostCode");
                dtPatientBookingdb.Columns.Add("GPPracticeEasting");
                dtPatientBookingdb.Columns.Add("GPPracticeNorthing");
                dtPatientBookingdb.Columns.Add("GpPracticeLatitude");
                dtPatientBookingdb.Columns.Add("GpPracticeLongitude");
                dtPatientBookingdb.Columns.Add("GpPracticeGridReference");
                dtPatientBookingdb.Columns.Add("GpPracticeURPN");
                dtPatientBookingdb.Columns.Add("IsPrivatePatient");

               


                dr = dtPatientBookingdb.NewRow();
                dr["BookingId"] = dttransportRequirement.Rows[0]["Id"].ToString().Trim();
                dr["PatientId"] = dtPatient.Rows[0]["PatientID"].ToString().Trim();
                dr["IsFormainLand"] = dtPatient.Rows[0]["IsMainlandRepatriation"].ToString();
                dr["IsRiskAssessmentRequired"] = dtPatient.Rows[0]["IsRiskAssessmentRequired"].ToString().Trim();
                dr["PropertyRiskAssessment"] = dtadditionalInfoRiskAssessment.Rows[0]["PropertyRiskAssessment"].ToString().Trim();
                dr["PatientRiskAssessment"] = dtadditionalInfoRiskAssessment.Rows[0]["PatientRiskAssessment"].ToString().Trim();
                dr["IsFullManualHandling"] = dtadditionalInfoRiskAssessment.Rows[0]["IsManualHandlingProfileCarriedOutYes"].ToString().Trim();
                //dr["BookingDate"] = dtPatient.Rows[0]["BirthDate"].ToString().Trim();// System.DateTime.Now.Date;// dtPatient.Rows[0]["IsleOfWightNo"].ToString().Trim();
                dr["StandardAppointmentTimeID"] = dtPatient.Rows[0]["AppointmentTimeId"].ToString().Trim();
                dr["JourneyDate"] = Convert.ToDateTime(dtPatient.Rows[0]["JourneyDate"].ToString().Trim());//, new CultureInfo("ur-pk", false));
                dr["ActualAppointmentTime"] = dtPatient.Rows[0]["ActualAppointmentTime"].ToString().Trim();
                dr["GPName"] = dtPatient.Rows[0]["NameOfGP"].ToString().Trim();// dtPatient.Rows[0]["LineThree"].ToString().Trim();
                dr["EstimatedDurationofAppointment"] = dtPatient.Rows[0]["EstimatedAppointmentDurationId"].ToString().Trim();
                dr["Weight"] = dtPatient.Rows[0]["LastRecordedPatientWeight"].ToString().Trim();
                DateTime outDate = System.DateTime.Now;
                //DateTime.TryParse(dtPatient.Rows[0]["WeighingDate"].ToString().Trim(), out outDate);
                if (DateTime.TryParse(dtPatient.Rows[0]["WeighingDate"].ToString().Trim(), out outDate))
                    dr["WeightDate"] = outDate;//,new CultureInfo("ur-pk",false));
                dr["TransportRequestReasonID"] = dttransportRequirement.Rows[0]["TransportRequestReasonId"].ToString().Trim();
                dr["TransportSelectionID"] = dttransportRequirement.Rows[0]["TransportSelectionId"].ToString().Trim();
                dr["GPPracticeID"] = dtPatient.Rows[0]["GPPracticeId"].ToString().Trim();
                dr["GPPhoneNo"] = dtPatient.Rows[0]["ContactTelephoneNo"].ToString().Trim();

                dr["IsPickHomeAddress"] = dtCollectionaddress.Rows[0]["IsThisPatientHomeAddress"].ToString().Trim();
                dr["PickFacilityID"] = dtCollectionaddress.Rows[0]["FacilityId"].ToString().Trim();
                dr["PickDepartmentID"] = dtCollectionaddress.Rows[0]["DepartmentId"].ToString().Trim();
                dr["PickAddressLine1"] = dtCollectionaddress.Rows[0]["LineOne"].ToString().Trim();
                dr["PickAddressLine2"] = dtCollectionaddress.Rows[0]["LineTwo"].ToString().Trim();
                dr["PickAddressLine3"] = dtCollectionaddress.Rows[0]["LineThree"].ToString().Trim();
                dr["PickAddressLine4"] = dtCollectionaddress.Rows[0]["LineFour"].ToString().Trim();
                dr["PickPostCode"] = dtCollectionaddress.Rows[0]["PostCode"].ToString().Trim();
                dr["PickURPN"] = dtCollectionaddress.Rows[0]["UPRN"].ToString().Trim();
                dr["PickLatitude"] = dtCollectionaddress.Rows[0]["Latitude"].ToString().Trim();
                dr["PickLongitude"] = dtCollectionaddress.Rows[0]["Longitude"].ToString().Trim();
                dr["PickEasting"] = dtCollectionaddress.Rows[0]["Easting"].ToString().Trim();
                dr["PickNorthing"] = dtCollectionaddress.Rows[0]["Northing"].ToString().Trim();
                dr["PickGridReference"] = dtCollectionaddress.Rows[0]["GridReference"].ToString().Trim();
                dr["PickContactPhone"] = dtCollectionaddress.Rows[0]["ContactTelNo"].ToString().Trim();
                dr["PickExtention"] = dtCollectionaddress.Rows[0]["ExtensionNo"].ToString().Trim();

                dr["IsDropHomeAddress"] = dtdestinationAddress.Rows[0]["IsThisPatientHomeAddress"].ToString().Trim();
                dr["DropFacilityID"] = dtdestinationAddress.Rows[0]["FacilityId"].ToString().Trim();
                dr["DropDepartmentID"] = dtdestinationAddress.Rows[0]["DepartmentId"].ToString().Trim();
                dr["DropAddressLine1"] = dtdestinationAddress.Rows[0]["LineOne"].ToString().Trim();
                dr["DropAddressLine2"] = dtdestinationAddress.Rows[0]["LineTwo"].ToString().Trim();
                dr["DropAddressLine3"] = dtdestinationAddress.Rows[0]["LineThree"].ToString().Trim();
                dr["DropAddressLine4"] = dtdestinationAddress.Rows[0]["LineFour"].ToString().Trim();
                dr["DropPostCode"] = dtdestinationAddress.Rows[0]["PostCode"].ToString().Trim();
                dr["DropURPN"] = dtdestinationAddress.Rows[0]["UPRN"].ToString().Trim();
                dr["DropLatitude"] = dtdestinationAddress.Rows[0]["Latitude"].ToString().Trim();
                dr["DropLongitude"] = dtdestinationAddress.Rows[0]["Longitude"].ToString().Trim();
                dr["DropEasting"] = dtdestinationAddress.Rows[0]["Easting"].ToString().Trim();
                dr["DropNorthing"] = dtdestinationAddress.Rows[0]["Northing"].ToString().Trim();
                dr["DropGridReference"] = dtdestinationAddress.Rows[0]["GridReference"].ToString().Trim();
                dr["DropContactPhone"] = dtdestinationAddress.Rows[0]["ContactTelNo"].ToString().Trim();
                dr["DropExtention"] = dtdestinationAddress.Rows[0]["ExtensionNo"].ToString().Trim();


                // dr["IsEligible"] = ; //dthomeAddress.Rows[0]["Latitude"].ToString().Trim();
                dr["SendEmailtoUser"] = true;// dthomeAddress.Rows[0]["Longitude"].ToString().Trim();
                dr["IsPatientInfectious"] = dttransportRequirement.Rows[0]["IsInfectious"].ToString().Trim();
                dr["InfectionID"] = dttransportRequirement.Rows[0]["InfectiousId"].ToString().Trim();
                dr["TravellingWithOwnOxygen"] = dttransportRequirement.Rows[0]["IsTravellingWithOwnOxygen"].ToString().Trim();
                dr["EscortTravelling"] = dttransportRequirement.Rows[0]["IsEscortTravelling"].ToString().Trim();
                dr["EscortType"] = dttransportRequirement.Rows[0]["EscortTypeId"].ToString().Trim();
                dr["EscortNumber"] = dttransportRequirement.Rows[0]["EscortNumberId"].ToString().Trim();
                dr["Bariatric"] = dttransportRequirement.Rows[0]["IsBariatric"].ToString().Trim();
                dr["FullLegPlaster"] = dttransportRequirement.Rows[0]["IsFullLegPlasterPOP"].ToString().Trim();
                dr["ElectricWheelChair"] = dttransportRequirement.Rows[0]["IsElectricWheelchair"].ToString().Trim();
                dr["WheelChairLegExtention"] = dttransportRequirement.Rows[0]["IsWheelchairAndLegExtension"].ToString().Trim();
                dr["WaterFlow"] = dttransportRequirement.Rows[0]["IsWaterlow"].ToString().Trim();
                dr["DNACPR"] = dttransportRequirement.Rows[0]["IsDNACPR"].ToString().Trim();
                dr["Diabetic"] = dttransportRequirement.Rows[0]["IsDiabetic"].ToString().Trim();
                dr["NuclearMedicine"] = dttransportRequirement.Rows[0]["IsNuclearMedicineRadioActiveRisk"].ToString().Trim();
                dr["Non"] = dttransportRequirement.Rows[0]["IsNoneOfAbove"].ToString().Trim();
                dr["IsDeleted"] = false;
                dr["BookingStatus"] = 1;
                dr["StatusDateTime"] = Convert.ToDateTime(System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt"), new CultureInfo("ur-pk", false));// "";// 
                dr["SubjectiveCode"] = dtPatient.Rows[0]["RequesterSubjectiveCode"].ToString().Trim();
                dr["CostCenter"] = dtPatient.Rows[0]["RequesterCostCenter"].ToString();
                dr["AuthoirizingClinican"] = dtPatient.Rows[0]["RequesterAuthorizingClinician"].ToString();
                dr["ClinicianRole"] = dtPatient.Rows[0]["RequesterAuthorizingRoleId"].ToString();
                dr["PatientAdditionalInfo"] = dttransportRequirement.Rows[0]["AdditionalPatientInfo"].ToString().Trim();
                dr["TwentyFourHourUpdate"] = dttransportRequirement.Rows[0]["Is24HourAmendment"].ToString().Trim();

                dr["GPPracticeAddressLine1"] = dtPatient.Rows[0]["GPPracticeAddressLineOne"].ToString();
                dr["GPPracticeAddressLine2"] = dtPatient.Rows[0]["GPPracticeAddressLineTwo"].ToString();
                dr["GPPracticeAddressLine3"] = dtPatient.Rows[0]["GPPracticeAddressLineThree"].ToString();
                dr["GPPracticeAddressLine4"] = dtPatient.Rows[0]["GPPracticeAddressLineFour"].ToString();
                dr["GPPracticePostCode"] = dtPatient.Rows[0]["GPPracticeAddressPostCode"].ToString();
                dr["GPPracticeEasting"] = dtPatient.Rows[0]["GPPracticeEasting"].ToString();
                dr["GPPracticeNorthing"] = dtPatient.Rows[0]["GPPracticeNorthing"].ToString();
                dr["GpPracticeLatitude"] = dtPatient.Rows[0]["GpPracticeLatitude"].ToString();
                dr["GpPracticeLongitude"] = dtPatient.Rows[0]["GpPracticeLongitude"].ToString();
                dr["GpPracticeGridReference"] = dtPatient.Rows[0]["GpPracticeGridReference"].ToString();
                dr["GpPracticeURPN"] = dtPatient.Rows[0]["GpPracticeURPN"].ToString();
                dr["isPrivatePatient"] = dtPatient.Rows[0]["isPrivatePatient"].ToString().Trim();
                dtPatientBookingdb.Rows.Add(dr);
                if (patient.ComplexJourney)
                {
                    dtPatientBookingdb.Rows.Add(dr.ItemArray);
                    dtPatientBookingdb.Rows[1]["JourneyDate"] = patient.JourneyDate2;
                }
                dtPatientBookingdb.AcceptChanges();

                #endregion

                #region Booking Special Requirement Table
                DataTable dtBookingSpReqDb = new DataTable("ptblBookingSpecialRequirement");
                dtBookingSpReqDb.Columns.Add("SpecialRequirementId");
                dtBookingSpReqDb.Columns.Add("BookingId");
                dtBookingSpReqDb.Columns.Add("TrainedPersonRequired");
                dtBookingSpReqDb.Columns.Add("WhilstlayingDown");
                dtBookingSpReqDb.Columns.Add("OxygenTherapyRequired");
                dtBookingSpReqDb.Columns.Add("HasDisability");
                dtBookingSpReqDb.Columns.Add("IsAdmissionToNursing");
                dtBookingSpReqDb.Columns.Add("VisitToHospitalorHospice");
                dtBookingSpReqDb.Columns.Add("VisitToandFromResidentialCareHome");
                dtBookingSpReqDb.Columns.Add("AuthorizingConsultant");
                dtBookingSpReqDb.Columns.Add("AuthorizingPractice");
                dtBookingSpReqDb.Columns.Add("IsDeleted");

                dr = dtBookingSpReqDb.NewRow();

                dr["SpecialRequirementId"] = 0;// dthomeAddress.Rows[0]["Longitude"].ToString().Trim();
                dr["BookingId"] = dttransportRequirement.Rows[0]["Id"].ToString().Trim();// dttransportRequirement.Rows[0]["IsInfectious"].ToString().Trim();
                dr["TrainedPersonRequired"] = dtspecialistTransportRequest.Rows[0]["IsHandledByProfessional"].ToString().Trim();
                dr["WhilstlayingDown"] = dtspecialistTransportRequest.Rows[0]["IsWhilstLayingDown"].ToString().Trim();
                dr["OxygenTherapyRequired"] = dtspecialistTransportRequest.Rows[0]["IsOxygenTheropy"].ToString().Trim();
                dr["HasDisability"] = dtspecialistTransportRequest.Rows[0]["IsPrecludesTravelling"].ToString().Trim();
                dr["IsAdmissionToNursing"] = dtspecialistTransportRequest.Rows[0]["IsAdmission"].ToString().Trim();
                dr["VisitToHospitalorHospice"] = dtspecialistTransportRequest.Rows[0]["IsVisitOrAdmitted"].ToString().Trim();
                dr["VisitToandFromResidentialCareHome"] = dtspecialistTransportRequest.Rows[0]["IsVisit"].ToString().Trim();
                dr["AuthorizingConsultant"] = dtspecialistTransportRequest.Rows[0]["AuthorisingConsultantOrGP"].ToString().Trim();
                dr["AuthorizingPractice"] = dtspecialistTransportRequest.Rows[0]["AuthorisingPracticeName"].ToString().Trim();
                dr["IsDeleted"] = false;// dthomeAddress.Rows[0]["IsWaterlow"].ToString().Trim();

                dtBookingSpReqDb.Rows.Add(dr);
                if (patient.ComplexJourney)
                {
                    dtBookingSpReqDb.Rows.Add(dr.ItemArray);
                    // dtBookingSpReqDb.Rows[1]["JourneyDate"] = patient.JourneyDate2;
                }
                dtBookingSpReqDb.AcceptChanges();
                #endregion
                ds.Tables.Add(dtPatientdb);
                ds.Tables.Add(dtPatientBookingdb);
                ds.Tables.Add(dtBookingSpReqDb);
                ds.AcceptChanges();


                #endregion



                clsEBooking objEBooking = new clsEBooking();
                try
                {
                    objEBooking.AddorUpdateJourney(ds, out refNumber, userid, out BookingNumber);
                }
                catch (Exception ee)
                {
                    refNumber = "-1";
                    BookingNumber = "-1";
                    throw new Exception(ee.ToString());
                }
                // var result = new { refNum = refNumber, BookingNo = BookingNumber };
                //return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Get

        public CompositeModel GetJourney(string referenceNumber)
        {
            //CompositeModel modeljourney = new CompositeModel();
            List<CompositeModel> journey = GetJourneyListFiltered("1,2,3,4", "", "", referenceNumber);
           //  modeljourney = journey.Find(x => x.Patient.BookingNo.Contains(referenceNumber));
           // RequestertModel RequesterData =
            //modeljourney.Requester = GetRequesterData();
            return journey.Find(x => x.Patient.BookingNo.Contains(referenceNumber));
        }

        public jouneyCompositePDF GetJourneyPDF(string referenceNumber)
        {
            jouneyCompositePDF modeljourney = new jouneyCompositePDF();
            clsEBooking objEbooking = new clsEBooking();
            DataSet ds_Journeypdf = objEbooking.GetJourneyPDF(referenceNumber);
            if (ds_Journeypdf.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds_Journeypdf.Tables[0].Rows[0];
                #region Patient
                //patient = new PatientModel();
                //journey = new CompositeModel();
                //patient.PatientID = Convert.ToInt32(dr["PatientId"]);
                modeljourney.IsRiskAssessmentRequired = (Boolean)dr["IsRiskAssessmentRequired"];
                modeljourney.IsMainlandRepatriation = (Boolean)dr["IsFormainLand"];
                try
                {
                    modeljourney.JourneyDate = Convert.ToDateTime(dr["JourneyDate"]);
                    modeljourney.LastStatusAt = Convert.ToDateTime(dr["LastStatusAt"]);
                }
                catch { }
                try
                {
                    modeljourney.StandardAppointmentTime = dr["StandardAppointmentTime"].ToString().Trim();
                    modeljourney.AppointmentDuration = dr["AppointmentDuration"].ToString().Trim();
                }
                catch { }
                modeljourney.ActualAppointmentTime = dr["ActualAppointmentTime"].ToString().Substring(0, 5);
               // patient.GenderTitleId = Convert.ToInt32(dr["TitleId"]);
                modeljourney.FirstName = (string)dr["FirstName"];
                modeljourney.Surname = (string)dr["SurName"];
                try
                {
                    modeljourney.BirthDate = Convert.ToDateTime(dr["DOB"]);

                }
                catch { }
                modeljourney.NHSNumber = (string)dr["NHSNumber"];
                modeljourney.IsleOfWightNo = (string)dr["IOWNumber"];
                modeljourney.NameOfGP = (string)dr["GPName"];
                try
                {


                    modeljourney.WeighingDate = Convert.ToDateTime(dr["WeightDate"]);
                    modeljourney.LastRecordedPatientWeight = Convert.ToDouble(dr["Weight"]);
                }
                catch { }
                try
                {
                    modeljourney.GpPracticeName = dr["GpPracticeName"].ToString().Trim();

                }
                catch { }
                modeljourney.GPPracticeAddressLineOne = dr["GpAddressLine1"].ToString().Trim();
                modeljourney.GPPracticeAddressLineTwo = dr["GpAddressLine2"].ToString().Trim();
                modeljourney.GPPracticeAddressLineThree = dr["GpAddressLine3"].ToString().Trim();
                modeljourney.GPPracticeAddressLineFour = dr["GpAddressLine4"].ToString().Trim();
                modeljourney.GPPracticeAddressPostCode = dr["GpAddressPostCode"].ToString().Trim();
                modeljourney.ContactTelephoneNo = dr["GPPhoneNo"].ToString().Trim();
                modeljourney.BookingNumber = dr["BookingNumber"].ToString();
                modeljourney.BookingStatusName = dr["CaseID"].ToString().Trim() == "" ? dr["BookingStatusName"].ToString() : "Amendment Request";
                modeljourney.RequesterSubjectiveCode = dr["SubjectiveCode"].ToString();
                modeljourney.RequesterCostCenter = dr["CostCenter"].ToString();
                modeljourney.RequesterAuthorizingClinician = dr["AuthoirizingClinical"].ToString();
                modeljourney.ClinicianRoleName = dr["ClinicianRoleName"].ToString().Trim();
               // modeljourney.RejectionReason = dr["LastBookingStatusNotes"].ToString().Trim().ToLower().IndexOf("new booking") >= 0 ? "" : dr["LastBookingStatusNotes"].ToString().Trim();
               // modeljourney.LastStatusAt = Convert.ToDateTime(dr["LastStatusAt"]);
               // modeljourney.Patient = patient;
                #endregion
                #region HomeAddress

               // homeAddress = new HomeAddressModel();


                modeljourney.IsNoFixAbode = (Boolean)dr["HasNoAdobe"];
                modeljourney.HomeLineOne = dr["AddressLine1"].ToString().Trim();
                modeljourney.HomeLineTwo = dr["AddressLine2"].ToString().Trim();
                modeljourney.HomeLineThree = dr["AddressLine3"].ToString().Trim();
                modeljourney.HomeLineFour = dr["AddressLine4"].ToString().Trim();
                modeljourney.HomePostCode = dr["PostCode"].ToString().Trim();

                //homeAddress.Easting = dr["Easting"].ToString().Trim();// Convert.ToDouble(dr["PatientEasting"].ToString() == "" ? "0" : dr["PatientEasting"].ToString().Trim());
                //homeAddress.Northing = dr["Northing"].ToString().Trim();
                //homeAddress.GridReference = dr["GridReference"].ToString().Trim();
                //homeAddress.Longitude = dr["Longitude"].ToString().Trim();
                //homeAddress.Latitude = dr["Latitude"].ToString().Trim();
                //homeAddress.UPRN = dr["URPN"].ToString().Trim();

                modeljourney.HomePhone= dr["HomePhone"].ToString().Trim();
                modeljourney.AlternatePhone= dr["AlternatePhone"].ToString().Trim();
                
                modeljourney.RelationshiptoPatient = dr["RelationshiptoPatient"].ToString().Trim();
                
               
               // journey.HomeAddress = homeAddress;
                #endregion

                #region CollectionAddress
                //collectionAddress = new CollectionAddressModel();

                modeljourney.PickIsThisPatientHomeAddress = (Boolean)dr["IsPickHomeAddress"];
                modeljourney.PickLineOne = dr["PickAddressLine1"].ToString().Trim();
                modeljourney.PickLineTwo = dr["PickAddressLine2"].ToString().Trim();
                modeljourney.PickLineThree = dr["PickAddressLine3"].ToString().Trim();
                modeljourney.PickLineFour = dr["PickAddressLine4"].ToString().Trim();
                modeljourney.PickPostCode = dr["PickPostCode"].ToString().Trim();

                //collectionAddress.Easting = dr["PickEasting"].ToString().Trim();// Convert.ToDouble(dr["PatientEasting"].ToString() == "" ? "0" : dr["PatientEasting"].ToString().Trim());
                //collectionAddress.Northing = dr["PickNorthing"].ToString().Trim();
                //collectionAddress.GridReference = dr["PickGridReference"].ToString().Trim();
                //collectionAddress.Longitude = dr["PickLongitude"].ToString().Trim();
                //collectionAddress.Latitude = dr["PickLatitude"].ToString().Trim();
                //collectionAddress.UPRN = dr["PickURPN"].ToString().Trim();

                modeljourney.PickContactTelNo= dr["PickContactPhone"].ToString().Trim();
                modeljourney.PickExtensionNo = dr["PickExtention"].ToString().Trim();
                
                modeljourney.PickFacilityType = dr["PickFacilityType"].ToString().Trim();
                modeljourney.PickFacility = dr["PickFacility"].ToString().Trim();
                modeljourney.PickFacilityDepartment = dr["PickFacilityDepartment"].ToString().Trim();
                
           

                #endregion

                #region DestinationAddress
                modeljourney.DropIsThisPatientHomeAddress = (Boolean)dr["IsDropHomeAddress"];
                modeljourney.DropLineOne = dr["DropAddressLine1"].ToString().Trim();
                modeljourney.DropLineTwo = dr["DropAddressLine2"].ToString().Trim();
                modeljourney.DropLineThree = dr["DropAddressLine3"].ToString().Trim();
                modeljourney.DropLineFour = dr["DropAddressLine4"].ToString().Trim();
                modeljourney.DropPostCode = dr["DropPostCode"].ToString().Trim();

                //collectionAddress.Easting = dr["DropEasting"].ToString().Trim();// Convert.ToDouble(dr["PatientEasting"].ToString() == "" ? "0" : dr["PatientEasting"].ToString().Trim());
                //collectionAddress.Northing = dr["DropNorthing"].ToString().Trim();
                //collectionAddress.GridReference = dr["DropGridReference"].ToString().Trim();
                //collectionAddress.Longitude = dr["DropLongitude"].ToString().Trim();
                //collectionAddress.Latitude = dr["DropLatitude"].ToString().Trim();
                //collectionAddress.UPRN = dr["DropURPN"].ToString().Trim();

                modeljourney.DropContactTelNo = dr["DropContactPhone"].ToString().Trim();
                modeljourney.DropExtensionNo = dr["DropExtention"].ToString().Trim();

                modeljourney.DropFacilityType = dr["DropFacilityType"].ToString().Trim();
                modeljourney.DropFacility = dr["DropFacility"].ToString().Trim();
                modeljourney.DropFacilityDepartment = dr["DropFacilityDepartment"].ToString().Trim();
                #endregion
                #region transport Requirement
               // modeljourney = new TransportRequirementModel();
                //transportRequirement.Id = Convert.ToInt32(dr["BookingId"]);
                modeljourney.TransportRequestReason = dr["TransportRequestReason"].ToString();
                modeljourney.TransportSelection = dr["TransportSelection"].ToString();
                modeljourney.IsInfectious = (Boolean)dr["isPatientInfectious"];
                modeljourney.IsBariatric = (Boolean)dr["Bariatric"];
                modeljourney.IsDiabetic = (Boolean)dr["Diabetic"];
                modeljourney.IsDNACPR = (Boolean)dr["DNACPR"];
                modeljourney.IsElectricWheelchair = (Boolean)dr["ElectricWheelChair"];
                modeljourney.IsEscortTravelling = (Boolean)dr["EscortTravelling"];
                modeljourney.IsFullLegPlasterPOP = (Boolean)dr["FullLegPlaster"];
                modeljourney.IsNoneOfAbove = (Boolean)dr["Non"];
                modeljourney.IsNuclearMedicineRadioActiveRisk = (Boolean)dr["NuclearMedicine"];
                modeljourney.IsTravellingWithOwnOxygen = (Boolean)dr["TravellingWithOwnOxygen"];
                modeljourney.IsWaterlow = (Boolean)dr["WaterFlow"];
                modeljourney.IsWheelchairAndLegExtension = (Boolean)dr["WheelChairLegExtention"];
                modeljourney.InfectionName = dr["InfectionName"].ToString();
                modeljourney.EscortType = dr["EscortTypeName"].ToString().Trim();
                modeljourney.EscortNumberId = dr["EscortNumber"].ToString() == "" ? 0 : Convert.ToInt32(dr["EscortNumber"]);
                //modeljourney.Id = dr["BookingID"].ToString() == "" ? 0 : Convert.ToInt32(dr["BookingID"]);
                modeljourney.AdditionalPatientInfo = dr["PatientAdditionalInfo"].ToString().Trim();
                // transportRequirement.EscortNumberId
                //journey.TransportRequirement = transportRequirement;
                #endregion

                #region journeyriskAssessment
               // riskAssessment = new RiskAssessmentModel();
               // modeljourney.IsManualHandlingProfileCarriedOutNo = (Boolean)dr["IsFullManualHandling"] == true ? false : true;
                modeljourney.IsManualHandlingProfileCarriedOutYes = (Boolean)dr["IsFullManualHandling"] == true ? true : false;
                modeljourney.PatientRiskAssessment = dr["PatientRiskAssessment"].ToString();
                modeljourney.PropertyRiskAssessment = dr["PropertyRiskAssessment"].ToString();

                //journey.RiskAssessment = riskAssessment;
                #endregion

                #region Requester
               // _requester.Id = Convert.ToInt32(dr["UserID"]);
              //  _requester.RequestDate = System.DateTime.Now;//.ToString("dd/MM/yyyy");//_//DateTime.Parse(System.DateTime.Now.ToString("dd/MM/yyyy"), new CultureInfo("ur-pk", false));
               // _requester.GenderTitleId = Convert.ToInt32(dr["GenderID"]);
                modeljourney.RequesterFirstName = dr["FirstName"].ToString();
                modeljourney.RequesterSurName = dr["SurName"].ToString();
                modeljourney.StaffPosition = dr["PositionName"].ToString();
                modeljourney.RequesterFacility = dr["RequesterFacility"].ToString();
                modeljourney.ServiceType = dr["ServiceType"].ToString();
               // modeljourney.RequesterCostCenter = "";//; dr["SurName"].ToString();
              //  modeljourney.SubjectiveCode = "";// dr["SurName"].ToString();
                modeljourney.EmailAddress = dr["Email"].ToString();
               // modeljourney.PostalAddress = dr["PostalAddress"].ToString();
               // modeljourney.FacilityId = dr["FacilityID"].ToString().Trim();
                modeljourney.UserName = dr["Login"].ToString().Trim();
                modeljourney.RequesterContactTelNo = dr["RequesterTelephone"].ToString().Trim();// dr["SurName"].ToString();
               // modeljourney.AuthorisingClinician = "";// dr["SurName"].ToString();
               // modeljourney.AuthorisingRoleId = "";// dr["SurName"].ToString();
               // modeljourney.FacilityName = dr["FacilityName"].ToString().Trim();
               // modeljourney.DepartmentName = dr["FacilityDepartmentName"].ToString().Trim();
               // modeljourney.facilityType = dr["FacilityTypeName"].ToString().Trim();
              //  modeljourney.ReqExtension = dr["Extension"].ToString().Trim();
                #endregion
            }

            return modeljourney;
        }
        private RequestertModel GetRequesterData() 
        {
            clsEBooking objEBooking = new clsEBooking();
            var userid = HttpContext.Current.User.Identity.Name;
            DataSet ds_requester = objEBooking.GetUserDetails(int.Parse(userid != null ? userid.ToString().Trim() : "0"));
            RequestertModel _requester = new RequestertModel();
            if (ds_requester.Tables[0].Rows.Count > 0)
            {
                _requester.Id = Convert.ToInt32(ds_requester.Tables[0].Rows[0]["UserID"]);
                _requester.RequestDate = System.DateTime.Now;//.ToString("dd/MM/yyyy");//_//DateTime.Parse(System.DateTime.Now.ToString("dd/MM/yyyy"), new CultureInfo("ur-pk", false));
                _requester.GenderTitleId = Convert.ToInt32(ds_requester.Tables[0].Rows[0]["GenderID"]);
                _requester.FirstName = ds_requester.Tables[0].Rows[0]["FirstName"].ToString();
                _requester.Surname = ds_requester.Tables[0].Rows[0]["SurName"].ToString();
                _requester.StaffPosition = ds_requester.Tables[0].Rows[0]["PositionName"].ToString();
                _requester.DepartmentId = ds_requester.Tables[0].Rows[0]["DepartmentID"].ToString();
                _requester.ServiceTypeId = ds_requester.Tables[0].Rows[0]["ServiceID"].ToString();
                _requester.CostCentre = "";//; ds_requester.Tables[0].Rows[0]["SurName"].ToString();
                _requester.SubjectiveCode = "";// ds_requester.Tables[0].Rows[0]["SurName"].ToString();
                _requester.EmailAddress = ds_requester.Tables[0].Rows[0]["Email"].ToString();
                _requester.PostalAddress = ds_requester.Tables[0].Rows[0]["PostalAddress"].ToString();
                _requester.FacilityId = ds_requester.Tables[0].Rows[0]["FacilityID"].ToString().Trim();
                _requester.UserName = ds_requester.Tables[0].Rows[0]["Login"].ToString().Trim();
                _requester.ContactTelNo = ds_requester.Tables[0].Rows[0]["TelephoneNo"].ToString().Trim();// ds_requester.Tables[0].Rows[0]["SurName"].ToString();
                _requester.AuthorisingClinician = "";// ds_requester.Tables[0].Rows[0]["SurName"].ToString();
                _requester.AuthorisingRoleId = "";// ds_requester.Tables[0].Rows[0]["SurName"].ToString();
                _requester.FacilityName = ds_requester.Tables[0].Rows[0]["FacilityName"].ToString().Trim();
                _requester.DepartmentName = ds_requester.Tables[0].Rows[0]["FacilityDepartmentName"].ToString().Trim();
                _requester.facilityType = ds_requester.Tables[0].Rows[0]["FacilityTypeName"].ToString().Trim();
                _requester.Extension = ds_requester.Tables[0].Rows[0]["Extension"].ToString().Trim();
            }
            return _requester;
        }
        public dynamic SearchJourneyByRefNumber(string referenceNumber)
        {
            List<CompositeModel> journey = GetJourneyListFiltered("1,2,3", "", "", referenceNumber);
            Boolean journeyfind = journey.Find(x => x.Patient.BookingNo.Contains(referenceNumber)) == null;
            if (journeyfind == true)
            {
                return false;
            }
            return (journey.Find(x => x.Patient.BookingNo.Contains(referenceNumber)));

        }

        public dynamic SearchPatientbyNHSnumber(string nhsNumber)
        {
            clsEBooking objEbooking = new clsEBooking();
            DataSet ds_Patient = objEbooking.SearchPatient(nhsNumber);

            if (ds_Patient.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                var objpatientandHomeComposite = new PatientandHomeComposite();
                var homeAddress = new HomeAddressModel();
                var patient = new PatientModel();
                patient.PatientID = Convert.ToInt32(ds_Patient.Tables[0].Rows[0]["PatientID"].ToString().Trim());
                patient.GenderTitleId = Convert.ToInt32(ds_Patient.Tables[0].Rows[0]["TitleID"].ToString().Trim());
                patient.FirstName = ds_Patient.Tables[0].Rows[0]["Firstname"].ToString().Trim();
                patient.Surname = ds_Patient.Tables[0].Rows[0]["SurName"].ToString().Trim();
                patient.BirthDate = Convert.ToDateTime(ds_Patient.Tables[0].Rows[0]["DOB"].ToString().Trim());
                patient.NHSNumber = ds_Patient.Tables[0].Rows[0]["NHSNumber"].ToString().Trim();
                patient.IsleOfWightNo = ds_Patient.Tables[0].Rows[0]["IOWNumber"].ToString().Trim();
                patient.NameOfGP = ds_Patient.Tables[0].Rows[0]["GPName"].ToString().Trim();

                homeAddress.IsNoFixAbode = Convert.ToBoolean(ds_Patient.Tables[0].Rows[0]["HasNoAdobe"].ToString().Trim());
                homeAddress.LineOne = ds_Patient.Tables[0].Rows[0]["AddressLine1"].ToString().Trim();
                homeAddress.LineTwo = ds_Patient.Tables[0].Rows[0]["AddressLine2"].ToString().Trim();
                homeAddress.LineThree = ds_Patient.Tables[0].Rows[0]["AddressLine3"].ToString().Trim();
                homeAddress.LineFour = ds_Patient.Tables[0].Rows[0]["AddressLine4"].ToString().Trim();
                homeAddress.PostCode = ds_Patient.Tables[0].Rows[0]["PostCode"].ToString().Trim();
                homeAddress.UPRN = ds_Patient.Tables[0].Rows[0]["URPN"].ToString().Trim();
                homeAddress.Latitude = ds_Patient.Tables[0].Rows[0]["latitude"].ToString().Trim();
                homeAddress.Longitude = ds_Patient.Tables[0].Rows[0]["Longitude"].ToString().Trim();
                homeAddress.Easting = ds_Patient.Tables[0].Rows[0]["Easting"].ToString().Trim();
                homeAddress.Northing = ds_Patient.Tables[0].Rows[0]["Northing"].ToString().Trim();
                homeAddress.GridReference = ds_Patient.Tables[0].Rows[0]["GridReference"].ToString().Trim();
                homeAddress.ContactTelNo = ds_Patient.Tables[0].Rows[0]["HomePhone"].ToString().Trim();
                homeAddress.AlternateContactTelNo = ds_Patient.Tables[0].Rows[0]["AlternatePhone"].ToString().Trim();
                homeAddress.RelationshipId = Convert.ToInt32(ds_Patient.Tables[0].Rows[0]["RelationshipID"].ToString().Trim());

                objpatientandHomeComposite.Patient = patient;
                objpatientandHomeComposite.HomeAddress = homeAddress;

                return objpatientandHomeComposite;

            }

            //return true;
        }
        public bool Deletejourney(string referenceNumber, string CancelReason, int UpdatedBy,bool is24HourCancellation)
        {

            clsEBooking objEbooking = new clsEBooking();
            return objEbooking.ChangeJourneyStatus(0, referenceNumber, CancelReason, 5, UpdatedBy,is24HourCancellation);//BookingId=0 when passing Reference of BookingNumber, Status=5 for Cancelled Journey

        }

        public dynamic GetJourneyList()
        {

            List<CompositeModel> journeyList = new List<CompositeModel>();

            var patient = new PatientModel();
            var homeAddress = new HomeAddressModel();
            var collectionAddress = new CollectionAddressModel();
            var destinationAddress = new DestinationAddressModel();
            var transportRequirement = new TransportRequirementModel();
            var riskAssessment = new RiskAssessmentModel();
            var specialistTransportRequest = new SpecialistTransportRequestModel();

            var journey = new CompositeModel();

            clsEBooking objEbooking = new clsEBooking();
            DataSet dsJourney = objEbooking.GetJourney(0, "", "", "", "");
            if (dsJourney.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsJourney.Tables[0].Rows)
                {
                    #region Patient
                    patient = new PatientModel();
                    journey = new CompositeModel();
                    patient.PatientID = Convert.ToInt32(dr["PatientId"]);
                    patient.IsRiskAssessmentRequired = (Boolean)dr["IsRiskAssessmentRequired"];
                    patient.IsMainlandRepatriation = (Boolean)dr["IsFormainLand"];
                    try
                    {
                        patient.JourneyDate = Convert.ToDateTime(dr["JourneyDate"]);
                        patient.BookingDateTime = Convert.ToDateTime(dr["InsertedAt"]);
                    }
                    catch { }
                    try
                    {
                        patient.AppointmentTimeId = Convert.ToInt32(dr["StandardAppointmentTimeID"]);
                        patient.EstimatedAppointmentDurationId = Convert.ToInt32(dr["EstimatedDurationofAppointment"]);
                    }
                    catch { }
                    patient.ActualAppointmentTime = dr["ActualAppointmentTime"].ToString().Substring(0, 5);
                    patient.GenderTitleId = Convert.ToInt32(dr["TitleId"]);
                    patient.FirstName = (string)dr["FirstName"];
                    patient.Surname = (string)dr["SurName"];
                    try
                    {
                        patient.BirthDate = Convert.ToDateTime(dr["DOB"]);

                    }
                    catch { }
                    patient.NHSNumber = (string)dr["NHSNumber"];
                    patient.IsleOfWightNo = (string)dr["IOWNumber"];
                    patient.NameOfGP = (string)dr["GPName"];
                    try
                    {


                        patient.WeighingDate = Convert.ToDateTime(dr["WeightDate"]);
                        patient.LastRecordedPatientWeight = Convert.ToDouble(dr["Weight"]);
                    }
                    catch { }
                    try
                    {
                        patient.GPPracticeId = Convert.ToInt32(dr["GPPracticeID"]);

                    }
                    catch { }
                    patient.GPPracticeAddressLineOne = (string)dr["GpAddressLine1"];
                    patient.GPPracticeAddressLineTwo = (string)dr["GpAddressLine2"];
                    patient.GPPracticeAddressLineThree = (string)dr["GpAddressLine3"];
                    patient.GPPracticeAddressLineFour = (string)dr["GpAddressLine4"];
                    patient.GPPracticeAddressPostCode = (string)dr["GpAddressPostCode"];
                    patient.ContactTelephoneNo = (string)dr["GPPhoneNo"];
                    patient.BookingNo = dr["BookingNumber"].ToString();
                    patient.BookingStatusName = dr["BookingStatusName"].ToString();
                    journey.Patient = patient;
                    #endregion
                    #region HomeAddress

                    homeAddress = new HomeAddressModel();


                    homeAddress.IsNoFixAbode = (Boolean)dr["HasNoAdobe"];
                    homeAddress.LineOne = (string)dr["AddressLine1"];
                    homeAddress.LineTwo = (string)dr["AddressLine2"];
                    homeAddress.LineThree = (string)dr["AddressLine3"];
                    homeAddress.LineFour = (string)dr["AddressLine4"];
                    homeAddress.PostCode = (string)dr["PostCode"];
                    homeAddress.ContactTelNo = (string)dr["HomePhone"];
                    homeAddress.AlternateContactTelNo = (string)dr["AlternatePhone"];
                    try
                    {
                        homeAddress.RelationshipId = Convert.ToInt32(dr["RelationShipID"]);
                    }
                    catch { }
                    journey.HomeAddress = homeAddress;
                    #endregion

                    #region CollectionAddress
                    collectionAddress = new CollectionAddressModel();

                    collectionAddress.IsThisPatientHomeAddress = (Boolean)dr["IsPickHomeAddress"];
                    collectionAddress.LineOne = (string)dr["PickAddressLine1"];
                    collectionAddress.LineTwo = (string)dr["PickAddressLine2"];
                    collectionAddress.LineThree = (string)dr["PickAddressLine3"];
                    collectionAddress.LineFour = (string)dr["PickAddressLine4"];
                    collectionAddress.PostCode = (string)dr["PickPostCode"];
                    collectionAddress.ContactTelNo = (string)dr["PickContactPhone"];
                    collectionAddress.ExtensionNo = (string)dr["PickExtention"];
                    try
                    {
                        collectionAddress.FacilityId = Convert.ToInt32(dr["PickFacilityID"]);
                        //collectionAddress.FacilityTypeId = (Boolean)dr["IsPickHomeAddress"];
                        collectionAddress.FacilityTypeId = dr["CollectionFacilityTypeId"].ToString() == "" ? 0 : Convert.ToInt32(dr["CollectionFacilityTypeId"]);
                        collectionAddress.DepartmentId = Convert.ToInt32(dr["PickDepartmentID"]);
                    }
                    catch { }
                    journey.CollectionAddress = collectionAddress;
                    #endregion

                    #region DestinationAddress
                    destinationAddress = new DestinationAddressModel();
                    destinationAddress.IsThisPatientHomeAddress = (Boolean)dr["IsDropHomeAddress"];
                    destinationAddress.LineOne = (string)dr["DropAddressLine1"];
                    destinationAddress.LineTwo = (string)dr["DropAddressLine2"];
                    destinationAddress.LineThree = (string)dr["DropAddressLine3"];
                    destinationAddress.LineFour = (string)dr["DropAddressLine4"];
                    destinationAddress.PostCode = (string)dr["DropPostCode"];
                    destinationAddress.ContactTelNo = (string)dr["DropContactPhone"];
                    destinationAddress.ExtensionNo = (string)dr["DropExtention"];
                    try
                    {
                        destinationAddress.FacilityId = Convert.ToInt32(dr["DropFacilityID"]);
                        destinationAddress.FacilityTypeId = dr["DestinationFacilityTypeId"].ToString() == "" ? 0 : Convert.ToInt32(dr["DestinationFacilityTypeId"]);
                        //collectionAddress.FacilityTypeId = (Boolean)dr["IsPickHomeAddress"];
                        destinationAddress.DepartmentId = Convert.ToInt32(dr["DropDepartmentID"]);
                    }
                    catch { }
                    journey.DestinationAddress = destinationAddress;
                    #endregion
                    #region transport Requirement
                    transportRequirement = new TransportRequirementModel();
                    transportRequirement.Id = Convert.ToInt32(dr["BookingId"]);
                    transportRequirement.TransportRequestReasonId = dr["TransportRequestReasonID"].ToString() == "" ? 0 : Convert.ToInt32(dr["TransportRequestReasonID"]);
                    transportRequirement.TransportSelectionId = dr["TransportSelectionID"].ToString() == "" ? 0 : Convert.ToInt32(dr["TransportSelectionID"]);
                    transportRequirement.IsInfectious = (Boolean)dr["isPatientInfectious"];
                    transportRequirement.IsBariatric = (Boolean)dr["Bariatric"];
                    transportRequirement.IsDiabetic = (Boolean)dr["Diabetic"];
                    transportRequirement.IsDNACPR = (Boolean)dr["DNACPR"];
                    transportRequirement.IsElectricWheelchair = (Boolean)dr["ElectricWheelChair"];
                    transportRequirement.IsEscortTravelling = (Boolean)dr["EscortTravelling"];
                    transportRequirement.IsFullLegPlasterPOP = (Boolean)dr["FullLegPlaster"];
                    transportRequirement.IsNoneOfAbove = (Boolean)dr["Non"];
                    transportRequirement.IsNuclearMedicineRadioActiveRisk = (Boolean)dr["NuclearMedicine"];
                    transportRequirement.IsTravellingWithOwnOxygen = (Boolean)dr["TravellingWithOwnOxygen"];
                    transportRequirement.IsWaterlow = (Boolean)dr["WaterFlow"];
                    transportRequirement.IsWheelchairAndLegExtension = (Boolean)dr["WheelChairLegExtention"];
                    transportRequirement.InfectiousId = dr["InfectionID"].ToString() == "" ? 0 : Convert.ToInt32(dr["InfectionID"]);
                    transportRequirement.EscortTypeId = dr["EscortType"].ToString() == "" ? 0 : Convert.ToInt32(dr["EscortType"]);
                    transportRequirement.EscortNumberId = dr["EscortNumber"].ToString() == "" ? 0 : Convert.ToInt32(dr["EscortNumber"]);
                    transportRequirement.Id = dr["BookingID"].ToString() == "" ? 0 : Convert.ToInt32(dr["BookingID"]);
                    //transportRequirement.AdditionalPatientInfo=dr[""]
                    // transportRequirement.EscortNumberId
                    journey.TransportRequirement = transportRequirement;
                    #endregion

                    #region journeyriskAssessment
                    riskAssessment = new RiskAssessmentModel();
                    riskAssessment.IsManualHandlingProfileCarriedOutNo = (Boolean)dr["IsFullManualHandling"] == true ? false : true;
                    riskAssessment.IsManualHandlingProfileCarriedOutYes = (Boolean)dr["IsFullManualHandling"] == true ? true : false;
                    riskAssessment.PatientRiskAssessment = dr["PatientRiskAssessment"].ToString();
                    riskAssessment.PropertyRiskAssessment = dr["PropertyRiskAssessment"].ToString();

                    journey.RiskAssessment = riskAssessment;
                    #endregion

                    #region SpeicalTransport Request
                    specialistTransportRequest = new SpecialistTransportRequestModel();
                    specialistTransportRequest.AuthorisingConsultantOrGP = dr["AuthorizingConsultant"].ToString();
                    specialistTransportRequest.AuthorisingPracticeName = dr["AuthorizingPractice"].ToString();
                    specialistTransportRequest.IsAdmission = (Boolean)dr["IsAdmissionToNursing"];
                    specialistTransportRequest.IsHandledByProfessional = (Boolean)dr["TrainedPersonRequired"];
                    specialistTransportRequest.IsOxygenTheropy = (Boolean)dr["OxygenTherapyRequired"];
                    specialistTransportRequest.IsPrecludesTravelling = (Boolean)dr["HasDisability"];
                    specialistTransportRequest.IsVisit = (Boolean)dr["VisitToandFromResidentialCareHome"];
                    specialistTransportRequest.IsVisitOrAdmitted = (Boolean)dr["VisitToHospitalorHospice"];
                    specialistTransportRequest.IsWhilstLayingDown = (Boolean)dr["WhilstLayingDown"];
                    journey.SpecialistTransportRequest = specialistTransportRequest;
                    #endregion
                    journeyList.Add(journey);



                }





            }

            return journeyList;

        }

        public dynamic GetJourneyListFiltered(string Status, string BookingDateFrom, string BookingDateTo, string BookingNumber)
        {

            List<CompositeModel> journeyList = new List<CompositeModel>();

            var patient = new PatientModel();
            var homeAddress = new HomeAddressModel();
            var collectionAddress = new CollectionAddressModel();
            var destinationAddress = new DestinationAddressModel();
            var transportRequirement = new TransportRequirementModel();
            var riskAssessment = new RiskAssessmentModel();
            var specialistTransportRequest = new SpecialistTransportRequestModel();
           
            var journey = new CompositeModel();

            clsEBooking objEbooking = new clsEBooking();
            DataSet dsJourney = objEbooking.GetJourney(0, Status, BookingDateFrom, BookingDateTo, BookingNumber);
            if (dsJourney.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsJourney.Tables[0].Rows)
                {
                    ///Explanation for the conditions applied below
                    ///* 1- If Case ID yet not assigned i-e not approved.
                    ///* 2- Case approved but made Amendments that are yet to be Approved/Rejected. 
                    ///* 3- The case Has been approved earlier but now its status has been rejected. 
                    /// In all the mentioned cases the data displayed will be from web schema. Otherwise the data from Calltaking schema would be displayed. 
                    /// 


                    if (dr["CaseID"].ToString().Trim() == "" || dr["LastBookingStatusID"].ToString().Trim() == "1" || dr["LastBookingStatusID"].ToString().Trim() == "3")
                    {
                        #region Patient
                        patient = new PatientModel();
                        journey = new CompositeModel();
                        patient.PatientID = Convert.ToInt32(dr["PatientId"]);
                        patient.IsRiskAssessmentRequired = (Boolean)dr["IsRiskAssessmentRequired"];
                        patient.IsMainlandRepatriation = (Boolean)dr["IsFormainLand"];
                        try
                        {
                            patient.JourneyDate = Convert.ToDateTime(dr["JourneyDate"]);
                            patient.BookingDateTime = Convert.ToDateTime(dr["InsertedAt"]);
                        }
                        catch { }
                        try
                        {
                            patient.AppointmentTimeId = Convert.ToInt32(dr["StandardAppointmentTimeID"]);
                            patient.EstimatedAppointmentDurationId = Convert.ToInt32(dr["EstimatedDurationofAppointment"]);
                        }
                        catch { }
                        patient.ActualAppointmentTime = dr["ActualAppointmentTime"].ToString().Substring(0, 5);
                        patient.GenderTitleId = Convert.ToInt32(dr["TitleId"]);
                        patient.FirstName = (string)dr["FirstName"];
                        patient.Surname = (string)dr["SurName"];
                        try
                        {
                            patient.BirthDate = Convert.ToDateTime(dr["DOB"]);

                        }
                        catch { }
                        patient.NHSNumber = (string)dr["NHSNumber"];
                        patient.IsleOfWightNo = (string)dr["IOWNumber"];
                        patient.NameOfGP = (string)dr["GPName"];
                        try
                        {


                            patient.WeighingDate = Convert.ToDateTime(dr["WeightDate"]);
                            patient.LastRecordedPatientWeight = Convert.ToDouble(dr["Weight"]);
                        }
                        catch { }
                        try
                        {
                            patient.GPPracticeId = Convert.ToInt32(dr["GPPracticeID"]);

                        }
                        catch { }
                        patient.GPPracticeAddressLineOne = dr["GpAddressLine1"].ToString().Trim();
                        patient.GPPracticeAddressLineTwo = dr["GpAddressLine2"].ToString().Trim();
                        patient.GPPracticeAddressLineThree = dr["GpAddressLine3"].ToString().Trim();
                        patient.GPPracticeAddressLineFour = dr["GpAddressLine4"].ToString().Trim();
                        patient.GPPracticeAddressPostCode = dr["GpAddressPostCode"].ToString().Trim();
                        patient.ContactTelephoneNo = dr["GPPhoneNo"].ToString().Trim();
                        patient.BookingNo = dr["BookingNumber"].ToString();
                        patient.BookingStatusName = dr["CaseID"].ToString().Trim() == "" ? dr["BookingStatusName"].ToString() : "Amendment Request";
                        patient.RequesterSubjectiveCode = dr["SubjectiveCode"].ToString();
                        patient.RequesterCostCenter = dr["CostCenter"].ToString();
                        patient.RequesterAuthorizingClinician = dr["AuthoirizingClinical"].ToString();
                        patient.RequesterAuthorizingRoleId = Convert.ToInt32(dr["ClinicianRole"].ToString() == "" ? "0" : dr["ClinicianRole"].ToString());
                        patient.RejectionReason = dr["LastBookingStatusNotes"].ToString().Trim().ToLower().IndexOf("new booking") >= 0 ? "" : dr["LastBookingStatusNotes"].ToString().Trim();
                        patient.LastStatusAt = Convert.ToDateTime(dr["LastStatusAt"]);
                        patient.isPrivatePatient = dr["isPrivatePatient"].ToString().Trim() == "" ? false : Convert.ToBoolean(dr["isPrivatePatient"]);
                        journey.Patient = patient;
                        #endregion
                        #region HomeAddress

                        homeAddress = new HomeAddressModel();


                        homeAddress.IsNoFixAbode = (Boolean)dr["HasNoAdobe"];
                        homeAddress.LineOne = dr["AddressLine1"].ToString().Trim();
                        homeAddress.LineTwo = dr["AddressLine2"].ToString().Trim();
                        homeAddress.LineThree = dr["AddressLine3"].ToString().Trim();
                        homeAddress.LineFour = dr["AddressLine4"].ToString().Trim();
                        homeAddress.PostCode = dr["PostCode"].ToString().Trim();

                        homeAddress.Easting = dr["Easting"].ToString().Trim();// Convert.ToDouble(dr["PatientEasting"].ToString() == "" ? "0" : dr["PatientEasting"].ToString().Trim());
                        homeAddress.Northing = dr["Northing"].ToString().Trim();
                        homeAddress.GridReference = dr["GridReference"].ToString().Trim();
                        homeAddress.Longitude = dr["Longitude"].ToString().Trim();
                        homeAddress.Latitude = dr["Latitude"].ToString().Trim();
                        homeAddress.UPRN = dr["URPN"].ToString().Trim();

                        homeAddress.ContactTelNo = dr["HomePhone"].ToString().Trim();
                        homeAddress.AlternateContactTelNo = dr["AlternatePhone"].ToString().Trim();
                        try
                        {
                            homeAddress.RelationshipId = Convert.ToInt32(dr["RelationShipID"]);
                        }
                        catch { }
                        journey.HomeAddress = homeAddress;
                        #endregion

                        #region CollectionAddress
                        collectionAddress = new CollectionAddressModel();

                        collectionAddress.IsThisPatientHomeAddress = (Boolean)dr["IsPickHomeAddress"];
                        collectionAddress.LineOne = dr["PickAddressLine1"].ToString().Trim();
                        collectionAddress.LineTwo = dr["PickAddressLine2"].ToString().Trim();
                        collectionAddress.LineThree = dr["PickAddressLine3"].ToString().Trim();
                        collectionAddress.LineFour = dr["PickAddressLine4"].ToString().Trim();
                        collectionAddress.PostCode = dr["PickPostCode"].ToString().Trim();

                        collectionAddress.Easting = dr["PickEasting"].ToString().Trim();// Convert.ToDouble(dr["PatientEasting"].ToString() == "" ? "0" : dr["PatientEasting"].ToString().Trim());
                        collectionAddress.Northing = dr["PickNorthing"].ToString().Trim();
                        collectionAddress.GridReference = dr["PickGridReference"].ToString().Trim();
                        collectionAddress.Longitude = dr["PickLongitude"].ToString().Trim();
                        collectionAddress.Latitude = dr["PickLatitude"].ToString().Trim();
                        collectionAddress.UPRN = dr["PickURPN"].ToString().Trim();

                        collectionAddress.ContactTelNo = dr["PickContactPhone"].ToString().Trim();
                        collectionAddress.ExtensionNo = dr["PickExtention"].ToString().Trim();
                        try
                        {
                            collectionAddress.FacilityId = Convert.ToInt32(dr["PickFacilityID"]);
                            //collectionAddress.FacilityTypeId = (Boolean)dr["IsPickHomeAddress"];
                            collectionAddress.FacilityTypeId = dr["CollectionFacilityTypeId"].ToString() == "" ? 0 : Convert.ToInt32(dr["CollectionFacilityTypeId"]);
                            collectionAddress.DepartmentId = Convert.ToInt32(dr["PickDepartmentID"]);
                        }
                        catch { }
                        journey.CollectionAddress = collectionAddress;
                        #endregion

                        #region DestinationAddress
                        destinationAddress = new DestinationAddressModel();
                        destinationAddress.IsThisPatientHomeAddress = (Boolean)dr["IsDropHomeAddress"];
                        destinationAddress.LineOne = dr["DropAddressLine1"].ToString().Trim();
                        destinationAddress.LineTwo = dr["DropAddressLine2"].ToString().Trim();
                        destinationAddress.LineThree = dr["DropAddressLine3"].ToString().Trim();
                        destinationAddress.LineFour = dr["DropAddressLine4"].ToString().Trim();
                        destinationAddress.PostCode = dr["DropPostCode"].ToString().Trim();

                        destinationAddress.Easting = dr["DropEasting"].ToString().Trim();// Convert.ToDouble(dr["PatientEasting"].ToString() == "" ? "0" : dr["PatientEasting"].ToString().Trim());
                        destinationAddress.Northing = dr["DropNorthing"].ToString().Trim();
                        destinationAddress.GridReference = dr["DropGridReference"].ToString().Trim();
                        destinationAddress.Longitude = dr["DropLongitude"].ToString().Trim();
                        destinationAddress.Latitude = dr["DropLatitude"].ToString().Trim();
                        destinationAddress.UPRN = dr["DropURPN"].ToString().Trim();



                        destinationAddress.ContactTelNo = dr["DropContactPhone"].ToString().Trim();
                        destinationAddress.ExtensionNo = dr["DropExtention"].ToString().Trim();
                        try
                        {
                            destinationAddress.FacilityId = Convert.ToInt32(dr["DropFacilityID"]);
                            destinationAddress.FacilityTypeId = dr["DestinationFacilityTypeId"].ToString() == "" ? 0 : Convert.ToInt32(dr["DestinationFacilityTypeId"]);
                            //collectionAddress.FacilityTypeId = (Boolean)dr["IsPickHomeAddress"];
                            destinationAddress.DepartmentId = Convert.ToInt32(dr["DropDepartmentID"]);
                        }
                        catch { }
                        journey.DestinationAddress = destinationAddress;
                        #endregion
                        #region transport Requirement
                        transportRequirement = new TransportRequirementModel();
                        transportRequirement.Id = Convert.ToInt32(dr["BookingId"]);
                        transportRequirement.TransportRequestReasonId = dr["TransportRequestReasonID"].ToString() == "" ? 0 : Convert.ToInt32(dr["TransportRequestReasonID"]);
                        transportRequirement.TransportSelectionId = dr["TransportSelectionID"].ToString() == "" ? 0 : Convert.ToInt32(dr["TransportSelectionID"]);
                        transportRequirement.IsInfectious = (Boolean)dr["isPatientInfectious"];
                        transportRequirement.IsBariatric = (Boolean)dr["Bariatric"];
                        transportRequirement.IsDiabetic = (Boolean)dr["Diabetic"];
                        transportRequirement.IsDNACPR = (Boolean)dr["DNACPR"];
                        transportRequirement.IsElectricWheelchair = (Boolean)dr["ElectricWheelChair"];
                        transportRequirement.IsEscortTravelling = (Boolean)dr["EscortTravelling"];
                        transportRequirement.IsFullLegPlasterPOP = (Boolean)dr["FullLegPlaster"];
                        transportRequirement.IsNoneOfAbove = (Boolean)dr["Non"];
                        transportRequirement.IsNuclearMedicineRadioActiveRisk = (Boolean)dr["NuclearMedicine"];
                        transportRequirement.IsTravellingWithOwnOxygen = (Boolean)dr["TravellingWithOwnOxygen"];
                        transportRequirement.IsWaterlow = (Boolean)dr["WaterFlow"];
                        transportRequirement.IsWheelchairAndLegExtension = (Boolean)dr["WheelChairLegExtention"];
                        transportRequirement.InfectiousId = dr["InfectionID"].ToString() == "" ? 0 : Convert.ToInt32(dr["InfectionID"]);
                        transportRequirement.EscortTypeId = dr["EscortType"].ToString() == "" ? 0 : Convert.ToInt32(dr["EscortType"]);
                        transportRequirement.EscortNumberId = dr["EscortNumber"].ToString() == "" ? 0 : Convert.ToInt32(dr["EscortNumber"]);
                        transportRequirement.Id = dr["BookingID"].ToString() == "" ? 0 : Convert.ToInt32(dr["BookingID"]);
                        transportRequirement.AdditionalPatientInfo = dr["PatientAdditionalInfo"].ToString().Trim();
                        // transportRequirement.EscortNumberId
                        journey.TransportRequirement = transportRequirement;
                        #endregion

                        #region journeyriskAssessment
                        riskAssessment = new RiskAssessmentModel();
                        riskAssessment.IsManualHandlingProfileCarriedOutNo = (Boolean)dr["IsFullManualHandling"] == true ? false : true;
                        riskAssessment.IsManualHandlingProfileCarriedOutYes = (Boolean)dr["IsFullManualHandling"] == true ? true : false;
                        riskAssessment.PatientRiskAssessment = dr["PatientRiskAssessment"].ToString();
                        riskAssessment.PropertyRiskAssessment = dr["PropertyRiskAssessment"].ToString();

                        journey.RiskAssessment = riskAssessment;
                        #endregion

                        #region SpeicalTransport Request
                        specialistTransportRequest = new SpecialistTransportRequestModel();
                        specialistTransportRequest.AuthorisingConsultantOrGP = dr["AuthorizingConsultant"].ToString();
                        specialistTransportRequest.AuthorisingPracticeName = dr["AuthorizingPractice"].ToString();
                        specialistTransportRequest.IsAdmission = (Boolean)dr["IsAdmissionToNursing"];
                        specialistTransportRequest.IsHandledByProfessional = (Boolean)dr["TrainedPersonRequired"];
                        specialistTransportRequest.IsOxygenTheropy = (Boolean)dr["OxygenTherapyRequired"];
                        specialistTransportRequest.IsPrecludesTravelling = (Boolean)dr["HasDisability"];
                        specialistTransportRequest.IsVisit = (Boolean)dr["VisitToandFromResidentialCareHome"];
                        specialistTransportRequest.IsVisitOrAdmitted = (Boolean)dr["VisitToHospitalorHospice"];
                        specialistTransportRequest.IsWhilstLayingDown = (Boolean)dr["WhilstLayingDown"];
                        journey.SpecialistTransportRequest = specialistTransportRequest;
                        #endregion
                        journeyList.Add(journey);
                    }
                    else
                    {
                        clsCall objCall = new clsCall();
                        DataSet ds_Call = objCall.LoadCase(Convert.ToInt32(dr["CaseID"].ToString().Trim()));
                        if (ds_Call.Tables[0].Rows.Count > 0)
                        {
                            #region Patient
                            patient = new PatientModel();
                            journey = new CompositeModel();
                            patient.PatientID = Convert.ToInt32(dr["PatientId"]);
                            patient.IsRiskAssessmentRequired = Convert.ToBoolean(ds_Call.Tables[0].Rows[0]["IsRiskAssessmentRequired"].ToString().Trim() == "" ? 0 : ds_Call.Tables[0].Rows[0]["IsRiskAssessmentRequired"]);
                            patient.IsMainlandRepatriation = Convert.ToBoolean(ds_Call.Tables[0].Rows[0]["IsForMainlandRepatriation"].ToString().Trim() == "" ? 0 : ds_Call.Tables[0].Rows[0]["IsForMainlandRepatriation"]);
                            try
                            {
                                patient.JourneyDate = Convert.ToDateTime(ds_Call.Tables[0].Rows[0]["CollectionTimeFrom"]);
                                patient.BookingDateTime = Convert.ToDateTime(dr["InsertedAt"]);
                            }
                            catch { }
                            try
                            {
                                patient.AppointmentTimeId = Convert.ToInt32(ds_Call.Tables[0].Rows[0]["AppointmentTimeID"]);
                                patient.EstimatedAppointmentDurationId = Convert.ToInt32(ds_Call.Tables[0].Rows[0]["AppointmentDurationID"]);
                            }
                            catch { }
                            patient.ActualAppointmentTime = Convert.ToDateTime(ds_Call.Tables[0].Rows[0]["AppointmentTime"].ToString()).ToString("HH:mm");
                            patient.GenderTitleId = Convert.ToInt32(ds_Call.Tables[0].Rows[0]["titleid"]);
                            patient.FirstName = (string)ds_Call.Tables[0].Rows[0]["firstname"];
                            patient.Surname = (string)ds_Call.Tables[0].Rows[0]["surname"];
                            try
                            {
                                patient.BirthDate = Convert.ToDateTime(ds_Call.Tables[0].Rows[0]["dob"]);

                            }
                            catch { }
                            patient.NHSNumber = (string)ds_Call.Tables[0].Rows[0]["nhsno"];
                            patient.IsleOfWightNo = "IW" + ((string)ds_Call.Tables[0].Rows[0]["nationalid"]).Replace("IW","");
                            patient.NameOfGP = (string)dr["GPName"];
                            try
                            {


                                patient.WeighingDate = Convert.ToDateTime(ds_Call.Tables[0].Rows[0]["PatientWeightDate"]);
                                patient.LastRecordedPatientWeight = Convert.ToDouble(ds_Call.Tables[0].Rows[0]["PatientWeight"]);
                            }
                            catch { }
                            try
                            {
                                patient.GPPracticeId = Convert.ToInt32(ds_Call.Tables[0].Rows[0]["PatientGpPracticeID"]);

                            }
                            catch { }
                            patient.GPPracticeAddressLineOne = dr["GpAddressLine1"].ToString().Trim();
                            patient.GPPracticeAddressLineTwo = dr["GpAddressLine2"].ToString().Trim();
                            patient.GPPracticeAddressLineThree = dr["GpAddressLine3"].ToString().Trim();
                            patient.GPPracticeAddressLineFour = dr["GpAddressLine4"].ToString().Trim();
                            patient.GPPracticeAddressPostCode = dr["GpAddressPostCode"].ToString().Trim();
                            patient.ContactTelephoneNo = dr["GPPhoneNo"].ToString().Trim();
                            patient.BookingNo = dr["BookingNumber"].ToString();
                            patient.BookingStatusName = dr["BookingStatusName"].ToString();
                            patient.RequesterSubjectiveCode = dr["SubjectiveCode"].ToString();
                            patient.RequesterCostCenter = dr["CostCenter"].ToString();
                            patient.RequesterAuthorizingClinician = dr["AuthoirizingClinical"].ToString();
                            patient.RequesterAuthorizingRoleId = Convert.ToInt32(dr["ClinicianRole"].ToString() == "" ? "0" : dr["ClinicianRole"].ToString());
                            patient.RejectionReason = dr["LastBookingStatusNotes"].ToString().Trim().ToLower().IndexOf("new booking") >= 0 ? "" : dr["LastBookingStatusNotes"].ToString().Trim();
                            patient.LastStatusAt = Convert.ToDateTime(dr["LastStatusAt"]);
                            patient.CADCaseID = Convert.ToInt32(dr["CaseID"].ToString().Trim());
                            patient.isPrivatePatient = dr["isPrivatePatient"].ToString().Trim() == "" ? false : Convert.ToBoolean(dr["isPrivatePatient"]);
                            journey.Patient = patient;
                            #endregion
                            #region HomeAddress

                            homeAddress = new HomeAddressModel();


                            homeAddress.IsNoFixAbode = (Boolean)ds_Call.Tables[0].Rows[0]["hasnoabode"];
                            homeAddress.LineOne = ds_Call.Tables[0].Rows[0]["PatientAddressLine1"].ToString().Trim();
                            homeAddress.LineTwo = ds_Call.Tables[0].Rows[0]["PatientAddressLine2"].ToString().Trim();
                            homeAddress.LineThree = ds_Call.Tables[0].Rows[0]["PatientAddressLine3"].ToString().Trim();
                            homeAddress.LineFour = ds_Call.Tables[0].Rows[0]["PatientAddressLine4"].ToString().Trim();
                            homeAddress.PostCode = ds_Call.Tables[0].Rows[0]["PatientPostCode"].ToString().Trim();

                            homeAddress.Easting = ds_Call.Tables[0].Rows[0]["PatientEasting"].ToString().Trim();// Convert.ToDouble(ds_Call.Tables[0].Rows[0]["PatientEasting"].ToString() == "" ? "0" : ds_Call.Tables[0].Rows[0]["PatientEasting"].ToString().Trim());
                            homeAddress.Northing = ds_Call.Tables[0].Rows[0]["PatientNorthing"].ToString().Trim();
                            homeAddress.GridReference = ds_Call.Tables[0].Rows[0]["PatientGridReference"].ToString().Trim();
                            homeAddress.Longitude = ds_Call.Tables[0].Rows[0]["PatientLongitude"].ToString().Trim();
                            homeAddress.Latitude = ds_Call.Tables[0].Rows[0]["PatientLatitude"].ToString().Trim();
                            homeAddress.UPRN = ds_Call.Tables[0].Rows[0]["PatientURPN"].ToString().Trim();

                            homeAddress.ContactTelNo = ds_Call.Tables[0].Rows[0]["PatientContactNo"].ToString().Trim();
                            homeAddress.AlternateContactTelNo = dr["AlternatePhone"].ToString().Trim();
                            try
                            {
                                homeAddress.RelationshipId = Convert.ToInt32(ds_Call.Tables[0].Rows[0]["RelationShipID"]);
                            }
                            catch { }
                            journey.HomeAddress = homeAddress;
                            #endregion

                            #region CollectionAddress
                            collectionAddress = new CollectionAddressModel();

                            collectionAddress.IsThisPatientHomeAddress = (Boolean)ds_Call.Tables[0].Rows[0]["PickIsPatientHomeAddress"];
                            collectionAddress.LineOne = ds_Call.Tables[0].Rows[0]["PickAddressLine1"].ToString().Trim();
                            collectionAddress.LineTwo = ds_Call.Tables[0].Rows[0]["PickAddressLine2"].ToString().Trim();
                            collectionAddress.LineThree = ds_Call.Tables[0].Rows[0]["PickTown"].ToString().Trim();
                            collectionAddress.LineFour = ds_Call.Tables[0].Rows[0]["PickCounty"].ToString().Trim();
                            collectionAddress.PostCode = ds_Call.Tables[0].Rows[0]["PickPostCode"].ToString().Trim();

                            collectionAddress.Easting = ds_Call.Tables[0].Rows[0]["PickEasting"].ToString().Trim();// Convert.ToDouble(ds_Call.Tables[0].Rows[0]["PatientEasting"].ToString() == "" ? "0" : ds_Call.Tables[0].Rows[0]["PatientEasting"].ToString().Trim());
                            collectionAddress.Northing = ds_Call.Tables[0].Rows[0]["PickNorthing"].ToString().Trim();
                            collectionAddress.GridReference = ds_Call.Tables[0].Rows[0]["PickGridReference"].ToString().Trim();
                            collectionAddress.Longitude = ds_Call.Tables[0].Rows[0]["PickLongitude"].ToString().Trim();
                            collectionAddress.Latitude = ds_Call.Tables[0].Rows[0]["PickLatitude"].ToString().Trim();
                            collectionAddress.UPRN = ds_Call.Tables[0].Rows[0]["PickURPN"].ToString().Trim();

                            collectionAddress.ContactTelNo = (string)ds_Call.Tables[0].Rows[0]["PickPhoneNo"];
                            collectionAddress.ExtensionNo = (string)ds_Call.Tables[0].Rows[0]["PickExtention"];
                            try
                            {
                                collectionAddress.FacilityId =       Convert.ToInt32(ds_Call.Tables[0].Rows[0]["PickFacilityID"].ToString().Trim() == "" ? "0" : ds_Call.Tables[0].Rows[0]["PickFacilityID"].ToString().Trim());
                                collectionAddress.FacilityTypeId =   Convert.ToInt32(ds_Call.Tables[0].Rows[0]["PickFacilityTypeID"].ToString() == "-1" ? "5" : ds_Call.Tables[0].Rows[0]["PickFacilityTypeID"].ToString());
                                collectionAddress.DepartmentId =     Convert.ToInt32(ds_Call.Tables[0].Rows[0]["PickDepartmentID"].ToString().Trim() == "" ? "0" : ds_Call.Tables[0].Rows[0]["PickDepartmentID"].ToString().Trim());
                            }
                            catch { }
                            journey.CollectionAddress = collectionAddress;
                            #endregion

                            #region DestinationAddress
                            destinationAddress = new DestinationAddressModel();
                            destinationAddress.IsThisPatientHomeAddress = (Boolean)ds_Call.Tables[0].Rows[0]["DropIsPatientHomeAddress"];
                            destinationAddress.LineOne = ds_Call.Tables[0].Rows[0]["DropAddressLine1"].ToString().Trim();
                            destinationAddress.LineTwo = ds_Call.Tables[0].Rows[0]["DropAddressLine2"].ToString().Trim();
                            destinationAddress.LineThree = ds_Call.Tables[0].Rows[0]["DropTown"].ToString().Trim();
                            destinationAddress.LineFour = ds_Call.Tables[0].Rows[0]["DropCounty"].ToString().Trim();
                            destinationAddress.PostCode = ds_Call.Tables[0].Rows[0]["DropPostCode"].ToString().Trim();

                            destinationAddress.Easting = ds_Call.Tables[0].Rows[0]["DropEasting"].ToString().Trim();// Convert.ToDouble(ds_Call.Tables[0].Rows[0]["PatientEasting"].ToString() == "" ? "0" : ds_Call.Tables[0].Rows[0]["PatientEasting"].ToString().Trim());
                            destinationAddress.Northing = ds_Call.Tables[0].Rows[0]["DropNorthing"].ToString().Trim();
                            destinationAddress.GridReference = ds_Call.Tables[0].Rows[0]["DropGridReference"].ToString().Trim();
                            destinationAddress.Longitude = ds_Call.Tables[0].Rows[0]["DropLongitude"].ToString().Trim();
                            destinationAddress.Latitude = ds_Call.Tables[0].Rows[0]["DropLatitude"].ToString().Trim();
                            destinationAddress.UPRN = ds_Call.Tables[0].Rows[0]["DropURPN"].ToString().Trim();

                            destinationAddress.ContactTelNo = ds_Call.Tables[0].Rows[0]["DropPhoneNo"].ToString().Trim();
                            destinationAddress.ExtensionNo = ds_Call.Tables[0].Rows[0]["DropExtention"].ToString().Trim();
                            try
                            {
                                destinationAddress.FacilityId = Convert.ToInt32(ds_Call.Tables[0].Rows[0]["DropFacilityID"].ToString().Trim() == "" ? "0" : ds_Call.Tables[0].Rows[0]["DropFacilityID"].ToString().Trim());
                                destinationAddress.FacilityTypeId = Convert.ToInt32(ds_Call.Tables[0].Rows[0]["DropFacilityTypeID"].ToString() == "-1" ? "5" : ds_Call.Tables[0].Rows[0]["DropFacilityTypeID"].ToString());
                                destinationAddress.DepartmentId = Convert.ToInt32(ds_Call.Tables[0].Rows[0]["DropDepartmentID"].ToString().Trim() == "" ? "0" : ds_Call.Tables[0].Rows[0]["DropDepartmentID"].ToString().Trim());
                            }
                            catch { }
                            journey.DestinationAddress = destinationAddress;
                            #endregion
                            #region transport Requirement
                            transportRequirement = new TransportRequirementModel();
                            transportRequirement.Id = Convert.ToInt32(dr["BookingId"]);
                            transportRequirement.TransportRequestReasonId = ds_Call.Tables[0].Rows[0]["TransportRequestReasonID"].ToString() == "" ? 0 : Convert.ToInt32(dr["TransportRequestReasonID"]);
                            transportRequirement.TransportSelectionId = ds_Call.Tables[0].Rows[0]["TransportSelectionID"].ToString() == "" ? 0 : Convert.ToInt32(dr["TransportSelectionID"]);
                            transportRequirement.IsInfectious = (Boolean)ds_Call.Tables[0].Rows[0]["IsPatientInfectious"];
                            transportRequirement.IsBariatric = (Boolean)ds_Call.Tables[0].Rows[0]["Bariatric"];
                            transportRequirement.IsDiabetic = (Boolean)ds_Call.Tables[0].Rows[0]["Diabetic"];
                            transportRequirement.IsDNACPR = (Boolean)ds_Call.Tables[0].Rows[0]["DNACPR"];
                            transportRequirement.IsElectricWheelchair = (Boolean)ds_Call.Tables[0].Rows[0]["ElectricWheelChair"];
                            transportRequirement.IsEscortTravelling = (Boolean)ds_Call.Tables[0].Rows[0]["EscortTravelling"];
                            transportRequirement.IsFullLegPlasterPOP = (Boolean)ds_Call.Tables[0].Rows[0]["FulllegPlaster"];
                            transportRequirement.IsNoneOfAbove = (Boolean)ds_Call.Tables[0].Rows[0]["NonofAboveAllergy"];
                            transportRequirement.IsNuclearMedicineRadioActiveRisk = (Boolean)ds_Call.Tables[0].Rows[0]["NuclearMedicine"];
                            transportRequirement.IsTravellingWithOwnOxygen = (Boolean)ds_Call.Tables[0].Rows[0]["TravellingWithOwnOxygen"];
                            transportRequirement.IsWaterlow = (Boolean)ds_Call.Tables[0].Rows[0]["WaterFlow"];
                            transportRequirement.IsWheelchairAndLegExtension = (Boolean)ds_Call.Tables[0].Rows[0]["WheelChairLegExtention"];
                            transportRequirement.InfectiousId = ds_Call.Tables[0].Rows[0]["InfectionId"].ToString() == "" ? 0 : Convert.ToInt32(ds_Call.Tables[0].Rows[0]["InfectionId"]);
                            transportRequirement.EscortTypeId = ds_Call.Tables[0].Rows[0]["EscortType"].ToString() == "" ? 0 : Convert.ToInt32(ds_Call.Tables[0].Rows[0]["EscortType"]);
                            transportRequirement.EscortNumberId = ds_Call.Tables[0].Rows[0]["EscortNumber"].ToString() == "" ? 0 : Convert.ToInt32(ds_Call.Tables[0].Rows[0]["EscortNumber"]);
                            transportRequirement.Id = dr["BookingID"].ToString() == "" ? 0 : Convert.ToInt32(dr["BookingID"]);
                            //transportRequirement.AdditionalPatientInfo=dr[""]
                            // transportRequirement.EscortNumberId
                            journey.TransportRequirement = transportRequirement;
                            #endregion

                            #region journeyriskAssessment
                            riskAssessment = new RiskAssessmentModel();
                            riskAssessment.IsManualHandlingProfileCarriedOutNo = (Boolean)ds_Call.Tables[0].Rows[0]["IsFullManualHandling"] == true ? false : true;
                            riskAssessment.IsManualHandlingProfileCarriedOutYes = (Boolean)ds_Call.Tables[0].Rows[0]["IsFullManualHandling"] == true ? true : false;
                            riskAssessment.PatientRiskAssessment = ds_Call.Tables[0].Rows[0]["PatientRiskAssessment"].ToString();
                            riskAssessment.PropertyRiskAssessment = ds_Call.Tables[0].Rows[0]["PropertyRiskAssessment"].ToString();

                            journey.RiskAssessment = riskAssessment;
                            #endregion

                            #region SpeicalTransport Request
                            specialistTransportRequest = new SpecialistTransportRequestModel();
                            specialistTransportRequest.AuthorisingConsultantOrGP = ds_Call.Tables[0].Rows[0]["AuthorizingConsultant"].ToString();
                            specialistTransportRequest.AuthorisingPracticeName = ds_Call.Tables[0].Rows[0]["AuthorizingPractice"].ToString();
                            specialistTransportRequest.IsAdmission = (Boolean)ds_Call.Tables[0].Rows[0]["IsAdmissionToNursing"];
                            specialistTransportRequest.IsHandledByProfessional = (Boolean)ds_Call.Tables[0].Rows[0]["TrainedPersonRequired"];
                            specialistTransportRequest.IsOxygenTheropy = (Boolean)ds_Call.Tables[0].Rows[0]["OxygenTherapyRequired"];
                            specialistTransportRequest.IsPrecludesTravelling = (Boolean)ds_Call.Tables[0].Rows[0]["HasDisability"];
                            specialistTransportRequest.IsVisit = (Boolean)ds_Call.Tables[0].Rows[0]["VisitToandFromResidentialCareHome"];
                            specialistTransportRequest.IsVisitOrAdmitted = (Boolean)ds_Call.Tables[0].Rows[0]["VisittoHospitalorHospice"];
                            specialistTransportRequest.IsWhilstLayingDown = (Boolean)ds_Call.Tables[0].Rows[0]["WhilstLayingDown"];
                            journey.SpecialistTransportRequest = specialistTransportRequest;
                            #endregion
                            journeyList.Add(journey);
                        }

                    }
                }






            }

            return journeyList;
        }

        public dynamic GetJourneyListFilteredJourneyDate(string Status, string JourneyDateFrom, string JourneyDateDateTo, string BookingNumber)
        {

            List<CompositeModel> journeyList = new List<CompositeModel>();

            var patient = new PatientModel();
            var homeAddress = new HomeAddressModel();
            var collectionAddress = new CollectionAddressModel();
            var destinationAddress = new DestinationAddressModel();
            var transportRequirement = new TransportRequirementModel();
            var riskAssessment = new RiskAssessmentModel();
            var specialistTransportRequest = new SpecialistTransportRequestModel();

            var journey = new CompositeModel();

            clsEBooking objEbooking = new clsEBooking();
            DataSet dsJourney = objEbooking.GetJourneys_Filtered_On_JourneyDate(0, Status, JourneyDateFrom, JourneyDateDateTo, BookingNumber);
            if (dsJourney.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsJourney.Tables[0].Rows)
                {
                    ///Explanation for the conditions applied below
                    ///* 1- If Case ID yet not assigned i-e not approved.
                    ///* 2- Case approved but made Amendments that are yet to be Approved/Rejected. 
                    ///* 3- The case Has been approved earlier but now its status has been rejected. 
                    /// In all the mentioned cases the data displayed will be from web schema. Otherwise the data from Calltaking schema would be displayed. 
                    /// 


                    if (dr["CaseID"].ToString().Trim() == "" || dr["LastBookingStatusID"].ToString().Trim() == "1" || dr["LastBookingStatusID"].ToString().Trim() == "3")
                    {
                        #region Patient
                        patient = new PatientModel();
                        journey = new CompositeModel();
                        patient.PatientID = Convert.ToInt32(dr["PatientId"]);
                        patient.IsRiskAssessmentRequired = (Boolean)dr["IsRiskAssessmentRequired"];
                        patient.IsMainlandRepatriation = (Boolean)dr["IsFormainLand"];
                        try
                        {
                            patient.JourneyDate = Convert.ToDateTime(dr["JourneyDate"]);
                            patient.BookingDateTime = Convert.ToDateTime(dr["InsertedAt"]);
                        }
                        catch { }
                        try
                        {
                            patient.AppointmentTimeId = Convert.ToInt32(dr["StandardAppointmentTimeID"]);
                            patient.EstimatedAppointmentDurationId = Convert.ToInt32(dr["EstimatedDurationofAppointment"]);
                        }
                        catch { }
                        patient.ActualAppointmentTime = dr["ActualAppointmentTime"].ToString().Substring(0, 5);
                        patient.GenderTitleId = Convert.ToInt32(dr["TitleId"]);
                        patient.FirstName = (string)dr["FirstName"];
                        patient.Surname = (string)dr["SurName"];
                        try
                        {
                            patient.BirthDate = Convert.ToDateTime(dr["DOB"]);

                        }
                        catch { }
                        patient.NHSNumber = (string)dr["NHSNumber"];
                        patient.IsleOfWightNo = (string)dr["IOWNumber"];
                        patient.NameOfGP = (string)dr["GPName"];
                        try
                        {


                            patient.WeighingDate = Convert.ToDateTime(dr["WeightDate"]);
                            patient.LastRecordedPatientWeight = Convert.ToDouble(dr["Weight"]);
                        }
                        catch { }
                        try
                        {
                            patient.GPPracticeId = Convert.ToInt32(dr["GPPracticeID"]);

                        }
                        catch { }
                        patient.GPPracticeAddressLineOne = dr["GpAddressLine1"].ToString().Trim();
                        patient.GPPracticeAddressLineTwo = dr["GpAddressLine2"].ToString().Trim();
                        patient.GPPracticeAddressLineThree = dr["GpAddressLine3"].ToString().Trim();
                        patient.GPPracticeAddressLineFour = dr["GpAddressLine4"].ToString().Trim();
                        patient.GPPracticeAddressPostCode = dr["GpAddressPostCode"].ToString().Trim();
                        patient.ContactTelephoneNo = dr["GPPhoneNo"].ToString().Trim();
                        patient.BookingNo = dr["BookingNumber"].ToString();
                        patient.BookingStatusName = dr["CaseID"].ToString().Trim() == "" ? dr["BookingStatusName"].ToString() : "Amendment Request";
                        patient.RequesterSubjectiveCode = dr["SubjectiveCode"].ToString();
                        patient.RequesterCostCenter = dr["CostCenter"].ToString();
                        patient.RequesterAuthorizingClinician = dr["AuthoirizingClinical"].ToString();
                        patient.RequesterAuthorizingRoleId = Convert.ToInt32(dr["ClinicianRole"].ToString() == "" ? "0" : dr["ClinicianRole"].ToString());
                        patient.RejectionReason = dr["LastBookingStatusNotes"].ToString().Trim().ToLower().IndexOf("new booking") >= 0 ? "" : dr["LastBookingStatusNotes"].ToString().Trim();
                        patient.LastStatusAt = Convert.ToDateTime(dr["LastStatusAt"]);
                        patient.isPrivatePatient = dr["isPrivatePatient"].ToString().Trim() == "" ? false : Convert.ToBoolean(dr["isPrivatePatient"]);
                        journey.Patient = patient;
                        #endregion
                        #region HomeAddress

                        homeAddress = new HomeAddressModel();


                        homeAddress.IsNoFixAbode = (Boolean)dr["HasNoAdobe"];
                        homeAddress.LineOne = dr["AddressLine1"].ToString().Trim();
                        homeAddress.LineTwo = dr["AddressLine2"].ToString().Trim();
                        homeAddress.LineThree = dr["AddressLine3"].ToString().Trim();
                        homeAddress.LineFour = dr["AddressLine4"].ToString().Trim();
                        homeAddress.PostCode = dr["PostCode"].ToString().Trim();

                        homeAddress.Easting = dr["Easting"].ToString().Trim();// Convert.ToDouble(dr["PatientEasting"].ToString() == "" ? "0" : dr["PatientEasting"].ToString().Trim());
                        homeAddress.Northing = dr["Northing"].ToString().Trim();
                        homeAddress.GridReference = dr["GridReference"].ToString().Trim();
                        homeAddress.Longitude = dr["Longitude"].ToString().Trim();
                        homeAddress.Latitude = dr["Latitude"].ToString().Trim();
                        homeAddress.UPRN = dr["URPN"].ToString().Trim();

                        homeAddress.ContactTelNo = dr["HomePhone"].ToString().Trim();
                        homeAddress.AlternateContactTelNo = dr["AlternatePhone"].ToString().Trim();
                        try
                        {
                            homeAddress.RelationshipId = Convert.ToInt32(dr["RelationShipID"]);
                        }
                        catch { }
                        journey.HomeAddress = homeAddress;
                        #endregion

                        #region CollectionAddress
                        collectionAddress = new CollectionAddressModel();

                        collectionAddress.IsThisPatientHomeAddress = (Boolean)dr["IsPickHomeAddress"];
                        collectionAddress.LineOne = dr["PickAddressLine1"].ToString().Trim();
                        collectionAddress.LineTwo = dr["PickAddressLine2"].ToString().Trim();
                        collectionAddress.LineThree = dr["PickAddressLine3"].ToString().Trim();
                        collectionAddress.LineFour = dr["PickAddressLine4"].ToString().Trim();
                        collectionAddress.PostCode = dr["PickPostCode"].ToString().Trim();

                        collectionAddress.Easting = dr["PickEasting"].ToString().Trim();// Convert.ToDouble(dr["PatientEasting"].ToString() == "" ? "0" : dr["PatientEasting"].ToString().Trim());
                        collectionAddress.Northing = dr["PickNorthing"].ToString().Trim();
                        collectionAddress.GridReference = dr["PickGridReference"].ToString().Trim();
                        collectionAddress.Longitude = dr["PickLongitude"].ToString().Trim();
                        collectionAddress.Latitude = dr["PickLatitude"].ToString().Trim();
                        collectionAddress.UPRN = dr["PickURPN"].ToString().Trim();

                        collectionAddress.ContactTelNo = dr["PickContactPhone"].ToString().Trim();
                        collectionAddress.ExtensionNo = dr["PickExtention"].ToString().Trim();
                        try
                        {
                            collectionAddress.FacilityId = Convert.ToInt32(dr["PickFacilityID"]);
                            //collectionAddress.FacilityTypeId = (Boolean)dr["IsPickHomeAddress"];
                            collectionAddress.FacilityTypeId = dr["CollectionFacilityTypeId"].ToString() == "" ? 0 : Convert.ToInt32(dr["CollectionFacilityTypeId"]);
                            collectionAddress.DepartmentId = Convert.ToInt32(dr["PickDepartmentID"]);
                        }
                        catch { }
                        journey.CollectionAddress = collectionAddress;
                        #endregion

                        #region DestinationAddress
                        destinationAddress = new DestinationAddressModel();
                        destinationAddress.IsThisPatientHomeAddress = (Boolean)dr["IsDropHomeAddress"];
                        destinationAddress.LineOne = dr["DropAddressLine1"].ToString().Trim();
                        destinationAddress.LineTwo = dr["DropAddressLine2"].ToString().Trim();
                        destinationAddress.LineThree = dr["DropAddressLine3"].ToString().Trim();
                        destinationAddress.LineFour = dr["DropAddressLine4"].ToString().Trim();
                        destinationAddress.PostCode = dr["DropPostCode"].ToString().Trim();

                        destinationAddress.Easting = dr["DropEasting"].ToString().Trim();// Convert.ToDouble(dr["PatientEasting"].ToString() == "" ? "0" : dr["PatientEasting"].ToString().Trim());
                        destinationAddress.Northing = dr["DropNorthing"].ToString().Trim();
                        destinationAddress.GridReference = dr["DropGridReference"].ToString().Trim();
                        destinationAddress.Longitude = dr["DropLongitude"].ToString().Trim();
                        destinationAddress.Latitude = dr["DropLatitude"].ToString().Trim();
                        destinationAddress.UPRN = dr["DropURPN"].ToString().Trim();



                        destinationAddress.ContactTelNo = dr["DropContactPhone"].ToString().Trim();
                        destinationAddress.ExtensionNo = dr["DropExtention"].ToString().Trim();
                        try
                        {
                            destinationAddress.FacilityId = Convert.ToInt32(dr["DropFacilityID"]);
                            destinationAddress.FacilityTypeId = dr["DestinationFacilityTypeId"].ToString() == "" ? 0 : Convert.ToInt32(dr["DestinationFacilityTypeId"]);
                            //collectionAddress.FacilityTypeId = (Boolean)dr["IsPickHomeAddress"];
                            destinationAddress.DepartmentId = Convert.ToInt32(dr["DropDepartmentID"]);
                        }
                        catch { }
                        journey.DestinationAddress = destinationAddress;
                        #endregion
                        #region transport Requirement
                        transportRequirement = new TransportRequirementModel();
                        transportRequirement.Id = Convert.ToInt32(dr["BookingId"]);
                        transportRequirement.TransportRequestReasonId = dr["TransportRequestReasonID"].ToString() == "" ? 0 : Convert.ToInt32(dr["TransportRequestReasonID"]);
                        transportRequirement.TransportSelectionId = dr["TransportSelectionID"].ToString() == "" ? 0 : Convert.ToInt32(dr["TransportSelectionID"]);
                        transportRequirement.IsInfectious = (Boolean)dr["isPatientInfectious"];
                        transportRequirement.IsBariatric = (Boolean)dr["Bariatric"];
                        transportRequirement.IsDiabetic = (Boolean)dr["Diabetic"];
                        transportRequirement.IsDNACPR = (Boolean)dr["DNACPR"];
                        transportRequirement.IsElectricWheelchair = (Boolean)dr["ElectricWheelChair"];
                        transportRequirement.IsEscortTravelling = (Boolean)dr["EscortTravelling"];
                        transportRequirement.IsFullLegPlasterPOP = (Boolean)dr["FullLegPlaster"];
                        transportRequirement.IsNoneOfAbove = (Boolean)dr["Non"];
                        transportRequirement.IsNuclearMedicineRadioActiveRisk = (Boolean)dr["NuclearMedicine"];
                        transportRequirement.IsTravellingWithOwnOxygen = (Boolean)dr["TravellingWithOwnOxygen"];
                        transportRequirement.IsWaterlow = (Boolean)dr["WaterFlow"];
                        transportRequirement.IsWheelchairAndLegExtension = (Boolean)dr["WheelChairLegExtention"];
                        transportRequirement.InfectiousId = dr["InfectionID"].ToString() == "" ? 0 : Convert.ToInt32(dr["InfectionID"]);
                        transportRequirement.EscortTypeId = dr["EscortType"].ToString() == "" ? 0 : Convert.ToInt32(dr["EscortType"]);
                        transportRequirement.EscortNumberId = dr["EscortNumber"].ToString() == "" ? 0 : Convert.ToInt32(dr["EscortNumber"]);
                        transportRequirement.Id = dr["BookingID"].ToString() == "" ? 0 : Convert.ToInt32(dr["BookingID"]);
                        transportRequirement.AdditionalPatientInfo = dr["PatientAdditionalInfo"].ToString().Trim();
                        // transportRequirement.EscortNumberId
                        journey.TransportRequirement = transportRequirement;
                        #endregion

                        #region journeyriskAssessment
                        riskAssessment = new RiskAssessmentModel();
                        riskAssessment.IsManualHandlingProfileCarriedOutNo = (Boolean)dr["IsFullManualHandling"] == true ? false : true;
                        riskAssessment.IsManualHandlingProfileCarriedOutYes = (Boolean)dr["IsFullManualHandling"] == true ? true : false;
                        riskAssessment.PatientRiskAssessment = dr["PatientRiskAssessment"].ToString();
                        riskAssessment.PropertyRiskAssessment = dr["PropertyRiskAssessment"].ToString();

                        journey.RiskAssessment = riskAssessment;
                        #endregion

                        #region SpeicalTransport Request
                        specialistTransportRequest = new SpecialistTransportRequestModel();
                        specialistTransportRequest.AuthorisingConsultantOrGP = dr["AuthorizingConsultant"].ToString();
                        specialistTransportRequest.AuthorisingPracticeName = dr["AuthorizingPractice"].ToString();
                        specialistTransportRequest.IsAdmission = (Boolean)dr["IsAdmissionToNursing"];
                        specialistTransportRequest.IsHandledByProfessional = (Boolean)dr["TrainedPersonRequired"];
                        specialistTransportRequest.IsOxygenTheropy = (Boolean)dr["OxygenTherapyRequired"];
                        specialistTransportRequest.IsPrecludesTravelling = (Boolean)dr["HasDisability"];
                        specialistTransportRequest.IsVisit = (Boolean)dr["VisitToandFromResidentialCareHome"];
                        specialistTransportRequest.IsVisitOrAdmitted = (Boolean)dr["VisitToHospitalorHospice"];
                        specialistTransportRequest.IsWhilstLayingDown = (Boolean)dr["WhilstLayingDown"];
                        journey.SpecialistTransportRequest = specialistTransportRequest;
                        #endregion
                        journeyList.Add(journey);
                    }
                    else
                    {
                        clsCall objCall = new clsCall();
                        DataSet ds_Call = objCall.LoadCase(Convert.ToInt32(dr["CaseID"].ToString().Trim()));
                        if (ds_Call.Tables[0].Rows.Count > 0)
                        {
                            #region Patient
                            patient = new PatientModel();
                            journey = new CompositeModel();
                            patient.PatientID = Convert.ToInt32(dr["PatientId"]);
                            patient.IsRiskAssessmentRequired = Convert.ToBoolean(ds_Call.Tables[0].Rows[0]["IsRiskAssessmentRequired"].ToString().Trim() == "" ? 0 : ds_Call.Tables[0].Rows[0]["IsRiskAssessmentRequired"]);
                            patient.IsMainlandRepatriation = Convert.ToBoolean(ds_Call.Tables[0].Rows[0]["IsForMainlandRepatriation"].ToString().Trim() == "" ? 0 : ds_Call.Tables[0].Rows[0]["IsForMainlandRepatriation"]);
                            try
                            {
                                patient.JourneyDate = Convert.ToDateTime(ds_Call.Tables[0].Rows[0]["CollectionTimeFrom"]);
                                patient.BookingDateTime = Convert.ToDateTime(dr["InsertedAt"]);
                            }
                            catch { }
                            try
                            {
                                patient.AppointmentTimeId = Convert.ToInt32(ds_Call.Tables[0].Rows[0]["AppointmentTimeID"]);
                                patient.EstimatedAppointmentDurationId = Convert.ToInt32(ds_Call.Tables[0].Rows[0]["AppointmentDurationID"]);
                            }
                            catch { }
                            patient.ActualAppointmentTime = Convert.ToDateTime(ds_Call.Tables[0].Rows[0]["AppointmentTime"].ToString()).ToString("HH:mm");
                            patient.GenderTitleId = Convert.ToInt32(ds_Call.Tables[0].Rows[0]["titleid"]);
                            patient.FirstName = (string)ds_Call.Tables[0].Rows[0]["firstname"];
                            patient.Surname = (string)ds_Call.Tables[0].Rows[0]["surname"];
                            try
                            {
                                patient.BirthDate = Convert.ToDateTime(ds_Call.Tables[0].Rows[0]["dob"]);

                            }
                            catch { }
                            patient.NHSNumber = (string)ds_Call.Tables[0].Rows[0]["nhsno"];
                            patient.IsleOfWightNo = "IW" + ((string)ds_Call.Tables[0].Rows[0]["nationalid"]).Replace("IW", "");
                            patient.NameOfGP = (string)dr["GPName"];
                            try
                            {


                                patient.WeighingDate = Convert.ToDateTime(ds_Call.Tables[0].Rows[0]["PatientWeightDate"]);
                                patient.LastRecordedPatientWeight = Convert.ToDouble(ds_Call.Tables[0].Rows[0]["PatientWeight"]);
                            }
                            catch { }
                            try
                            {
                                patient.GPPracticeId = Convert.ToInt32(ds_Call.Tables[0].Rows[0]["PatientGpPracticeID"]);

                            }
                            catch { }
                            patient.GPPracticeAddressLineOne = dr["GpAddressLine1"].ToString().Trim();
                            patient.GPPracticeAddressLineTwo = dr["GpAddressLine2"].ToString().Trim();
                            patient.GPPracticeAddressLineThree = dr["GpAddressLine3"].ToString().Trim();
                            patient.GPPracticeAddressLineFour = dr["GpAddressLine4"].ToString().Trim();
                            patient.GPPracticeAddressPostCode = dr["GpAddressPostCode"].ToString().Trim();
                            patient.ContactTelephoneNo = dr["GPPhoneNo"].ToString().Trim();
                            patient.BookingNo = dr["BookingNumber"].ToString();
                            patient.BookingStatusName = dr["BookingStatusName"].ToString();
                            patient.RequesterSubjectiveCode = dr["SubjectiveCode"].ToString();
                            patient.RequesterCostCenter = dr["CostCenter"].ToString();
                            patient.RequesterAuthorizingClinician = dr["AuthoirizingClinical"].ToString();
                            patient.RequesterAuthorizingRoleId = Convert.ToInt32(dr["ClinicianRole"].ToString() == "" ? "0" : dr["ClinicianRole"].ToString());
                            patient.RejectionReason = dr["LastBookingStatusNotes"].ToString().Trim().ToLower().IndexOf("new booking") >= 0 ? "" : dr["LastBookingStatusNotes"].ToString().Trim();
                            patient.LastStatusAt = Convert.ToDateTime(dr["LastStatusAt"]);
                            patient.CADCaseID = Convert.ToInt32(dr["CaseID"].ToString().Trim());
                            patient.isPrivatePatient = dr["isPrivatePatient"].ToString().Trim() == "" ? false : Convert.ToBoolean(dr["isPrivatePatient"]);
                            journey.Patient = patient;
                            #endregion
                            #region HomeAddress

                            homeAddress = new HomeAddressModel();


                            homeAddress.IsNoFixAbode = (Boolean)ds_Call.Tables[0].Rows[0]["hasnoabode"];
                            homeAddress.LineOne = ds_Call.Tables[0].Rows[0]["PatientAddressLine1"].ToString().Trim();
                            homeAddress.LineTwo = ds_Call.Tables[0].Rows[0]["PatientAddressLine2"].ToString().Trim();
                            homeAddress.LineThree = ds_Call.Tables[0].Rows[0]["PatientAddressLine3"].ToString().Trim();
                            homeAddress.LineFour = ds_Call.Tables[0].Rows[0]["PatientAddressLine4"].ToString().Trim();
                            homeAddress.PostCode = ds_Call.Tables[0].Rows[0]["PatientPostCode"].ToString().Trim();

                            homeAddress.Easting = ds_Call.Tables[0].Rows[0]["PatientEasting"].ToString().Trim();// Convert.ToDouble(ds_Call.Tables[0].Rows[0]["PatientEasting"].ToString() == "" ? "0" : ds_Call.Tables[0].Rows[0]["PatientEasting"].ToString().Trim());
                            homeAddress.Northing = ds_Call.Tables[0].Rows[0]["PatientNorthing"].ToString().Trim();
                            homeAddress.GridReference = ds_Call.Tables[0].Rows[0]["PatientGridReference"].ToString().Trim();
                            homeAddress.Longitude = ds_Call.Tables[0].Rows[0]["PatientLongitude"].ToString().Trim();
                            homeAddress.Latitude = ds_Call.Tables[0].Rows[0]["PatientLatitude"].ToString().Trim();
                            homeAddress.UPRN = ds_Call.Tables[0].Rows[0]["PatientURPN"].ToString().Trim();

                            homeAddress.ContactTelNo = ds_Call.Tables[0].Rows[0]["PatientContactNo"].ToString().Trim();
                            homeAddress.AlternateContactTelNo = dr["AlternatePhone"].ToString().Trim();
                            try
                            {
                                homeAddress.RelationshipId = Convert.ToInt32(ds_Call.Tables[0].Rows[0]["RelationShipID"]);
                            }
                            catch { }
                            journey.HomeAddress = homeAddress;
                            #endregion

                            #region CollectionAddress
                            collectionAddress = new CollectionAddressModel();

                            collectionAddress.IsThisPatientHomeAddress = (Boolean)ds_Call.Tables[0].Rows[0]["PickIsPatientHomeAddress"];
                            collectionAddress.LineOne = ds_Call.Tables[0].Rows[0]["PickAddressLine1"].ToString().Trim();
                            collectionAddress.LineTwo = ds_Call.Tables[0].Rows[0]["PickAddressLine2"].ToString().Trim();
                            collectionAddress.LineThree = ds_Call.Tables[0].Rows[0]["PickTown"].ToString().Trim();
                            collectionAddress.LineFour = ds_Call.Tables[0].Rows[0]["PickCounty"].ToString().Trim();
                            collectionAddress.PostCode = ds_Call.Tables[0].Rows[0]["PickPostCode"].ToString().Trim();

                            collectionAddress.Easting = ds_Call.Tables[0].Rows[0]["PickEasting"].ToString().Trim();// Convert.ToDouble(ds_Call.Tables[0].Rows[0]["PatientEasting"].ToString() == "" ? "0" : ds_Call.Tables[0].Rows[0]["PatientEasting"].ToString().Trim());
                            collectionAddress.Northing = ds_Call.Tables[0].Rows[0]["PickNorthing"].ToString().Trim();
                            collectionAddress.GridReference = ds_Call.Tables[0].Rows[0]["PickGridReference"].ToString().Trim();
                            collectionAddress.Longitude = ds_Call.Tables[0].Rows[0]["PickLongitude"].ToString().Trim();
                            collectionAddress.Latitude = ds_Call.Tables[0].Rows[0]["PickLatitude"].ToString().Trim();
                            collectionAddress.UPRN = ds_Call.Tables[0].Rows[0]["PickURPN"].ToString().Trim();

                            collectionAddress.ContactTelNo = (string)ds_Call.Tables[0].Rows[0]["PickPhoneNo"];
                            collectionAddress.ExtensionNo = (string)ds_Call.Tables[0].Rows[0]["PickExtention"];
                            try
                            {
                                collectionAddress.FacilityId = Convert.ToInt32(ds_Call.Tables[0].Rows[0]["PickFacilityID"].ToString().Trim() == "" ? "0" : ds_Call.Tables[0].Rows[0]["PickFacilityID"].ToString().Trim());
                                collectionAddress.FacilityTypeId = Convert.ToInt32(ds_Call.Tables[0].Rows[0]["PickFacilityTypeID"].ToString() == "-1" ? "5" : ds_Call.Tables[0].Rows[0]["PickFacilityTypeID"].ToString());
                                collectionAddress.DepartmentId = Convert.ToInt32(ds_Call.Tables[0].Rows[0]["PickDepartmentID"].ToString().Trim() == "" ? "0" : ds_Call.Tables[0].Rows[0]["PickDepartmentID"].ToString().Trim());
                            }
                            catch { }
                            journey.CollectionAddress = collectionAddress;
                            #endregion

                            #region DestinationAddress
                            destinationAddress = new DestinationAddressModel();
                            destinationAddress.IsThisPatientHomeAddress = (Boolean)ds_Call.Tables[0].Rows[0]["DropIsPatientHomeAddress"];
                            destinationAddress.LineOne = ds_Call.Tables[0].Rows[0]["DropAddressLine1"].ToString().Trim();
                            destinationAddress.LineTwo = ds_Call.Tables[0].Rows[0]["DropAddressLine2"].ToString().Trim();
                            destinationAddress.LineThree = ds_Call.Tables[0].Rows[0]["DropTown"].ToString().Trim();
                            destinationAddress.LineFour = ds_Call.Tables[0].Rows[0]["DropCounty"].ToString().Trim();
                            destinationAddress.PostCode = ds_Call.Tables[0].Rows[0]["DropPostCode"].ToString().Trim();

                            destinationAddress.Easting = ds_Call.Tables[0].Rows[0]["DropEasting"].ToString().Trim();// Convert.ToDouble(ds_Call.Tables[0].Rows[0]["PatientEasting"].ToString() == "" ? "0" : ds_Call.Tables[0].Rows[0]["PatientEasting"].ToString().Trim());
                            destinationAddress.Northing = ds_Call.Tables[0].Rows[0]["DropNorthing"].ToString().Trim();
                            destinationAddress.GridReference = ds_Call.Tables[0].Rows[0]["DropGridReference"].ToString().Trim();
                            destinationAddress.Longitude = ds_Call.Tables[0].Rows[0]["DropLongitude"].ToString().Trim();
                            destinationAddress.Latitude = ds_Call.Tables[0].Rows[0]["DropLatitude"].ToString().Trim();
                            destinationAddress.UPRN = ds_Call.Tables[0].Rows[0]["DropURPN"].ToString().Trim();

                            destinationAddress.ContactTelNo = ds_Call.Tables[0].Rows[0]["DropPhoneNo"].ToString().Trim();
                            destinationAddress.ExtensionNo = ds_Call.Tables[0].Rows[0]["DropExtention"].ToString().Trim();
                            try
                            {
                                destinationAddress.FacilityId = Convert.ToInt32(ds_Call.Tables[0].Rows[0]["DropFacilityID"].ToString().Trim() == "" ? "0" : ds_Call.Tables[0].Rows[0]["DropFacilityID"].ToString().Trim());
                                destinationAddress.FacilityTypeId = Convert.ToInt32(ds_Call.Tables[0].Rows[0]["DropFacilityTypeID"].ToString() == "-1" ? "5" : ds_Call.Tables[0].Rows[0]["DropFacilityTypeID"].ToString());
                                destinationAddress.DepartmentId = Convert.ToInt32(ds_Call.Tables[0].Rows[0]["DropDepartmentID"].ToString().Trim() == "" ? "0" : ds_Call.Tables[0].Rows[0]["DropDepartmentID"].ToString().Trim());
                            }
                            catch { }
                            journey.DestinationAddress = destinationAddress;
                            #endregion
                            #region transport Requirement
                            transportRequirement = new TransportRequirementModel();
                            transportRequirement.Id = Convert.ToInt32(dr["BookingId"]);
                            transportRequirement.TransportRequestReasonId = ds_Call.Tables[0].Rows[0]["TransportRequestReasonID"].ToString() == "" ? 0 : Convert.ToInt32(dr["TransportRequestReasonID"]);
                            transportRequirement.TransportSelectionId = ds_Call.Tables[0].Rows[0]["TransportSelectionID"].ToString() == "" ? 0 : Convert.ToInt32(dr["TransportSelectionID"]);
                            transportRequirement.IsInfectious = (Boolean)ds_Call.Tables[0].Rows[0]["IsPatientInfectious"];
                            transportRequirement.IsBariatric = (Boolean)ds_Call.Tables[0].Rows[0]["Bariatric"];
                            transportRequirement.IsDiabetic = (Boolean)ds_Call.Tables[0].Rows[0]["Diabetic"];
                            transportRequirement.IsDNACPR = (Boolean)ds_Call.Tables[0].Rows[0]["DNACPR"];
                            transportRequirement.IsElectricWheelchair = (Boolean)ds_Call.Tables[0].Rows[0]["ElectricWheelChair"];
                            transportRequirement.IsEscortTravelling = (Boolean)ds_Call.Tables[0].Rows[0]["EscortTravelling"];
                            transportRequirement.IsFullLegPlasterPOP = (Boolean)ds_Call.Tables[0].Rows[0]["FulllegPlaster"];
                            transportRequirement.IsNoneOfAbove = (Boolean)ds_Call.Tables[0].Rows[0]["NonofAboveAllergy"];
                            transportRequirement.IsNuclearMedicineRadioActiveRisk = (Boolean)ds_Call.Tables[0].Rows[0]["NuclearMedicine"];
                            transportRequirement.IsTravellingWithOwnOxygen = (Boolean)ds_Call.Tables[0].Rows[0]["TravellingWithOwnOxygen"];
                            transportRequirement.IsWaterlow = (Boolean)ds_Call.Tables[0].Rows[0]["WaterFlow"];
                            transportRequirement.IsWheelchairAndLegExtension = (Boolean)ds_Call.Tables[0].Rows[0]["WheelChairLegExtention"];
                            transportRequirement.InfectiousId = ds_Call.Tables[0].Rows[0]["InfectionId"].ToString() == "" ? 0 : Convert.ToInt32(ds_Call.Tables[0].Rows[0]["InfectionId"]);
                            transportRequirement.EscortTypeId = ds_Call.Tables[0].Rows[0]["EscortType"].ToString() == "" ? 0 : Convert.ToInt32(ds_Call.Tables[0].Rows[0]["EscortType"]);
                            transportRequirement.EscortNumberId = ds_Call.Tables[0].Rows[0]["EscortNumber"].ToString() == "" ? 0 : Convert.ToInt32(ds_Call.Tables[0].Rows[0]["EscortNumber"]);
                            transportRequirement.Id = dr["BookingID"].ToString() == "" ? 0 : Convert.ToInt32(dr["BookingID"]);
                            //transportRequirement.AdditionalPatientInfo=dr[""]
                            // transportRequirement.EscortNumberId
                            journey.TransportRequirement = transportRequirement;
                            #endregion

                            #region journeyriskAssessment
                            riskAssessment = new RiskAssessmentModel();
                            riskAssessment.IsManualHandlingProfileCarriedOutNo = (Boolean)ds_Call.Tables[0].Rows[0]["IsFullManualHandling"] == true ? false : true;
                            riskAssessment.IsManualHandlingProfileCarriedOutYes = (Boolean)ds_Call.Tables[0].Rows[0]["IsFullManualHandling"] == true ? true : false;
                            riskAssessment.PatientRiskAssessment = ds_Call.Tables[0].Rows[0]["PatientRiskAssessment"].ToString();
                            riskAssessment.PropertyRiskAssessment = ds_Call.Tables[0].Rows[0]["PropertyRiskAssessment"].ToString();

                            journey.RiskAssessment = riskAssessment;
                            #endregion

                            #region SpeicalTransport Request
                            specialistTransportRequest = new SpecialistTransportRequestModel();
                            specialistTransportRequest.AuthorisingConsultantOrGP = ds_Call.Tables[0].Rows[0]["AuthorizingConsultant"].ToString();
                            specialistTransportRequest.AuthorisingPracticeName = ds_Call.Tables[0].Rows[0]["AuthorizingPractice"].ToString();
                            specialistTransportRequest.IsAdmission = (Boolean)ds_Call.Tables[0].Rows[0]["IsAdmissionToNursing"];
                            specialistTransportRequest.IsHandledByProfessional = (Boolean)ds_Call.Tables[0].Rows[0]["TrainedPersonRequired"];
                            specialistTransportRequest.IsOxygenTheropy = (Boolean)ds_Call.Tables[0].Rows[0]["OxygenTherapyRequired"];
                            specialistTransportRequest.IsPrecludesTravelling = (Boolean)ds_Call.Tables[0].Rows[0]["HasDisability"];
                            specialistTransportRequest.IsVisit = (Boolean)ds_Call.Tables[0].Rows[0]["VisitToandFromResidentialCareHome"];
                            specialistTransportRequest.IsVisitOrAdmitted = (Boolean)ds_Call.Tables[0].Rows[0]["VisittoHospitalorHospice"];
                            specialistTransportRequest.IsWhilstLayingDown = (Boolean)ds_Call.Tables[0].Rows[0]["WhilstLayingDown"];
                            journey.SpecialistTransportRequest = specialistTransportRequest;
                            #endregion
                            journeyList.Add(journey);
                        }

                    }
                }






            }

            return journeyList;
        }
        private int GetTableIndex(string p, DataSet ds)
        {
            int totalTables = ds.Tables.Count;
            string alltables = ds.Tables[totalTables - 1].Rows[0][0].ToString().Trim();
            string[] tablenames = alltables.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tablenames.Length; i++)
            {
                if (p.ToLower() == tablenames[i].ToLower())
                {
                    return i;
                }
            }

            return -1;
        }
        #endregion
    }
}
