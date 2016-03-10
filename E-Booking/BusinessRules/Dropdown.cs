using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EBooking.Models;
using System.Data;
//using System.Web.Caching;
using System.Runtime.Caching;

namespace BusinessRules
{
    public class Dropdown
    {
        private static string _TableNames = "~!@";

        public static string TableNames
        {
            get { return Dropdown._TableNames; }
            set { Dropdown._TableNames = value; }
        }

        #region Get

        public List<dynamic> GetAllDropdowns(bool isFiltered)
        {
            int tableindex = 0;
            try
            {


                DataSet ds = new DataSet();
                StaticCache cache = new StaticCache();
                List<dynamic> dropdownLists = new List<dynamic>();
                #region GEt Data from Web Service
                if (!isFiltered)
                {
                    ds = cache.GetDropDowns(false);
                }
                else
                {
                    clsEBooking objEbooking = new clsEBooking();
                    ds = objEbooking.LoadCache(TableNames);
                }
                #endregion
                List<YourPositionModel> YourPosition = new List<YourPositionModel>();
                tableindex = GetTableIndex("Position", ds);
                if (tableindex != -1)
                {
                    foreach (DataRow dr in ds.Tables[tableindex].Rows)
                    {
                        YourPosition.Add(new YourPositionModel(Convert.ToInt16(dr[0].ToString()), dr[1].ToString()));
                    }
                    //YourPosition.Add(new YourPositionModel(1, "Position 1"));
                    //YourPosition.Add(new YourPositionModel(2, "Position 2"));
                    //YourPosition.Add(new YourPositionModel(3, "Position 3"));
                    dropdownLists.Add(YourPosition);
                }


                List<TitleModel> Titles = new List<TitleModel>();
                tableindex = GetTableIndex("Title", ds);
                if (tableindex != -1)
                {
                    foreach (DataRow dr in ds.Tables[tableindex].Rows)
                    {
                        Titles.Add(new TitleModel(Convert.ToInt16(dr["TitleID"].ToString()), dr["Title"].ToString()));
                    }
                    //Titles.Add(new TitleModel(1, "Mr"));
                    //Titles.Add(new TitleModel(2, "Mrs"));
                    //Titles.Add(new TitleModel(3, "Ms"));
                    //Titles.Add(new TitleModel(4, "Miss"));
                    //Titles.Add(new TitleModel(5, "Dr"));
                    //Titles.Add(new TitleModel(6, "Rev"));
                    //Titles.Add(new TitleModel(7, "Other"));
                    dropdownLists.Add(Titles);
                }
                List<DepartmentModel> departments = new List<DepartmentModel>();///Facility
                tableindex = GetTableIndex("tblHospitalDepartment", ds);
                if (tableindex != -1)
                {

                    for (int i = 0; i < ds.Tables[tableindex].Rows.Count; i++)
                    {
                        departments.Add(new DepartmentModel(Convert.ToInt16(ds.Tables[tableindex].Rows[i][0].ToString()), ds.Tables[tableindex].Rows[i][1].ToString()));
                    }

                    //departments.Add(new DepartmentModel(1, "Department-1"));
                    //departments.Add(new DepartmentModel(2, "Department-2"));
                    //departments.Add(new DepartmentModel(3, "Department-3"));
                    //departments.Add(new DepartmentModel(4, "Department-4"));
                    //departments.Add(new DepartmentModel(5, "Department-5"));
                    dropdownLists.Add(departments);
                }


                List<FacilityTypeModel> facilityTypes = new List<FacilityTypeModel>();
                tableindex = GetTableIndex("FacilityType", ds);
                if (tableindex != -1)
                {
                    foreach (DataRow dr in ds.Tables[tableindex].Rows)
                    {
                        facilityTypes.Add(new FacilityTypeModel(Convert.ToInt16(dr[0].ToString()), dr[1].ToString()));
                    }
                    //facilityTypes.Add(new FacilityTypeModel(1, "Care Home"));
                    //facilityTypes.Add(new FacilityTypeModel(2, "Clinics and Hospice"));
                    //facilityTypes.Add(new FacilityTypeModel(3, "GP Practice"));
                    //facilityTypes.Add(new FacilityTypeModel(4, "Hospital"));
                    //facilityTypes.Add(new FacilityTypeModel(5, "Other Address (manual input)"));

                    dropdownLists.Add(facilityTypes);
                }


                List<FacilityModel> facilities = new List<FacilityModel>();
                tableindex = GetTableIndex("Facility", ds);
                if (tableindex != -1)
                {
                    foreach (DataRow dr in ds.Tables[tableindex].Rows)
                    {
                        facilities.Add(new FacilityModel(Convert.ToInt16(dr[0].ToString()), dr[1].ToString(), Convert.ToInt16(dr[2].ToString())));
                    }
                    //facilities.Add(new FacilityModel(1, "Abbeyfield (Newport)", 1));
                    //facilities.Add(new FacilityModel(2, "Abbeyfield (Ryde)", 1));
                    //facilities.Add(new FacilityModel(3, "Abbeyfield (West Wight)", 1));
                    //facilities.Add(new FacilityModel(4, "Abbeyfield (Yarmouth)", 1));
                    //facilities.Add(new FacilityModel(5, "Abbeyfield Lodge (Ryde)", 1));
                    dropdownLists.Add(facilities);
                }




                //List<GPPracticeModel> gpPractices = new List<GPPracticeModel>();
                //tableindex = GetTableIndex("Facility", ds);
                //if (tableindex != -1)
                //{
                //    //DataTable dt=ds.Tables[tableindex];
                //    DataView dv1 = new DataView(ds.Tables[tableindex], "FacilityTypeID=3", "FacilityTypeID", DataViewRowState.CurrentRows);
                //    // dv1.RowStateFilter = DataViewRowState.Unchanged;
                //    //dv1.RowFilter = "FacilityTypeId=3";//For Gp Practise
                //    //dv=dv.FindRows("FacilityTypeID=3");
                //    for (int i = 0; i < dv1.Count; i++)
                //    {
                //        gpPractices.Add(new GPPracticeModel(Convert.ToInt16(dv1[i][0].ToString()), dv1[i][1].ToString()));
                //    }
                //    dv1.Dispose();

                //    dropdownLists.Add(gpPractices);
                //}

                List<GPPracticeModel> gpPractice = new List<GPPracticeModel>();
                tableindex = GetTableIndex("GPPractice", ds);
                if (tableindex != -1)
                {
                    foreach (DataRow dr in ds.Tables[tableindex].Rows)
                    {
                        gpPractice.Add(new GPPracticeModel(Convert.ToInt16(dr[0].ToString()), dr[1].ToString()));
                    }
                    //facilities.Add(new FacilityModel(1, "Abbeyfield (Newport)", 1));
                    //facilities.Add(new FacilityModel(2, "Abbeyfield (Ryde)", 1));
                    //facilities.Add(new FacilityModel(3, "Abbeyfield (West Wight)", 1));
                    //facilities.Add(new FacilityModel(4, "Abbeyfield (Yarmouth)", 1));
                    //facilities.Add(new FacilityModel(5, "Abbeyfield Lodge (Ryde)", 1));
                    dropdownLists.Add(gpPractice);
                }

                //List<GPPracticeAddressModel> gpPracticeAddresses = new List<GPPracticeAddressModel>();

                //gpPracticeAddresses.Add(new GPPracticeAddressModel(1, "GP Practice Address-1"));
                //gpPracticeAddresses.Add(new GPPracticeAddressModel(2, "GP Practice Address-2"));
                //gpPracticeAddresses.Add(new GPPracticeAddressModel(3, "GP Practice Address-3"));
                //gpPracticeAddresses.Add(new GPPracticeAddressModel(4, "GP Practice Address-4"));
                //gpPracticeAddresses.Add(new GPPracticeAddressModel(5, "GP Practice Address-5"));
                //dropdownLists.Add(gpPracticeAddresses);

                //List<PatientTypeModel> patientTypes = new List<PatientTypeModel>();

                //patientTypes.Add(new PatientTypeModel(1, "Patient Type-1"));
                //patientTypes.Add(new PatientTypeModel(2, "Patient Type-2"));
                //patientTypes.Add(new PatientTypeModel(3, "Patient Type-3"));
                //patientTypes.Add(new PatientTypeModel(4, "Patient Type-4"));
                //patientTypes.Add(new PatientTypeModel(5, "Patient Type-5"));
                //dropdownLists.Add(patientTypes);

                //List<RequestTypeModel> requestTypes = new List<RequestTypeModel>();

                //requestTypes.Add(new RequestTypeModel(1, "Request Type-1"));
                //requestTypes.Add(new RequestTypeModel(2, "Request Type-2"));
                //requestTypes.Add(new RequestTypeModel(3, "Request Type-3"));
                //requestTypes.Add(new RequestTypeModel(4, "Request Type-4"));
                //requestTypes.Add(new RequestTypeModel(5, "Request Type-5"));
                //dropdownLists.Add(requestTypes);

                List<ServiceTypeModel> serviceTypes = new List<ServiceTypeModel>();
                tableindex = GetTableIndex("ServiceType", ds);
                if (tableindex != -1)
                {
                    foreach (DataRow dr in ds.Tables[tableindex].Rows)
                    {
                        serviceTypes.Add(new ServiceTypeModel(Convert.ToInt16(dr[0].ToString()), dr[1].ToString()));
                    }
                    //serviceTypes.Add(new ServiceTypeModel(1, "Service Type 1"));
                    //serviceTypes.Add(new ServiceTypeModel(2, "Service Type 2"));
                    //serviceTypes.Add(new ServiceTypeModel(3, "Service Type 3"));
                    //serviceTypes.Add(new ServiceTypeModel(4, "Service Type 4"));
                    //serviceTypes.Add(new ServiceTypeModel(5, "Service Type 5"));
                    dropdownLists.Add(serviceTypes);
                }


                List<AuthorisingRoleModel> authorisingRoles = new List<AuthorisingRoleModel>();
                tableindex = GetTableIndex("ClinicianRole", ds);
                if (tableindex != -1)
                {
                    foreach (DataRow dr in ds.Tables[tableindex].Rows)
                    {
                        authorisingRoles.Add(new AuthorisingRoleModel(Convert.ToInt16(dr[0].ToString()), dr[1].ToString()));
                    }
                    //authorisingRoles.Add(new AuthorisingRoleModel(1, "Anaethetist"));
                    //authorisingRoles.Add(new AuthorisingRoleModel(2, "Consultant"));
                    //authorisingRoles.Add(new AuthorisingRoleModel(3, "Doctor"));
                    //authorisingRoles.Add(new AuthorisingRoleModel(4, "Occupational Therapist"));
                    //authorisingRoles.Add(new AuthorisingRoleModel(5, "Opthalmologist"));
                    //authorisingRoles.Add(new AuthorisingRoleModel(6, "Podiatrist"));
                    //authorisingRoles.Add(new AuthorisingRoleModel(7, "Radiographer"));
                    //authorisingRoles.Add(new AuthorisingRoleModel(8, "Radiologist"));
                    //authorisingRoles.Add(new AuthorisingRoleModel(9, "Registered Nurse Practitioner"));
                    //authorisingRoles.Add(new AuthorisingRoleModel(10, "Speech and Language Therapist"));
                    //authorisingRoles.Add(new AuthorisingRoleModel(11, "Surgeon"));




                    dropdownLists.Add(authorisingRoles);
                }


                List<AppointmentTimeModel> appointmentTimes = new List<AppointmentTimeModel>();
                tableindex = GetTableIndex("tblAppointmentTime", ds);
                if (tableindex != -1)
                {
                    foreach (DataRow dr in ds.Tables[tableindex].Rows)
                    {
                        appointmentTimes.Add(new AppointmentTimeModel(Convert.ToInt16(dr["AppointmentTimeID"].ToString()), dr["Time"].ToString()));
                    }
                   // appointmentTimes.Add(new AppointmentTimeModel(5000, "Immediate Discharge"));
                    // appointmentTimes.Add(new AppointmentTimeModel(1, "10:00"));
                    //appointmentTimes.Add(new AppointmentTimeModel(2, "14:00"));
                    dropdownLists.Add(appointmentTimes);
                }
                List<EstimatedAppointmentDurationModel> estimatedAppointmentDuration = new List<EstimatedAppointmentDurationModel>();
                tableindex = GetTableIndex("AppointmentDuration", ds);
                if (tableindex != -1)
                {
                    foreach (DataRow dr in ds.Tables[tableindex].Rows)
                    {
                        estimatedAppointmentDuration.Add(new EstimatedAppointmentDurationModel(Convert.ToInt16(dr[0].ToString()), dr[1].ToString()));
                    }
                    //estimatedAppointmentDuration.Add(new EstimatedAppointmentDurationModel(1, "30 minutes"));
                    //estimatedAppointmentDuration.Add(new EstimatedAppointmentDurationModel(2, "60 minutes"));
                    //estimatedAppointmentDuration.Add(new EstimatedAppointmentDurationModel(3, "90 minutes"));
                    //estimatedAppointmentDuration.Add(new EstimatedAppointmentDurationModel(4, "2 Hours"));
                    //estimatedAppointmentDuration.Add(new EstimatedAppointmentDurationModel(5, "2 Hours"));
                    //estimatedAppointmentDuration.Add(new EstimatedAppointmentDurationModel(6, "3 Hours"));
                    //estimatedAppointmentDuration.Add(new EstimatedAppointmentDurationModel(7, "4 Hours"));
                    //estimatedAppointmentDuration.Add(new EstimatedAppointmentDurationModel(8, "5 Hours"));
                    //estimatedAppointmentDuration.Add(new EstimatedAppointmentDurationModel(9, "6 hours and over"));
                    //dropdownLists.Add(estimatedAppointmentDuration);
                    dropdownLists.Add(estimatedAppointmentDuration);
                }


                List<RelationshipToPatientModel> relationshipToPatient = new List<RelationshipToPatientModel>();

                tableindex = GetTableIndex("RelationShip", ds);
                if (tableindex != -1)
                {
                    foreach (DataRow dr in ds.Tables[tableindex].Rows)
                    {
                        relationshipToPatient.Add(new RelationshipToPatientModel(Convert.ToInt16(dr["RelationShipID"].ToString()), dr["RelationShip"].ToString()));
                    }
                    dropdownLists.Add(relationshipToPatient);
                }
                //relationshipToPatient.Add(new RelationshipToPatientModel(1, "Aunt"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(2, "Care agency"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(3, "Daughter"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(4, "Daughter-in-Law"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(5, "Father"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(6, "Father-in-Law"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(7, "Granddaughter"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(8, "Grandfather"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(9, "Grandmother"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(10, "Grandson"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(11, "Guardian"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(12, "Husband"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(13, "Mother"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(14, "Mother-in-Law"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(15, "Neighbour"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(16, "Nephew"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(17, "Niece"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(18, "Partner"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(19, "Son"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(20, "Son in Law"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(21, "Uncle"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(22, "Wife"));
                //relationshipToPatient.Add(new RelationshipToPatientModel(23, "Other"));
                //  dropdownLists.Add(relationshipToPatient);


                List<EscortTypeModel> escortTypes = new List<EscortTypeModel>();
                tableindex = GetTableIndex("Escort", ds);
                if (tableindex != -1)
                {
                    foreach (DataRow dr in ds.Tables[tableindex].Rows)
                    {
                        escortTypes.Add(new EscortTypeModel(Convert.ToInt16(dr[0].ToString()), dr[1].ToString()));
                    }
                    dropdownLists.Add(escortTypes);
                    //escortTypes.Add(new EscortTypeModel(1, "Carer"));
                    //escortTypes.Add(new EscortTypeModel(2, "Family Member"));
                    //escortTypes.Add(new EscortTypeModel(3, "Friend"));
                    //escortTypes.Add(new EscortTypeModel(4, "Guardian"));
                    //escortTypes.Add(new EscortTypeModel(5, "Nurse"));
                    //escortTypes.Add(new EscortTypeModel(5, "Other professional"));
                    //dropdownLists.Add(escortTypes);
                }


                List<EscortNumberModel> escortNumbers = new List<EscortNumberModel>();
                //tableindex = GetTableIndex("Escort", ds);

                //foreach (DataRow dr in ds.Tables[tableindex].Rows)
                //{
                //    escortNumbers.Add(new EscortNumberModel(Convert.ToInt16(dr[0].ToString()), dr[1].ToString()));
                //}
                //dropdownLists.Add(escortNumbers);
                escortNumbers.Add(new EscortNumberModel(1, "1"));
                escortNumbers.Add(new EscortNumberModel(2, "2"));
                escortNumbers.Add(new EscortNumberModel(3, "3"));
                escortNumbers.Add(new EscortNumberModel(4, "4"));
                dropdownLists.Add(escortNumbers);



                List<InfectiousModel> Infectious = new List<InfectiousModel>();
                tableindex = GetTableIndex("InfectionType", ds);
                if (tableindex != -1)
                {
                    foreach (DataRow dr in ds.Tables[tableindex].Rows)
                    {
                        Infectious.Add(new InfectiousModel(Convert.ToInt16(dr["InfectionTypeID"].ToString()), dr["InfectionType"].ToString()));
                    }
                    //Infectious.Add(new InfectiousModel(1, "HIV"));
                    //Infectious.Add(new InfectiousModel(2, "MRSA"));
                    //Infectious.Add(new InfectiousModel(3, "C-Dif"));
                    //Infectious.Add(new InfectiousModel(4, "Other (please note information in box above)"));
                    dropdownLists.Add(Infectious);
                }


                List<TransportRequestReasonModel> transportRequestReasons = new List<TransportRequestReasonModel>();
                tableindex = GetTableIndex("TransportReason", ds);
                if (tableindex != -1)
                {
                    foreach (DataRow dr in ds.Tables[tableindex].Rows)
                    {
                        transportRequestReasons.Add(new TransportRequestReasonModel(Convert.ToInt16(dr[0].ToString()), dr[1].ToString()));
                    }
                    dropdownLists.Add(transportRequestReasons);
                    //transportRequestReasons.Add(new TransportRequestReasonModel(1, "Admission"));
                    //transportRequestReasons.Add(new TransportRequestReasonModel(2, "Discharge"));
                    //transportRequestReasons.Add(new TransportRequestReasonModel(3, "Intermediate Care"));
                    //transportRequestReasons.Add(new TransportRequestReasonModel(4, "Out Patient"));
                    //// transportRequestReasons.Add(new TransportRequestReasonModel(5, "Specialist Transport"));
                    //transportRequestReasons.Add(new TransportRequestReasonModel(5, "Transfer"));
                    //dropdownLists.Add(transportRequestReasons);
                }


                List<TransportSelectionModel> transportSelections = new List<TransportSelectionModel>();
                tableindex = GetTableIndex("TransportRequirement", ds);
                if (tableindex != -1)
                {
                    foreach (DataRow dr in ds.Tables[tableindex].Rows)
                    {
                        transportSelections.Add(new TransportSelectionModel(Convert.ToInt16(dr[0].ToString()), dr[1].ToString()));
                    }
                    dropdownLists.Add(transportSelections);
                    //transportSelections.Add(new TransportSelectionModel(1, "C1A"));
                    //transportSelections.Add(new TransportSelectionModel(2, "C2"));
                    //transportSelections.Add(new TransportSelectionModel(3, "OWC"));
                    //transportSelections.Add(new TransportSelectionModel(4, "OBWC"));
                    //transportSelections.Add(new TransportSelectionModel(5, "AWC"));
                    //transportSelections.Add(new TransportSelectionModel(6, "ABWC"));
                    //transportSelections.Add(new TransportSelectionModel(7, "WC/STR"));
                    //transportSelections.Add(new TransportSelectionModel(8, "STR"));
                    //dropdownLists.Add(transportSelections);
                }


                List<CancelReasonModel> CancelReasons = new List<CancelReasonModel>();
                tableindex = GetTableIndex("CancelRejectReason", ds);
                if (tableindex != -1)
                {
                    foreach (DataRow dr in ds.Tables[tableindex].Rows)
                    {
                        if (dr["IsCancellationReason"].ToString().Trim() == "1")
                            CancelReasons.Add(new CancelReasonModel(Convert.ToInt16(dr[0].ToString()), dr[1].ToString()));
                    }
                    dropdownLists.Add(CancelReasons);
                    //CancelReasons.Add(new CancelReasonModel(1, "Reason1"));
                    //CancelReasons.Add(new CancelReasonModel(2, "Reason2"));
                    //CancelReasons.Add(new CancelReasonModel(3, "Reason3"));
                    //CancelReasons.Add(new CancelReasonModel(4, "Reason4"));
                    //CancelReasons.Add(new CancelReasonModel(5, "Reason5"));
                    //CancelReasons.Add(new CancelReasonModel(6, "Reason6"));
                    //CancelReasons.Add(new CancelReasonModel(7, "Reason7"));
                    //CancelReasons.Add(new CancelReasonModel(8, "Other"));
                    //dropdownLists.Add(CancelReasons);

                }

                return dropdownLists;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dynamic GetFacilityDepartments(int FacilityId)
        {
            DataSet ds = new DataSet();
            StaticCache cache = new StaticCache();
            List<DepartmentModel> ldm = new List<DepartmentModel>();
            ds = cache.GetDropDowns(false);
            int tableindex = GetTableIndex("tblHospitalDepartment", ds);
            if (tableindex == -1)
            {
                return false;

            }
            DataView dv = new DataView(ds.Tables[tableindex], "FacilityID=" + FacilityId, "FacilityDepartmentName", DataViewRowState.CurrentRows);
            if (dv.Count == 0)
            {
                return false;
            }
            for (int i = 0; i < dv.Count; i++)
            {

                ldm.Add(new DepartmentModel(Convert.ToInt32(dv[i][0].ToString()), dv[i][1].ToString().Trim()));
            }
            return ldm;
        }

        public dynamic GetFacilities(int FacilityTypeId)
        {
            DataSet ds = new DataSet();
            StaticCache cache = new StaticCache();
            List<FacilityModel> ldm = new List<FacilityModel>();
            ds = cache.GetDropDowns(false);
            int tableindex = GetTableIndex("Facility", ds);
            if (tableindex == -1)
            {
                return false;

            }
            DataView dv = new DataView(ds.Tables[tableindex], "FacilityTypeID=" + FacilityTypeId, "FacilityName", DataViewRowState.CurrentRows);
            if (dv.Count == 0)
            {
                return false;
            }
            for (int i = 0; i < dv.Count; i++)
            {

                ldm.Add(new FacilityModel(Convert.ToInt32(dv[i][0].ToString()), dv[i][1].ToString().Trim(), Convert.ToInt32(dv[i][2].ToString().Trim())));
            }
            return ldm;
        }

        public int GetTableIndex(string p, DataSet ds)
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
