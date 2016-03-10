using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

//using RCOP.SystemFramework;
using BusinessRules.RoleBasedWS;
using System.Threading.Tasks;

namespace BusinessRules
{
    public class RoleBased
    {
        #region Declarations
        RoleBasedWS.RoleBased m_RoleBasedWs;
        //public event ValidateUserCompletedEventHandler OnValidateUser;
        //public event ValidateUserAndLogInvalidEntryCompletedEventHandler OnValidateUserAndLogInvalidEntry;
        //public event GetUserAccessSettingsCompletedEventHandler OnGetUserAccessSettings;
        #endregion

        public RoleBased()
        {
            m_RoleBasedWs = new RoleBasedWS.RoleBased();
            m_RoleBasedWs.Url = System.Web.Configuration.WebConfigurationManager.AppSettings["WSBaseURL"] + @"/RoleBased/RoleBased.asmx";
            
            //Credentials.Current.SetCredentials(m_RoleBasedWs);
            //m_RoleBasedWs.ValidateUserCompleted += new ValidateUserCompletedEventHandler(m_RoleBasedWs_ValidateUserCompleted);

            ////m_RoleBasedWs.ValidateUserAndLogInvalidEntryCompleted += new ValidateUserAndLogInvalidEntryCompletedEventHandler(m_RoleBasedWs_ValidateUserAndLogInvalidEntryCompleted);
            //m_RoleBasedWs.GetUserAccessSettingsCompleted += new GetUserAccessSettingsCompletedEventHandler(m_RoleBasedWs_GetUserAccessSettingsCompleted);
        }

       

        public String WsBaseUrl
        {
            set
            {
                m_RoleBasedWs.Url = value + @"/RoleBased/RoleBased.asmx";
            }
        }
      

        #region Functions
        public Boolean ValidateUserWeb(string userName, string password,out Int32 UserID, out Int32 RoleID)
        {
            try
            {
                return m_RoleBasedWs.ValidateWebUserEbooking(userName, password, out UserID, out RoleID);
            }
            catch (Exception ee)
            {
                UserID = 0;
                RoleID = 0;
                if (ee.ToString().Contains("Invalid User Code"))
                {
                    UserID = -1;
                    RoleID = -1;
                    
                }
              
                return false;
            }
            //  m_RoleBasedWs.ValidateUser(userName, password, ipAddress, machineSignature, AttemptsIn);
            //m_RoleBasedWs.val

        }
        
        public void GetUserAccessSettings(int userId)
        {
            m_RoleBasedWs.GetUserAccessSettingsAsync(userId);
        }


        public System.Data.DataSet ValidateUserSync(string userName, string password, string ipAddress, string machineSignature)
        {
            return m_RoleBasedWs.ValidateUser2(userName, password, ipAddress, machineSignature);
        }

        public DataSet SearchFailedLogin(string Signature, int? StationID, string IP, DateTime? FromDate, DateTime? ToDate, int PageIndex, int @pPageSize, int @pSortColumnIndex, string @pSortColumn)
        {
            return m_RoleBasedWs.SearchFailedLogin(Signature, StationID, IP, FromDate, ToDate, PageIndex, @pPageSize, @pSortColumnIndex, @pSortColumn);
        }


        public DataSet SearchInvalidMachines(int? machineTypeID, DateTime? FromDate, DateTime? ToDate)
        {
            return m_RoleBasedWs.SearchInvalidMachines(machineTypeID, FromDate, ToDate);

        }
        
        public void RegisterInvalidMachines(int InvalidMachineID, int userID)
        {
            m_RoleBasedWs.RegisterInvalidMachines(InvalidMachineID, userID);
        }
        #endregion
    }
}
