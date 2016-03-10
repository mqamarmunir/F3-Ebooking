using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
//using Common;
using BusinessRules.wsCall;
//using MultiLingual.Translator;
using System.IO;
//using BusinessRule.Push_Client;
//using RTCTypes.CommonTypes;
using System.Threading.Tasks;
using System.Data;

//System.Windows.Forms.CheckedListBox

namespace BusinessRules
{
    public enum eTripEventType
    {
        Single = 1,
        Return = 2,
        Inbound = 5,
        Outbound = 6,
        Patient_Assistance = 12,
        Asset_Transfer = 13
    }

    public enum ePaymentType
    {
        Cash = 1,
        Cheque = 2,
        Hospital = 3
    }


    //Test Comments
    //Comments by Cheran
    /// Put your comments here
    public class clsCall
    {
        #region -- Enum --
        /// <summary>
        /// Methods of clsBank.
        /// </summary>
        public enum eclsCallMethod
        {
            Save_Int,
            Delete_Bool,
            GetRecords_List
        }
        public enum eclsVehicleStatus
        {
            Available = 1,
            Unavailable = 2
        }
        public enum eclsFinishForm
        {
            Suspend = 1,
            Finish = 2,
            Available = 3
        }

        public enum eCallSource : byte
        {
            Phone = 0x00,
            Fax = 0x01,
            Email = 0x02,
            Web = 0x03
        }

        public enum eCallType : byte
        {
            Patienttransfer = 0x1,
            MedicalCover = 0x2,
            Repatriation = 0x3,
            Emergency = 0x4,
            Assistance = 0x05,
            AssetTranfser = 0x06,
            MCI = 0x07,
            CarService = 0x08
        }
        public enum eSMSMessageFor
        {
            CaseDispatch = 2,
            Break = 3,
            FirstResponder = 4,
            Employee = 5,
            InstantMessage = 6,
            FlagGazatteer = 7,
            PerformanceDashboardAlert = 8,
            LogisticDeskFleet = 9,
            LogisticDeskResource = 10,
            LogisticDeskTask = 11,
            SurveillanceAssistanceAlert = 12,
            HospitalAssign = 13,
            CancelDispatch = 14,
            CaseDetailDispatch = 15
        }

        public enum eCallFor
        {
            NewDispatch,
            UpdateDispatch
        }
        public enum eUnitScheduleFor
        {
            TripEventID = 1,
            VehicleRequestedID = 2
        }

        public enum eAdastraDecesion
        {
            None = 0,
            SendToAdastra = 1,
            SendToDispather = 2,
            Finished = 3,
            SendToClinicial = 4,
            ReciveFromAdastra = 5
        }

        public enum eMachineType
        {
            Dispatcher = 1,
            CallTaker = 2,
            None = 0
        }
        public enum eStatusColor
        {
            Schedule,
            Accept,
            Enroute,
            Onscene,
            Pickup,
            AtDestination,
            Returning,
            Finished,
            Canceled,
            Suspand,
            HandOver,
            RedBackup,
            ColdBackup,
            Carry
        }
        public enum eVisableColumn
        {
            CaseID,
            Deleted,
            Locked,
            ReferenceNo,
            Status,
            CallType
        }

        public enum eCaseStatus
        {
            CaseSubmitted = 1,
            AddressConfirmed = 29,
            ViewedByDispatcher = 2,
            ApprovedByDispatcher = 3,
            RejectedByDispatcher = 4,
            CancelledByCustomer = 5,
            ScheduledByDispatcher = 6,
            TransferredToAmbulance = 7,
            CancelledByDispatcher = 15,
            Accepted = 8,
            Enroute = 9,
            OnScene = 10,
            Pickup = 11,
            Transporting = 16,
            AtDestination = 12,
            Suspended = 13,
            Returning = 17,
            Finished = 14,
            BackToStation = 23,
            BackAtStation = 24,
            StandBy = 26,
            ColdBackup = 27,
            ReceivedonVehicle = 30,
            RedBackup = 35,
            Ambulancerequired = 34,
            TreatedatScene = 33,
            HandOver = 36,
            None = 0
        }
        public enum eBillingType
        {
            All = 0,
            Normal = 1,
            NoCharge = 2,
            Internal = 3
        }
        public enum ePushPinType
        {
            Vehicle = 0,
            Responder = 1,
            Hospital = 2,
            Incident = 3,
            Station = 4,
            ActiveCall = 5,
            General = 6,
            PickImage = 7,
            DropImage
        }

        public enum ePlanTransferCaseType
        {
            All = 0,
            Inhouse = 1,
            Outsource = 2
        }

        public enum ePlanTransferEmergencyType
        {
            All = 0,
            Normal = 1,
            Emergency = 2
        }

        public enum eCSDCaseFromePCR
        {
            ePCRService = 1,
            Dispatcher = 2
        }

        public enum eActionAgaintePCRCSDCase
        {
            None = 0,
            CSD = 1,
            Close = 2,
            Ignore = 3

        }
        public enum eBreakType
        {
            TeaBreak = 1,
            LunchBreak = 2,
            ShortBreak = 3,
            None = 4
        }
        public enum eBreakStatus
        {
            ValidAndForwarded = 1,
            ValidNotForwarded = 2,
            Invalid = 3,
            Finished = 4,
            Approved = 5,
            Cancelled = 6,
            NoReply = 7
        }
        public enum eNotesType
        {
            Call = 1,
            Status = 2,
            Finished = 3,
            Cancelled = 4,
            Patient = 5,
            Address = 6,
            Police = 7,
            Firebrigade = 8,
            OtherAgency = 9,
            PoliceContacted = 10,
            FireContacted = 11,
            CoastguardContacted = 12,
            SupervisorContacted = 13,
            DutyOfficerContacted = 14,
            GPSurgeryContacted = 15,
            AirAmbulanceContacted = 16,
            AEContacted = 17,
            CallLinked = 18,
            View = 19,
            AMPDSCodeChanged = 20
        }
        public enum eRTCDispatcherAlerts
        {
            InstantMessage = 1,
            RoadWork = 2,
            VehicleStatus = 3,
            VehicleRequired = 4,
            OutOfOrder = 5,
            DestinationChange = 6,
            AssistanceRequired = 7,
            StationAlert = 8,
            PaniconBoard = 9,
            InjuryonBoard = 10,
            Casualities = 11,
            VehicleDeploying = 12,
            CallTakerCases = 13,
            Crewbreak = 14,
            CrewEmegencyRequest = 15,
            DispatchCase = 16,
            EmergencyCallReceived = 17,
            EmergencycallSaved = 18,
            TriageChange = 19,
            NewSuitableVehicle = 19,
            FreeVehicle = 20,
            EmergencyPending = 21,
            AmbulanceConnectedRTC = 22,
            CrewloginonVehicle = 23,
            CrewlogoffonVehicle = 24,
            VehicleProperlyShotdown = 25,
            VehicleAbnormallyShutdown = 26,
            CaseReceivedOnVehicle = 27,
            CaseAccepetdOnVehicle = 28,
            AmbulanceDisconnectedRTC = 29,
            IntimationFromDispatcherToDispatcher = 30,
            UnitNotResponding = 31,
            ePCRCSDNotification = 32,
            None
        }
        #endregion

        



        #region -- Private Fields --


        private wsCall.wsCall mobjWSCall = null;



        #endregion

        public clsCall()
        {
            if(mobjWSCall==null)
            {
                mobjWSCall=new wsCall.wsCall();
            }
            mobjWSCall.Url = System.Web.Configuration.WebConfigurationManager.AppSettings["WSBaseURL"] + "/CADServices/wsCall.asmx";
        }

        public DataSet LoadCase(int CaseID)
        {
            return mobjWSCall.LoadCase(CaseID);
        }





    }




}
