using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;

namespace BusinessRules
{
    public class Credentials
    {
        #region Unused Data Members
        // private static bool mblnIsTokenSet = false;
        //private string m_Description;
        //private static string mstrStackTrace = null;
        //private string m_RegisterationDate;
        //private string m_Roles;
        #endregion 
        private string m_UserName;
        private string m_Password;
        private int m_Userid;
        private bool m_IsAdministrator;
        private int m_LogID;
        private int m_MachineID;
        private string m_MachineName;
        private string m_MachineSignature;
        
        private string m_Status;
        private string m_LogAtServer;
        private string m_LogAtClient;
        
        private System.Drawing.Image m_Image;
        
        private int m_PortalID;
        private string m_IP;
        private TokenManager.Token m_Token;
        private localhost.Service service;
        private static Credentials current;// = null;
      
        private SysCrypt m_SysCrypt;

    
      

        public static Credentials Current
        {
            get
            {
                if (current == null)
                {
                    current = new Credentials();
                   // mstrStackTrace = Environment.StackTrace;
                  //  mblnIsTokenSet = false;
                }
                return current;
            }
        }

        public Int32 UserLoggingId
        {
            get
            {
                return m_LogID;
            }
        }

        #region -- Credentials --
        /// <summary>
        /// Credentials
        /// 
        /// </summary>
        public Credentials()
        {
            
            m_UserName = string.Empty;
            m_Password = string.Empty;
            m_Userid = -1;
            //m_IsAdministrator = false;
          //  m_LogID = -1;
          //  m_MachineID = -1;

          //  m_MachineName = string.Empty;
          //  m_MachineSignature = string.Empty;
          //  //m_RegisterationDate = string.Empty;
          //  m_Status = string.Empty;
          //  m_LogAtServer = string.Empty;
          //  m_LogAtClient = string.Empty;
          //  //m_Description = string.Empty;
          ////  m_Roles = string.Empty;
          //  m_PortalID = -1;
          //  m_IP = string.Empty;
            m_SysCrypt = new SysCrypt();
           
            this.service = new BusinessRules.localhost.Service();
            //this.service.Url =  dsSettings.Obj.ClientAppSettings[0].WSBaseURL + "/AuthenticationServices/wsAuthentication.asmx";
            this.service.Url = System.Web.Configuration.WebConfigurationManager.AppSettings["WSBaseURL"] + "/AuthenticationServices/wsAuthentication.asmx";
            //this.service.UserLoginLoggingCompleted += new BusinessRules.localhost.UserLoginLoggingCompletedEventHandler(service_UserLoginLoggingCompleted);

            

            //if(String.IsNullOrEmpty(dsSettings.Obj.ClientAppSettings[0].SystemFingerPrint))

            //Concat Application Type With Machine Finger Prints
         //   m_MachineSignature = ClientFingerPrint.GetSystemFingerPrint() + BusinessRules.clsGeneral.GetApplicationPrefix();
            ////else
            // m_MachineSignature = dsSettings.Obj.ClientAppSettings[0].SystemFingerPrint;

            setMachineInformation();

        }

       
        #endregion
       
        void service_UserLoginLoggingCompleted(object sender, BusinessRules.localhost.UserLoginLoggingCompletedEventArgs e)
        {
            m_LogID = e.Result;
        }

        //public static string getCurrentVersion() {
        //    XmlDocument xdocClientConfig = new XmlDocument();
        //    string path = System.Windows.Forms.Application.StartupPath + @"\ClientConfig.xml";
        //    if (!System.IO.File.Exists(path))
        //        return "unknown";

        //    xdocClientConfig.Load(path);
        //    return xdocClientConfig.SelectSingleNode("/configuration/appStart/ClientApplicationInfo/installedVersion").InnerText;
        
        //}
        
        #region -- Set Machine Information --
        /// <summary>
        /// Set Machine Information
        /// </summary>
        private void setMachineInformation()
        {
            try
            {
                DataSet ds = service.GetMachineBySignature(m_MachineSignature);
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count >0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    m_MachineID = int.Parse(dr["MachineID"].ToString());
                    m_MachineName = dr["MachineName"].ToString();
                    ///m_RegisterationDate = dr["RegistrationDate"].ToString();
                    m_Status = dr["Status"].ToString();
                    if (m_Status == "False")
                    {
                        throw new Exception("This machine has been blocked");
                    }
                    m_LogAtServer = dr["AtServerLogLevel"].ToString();
                    m_LogAtClient = dr["AtClientLogLevel"].ToString();
                  //  m_Description = dr["Description"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        #endregion

        #region -- Log in --
        /// <summary>
        /// Log in
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool LogIn(int userID, string userName, string password, System.Drawing.Image userImage, string userRoles, int PortalID, string ip, bool IsAdministrator)
        {
            m_Userid = userID;
            m_UserName = userName;
            m_Password = password;
            m_Image = userImage;
            //m_Roles = userRoles;
            m_PortalID = PortalID;
            m_IsAdministrator = IsAdministrator;

            if (service == null)
            {
                this.service = new BusinessRules.localhost.Service();
                this.service.Url = System.Web.Configuration.WebConfigurationManager.AppSettings["WSBaseURL"] + "/AuthenticationServices/wsAuthentication.asmx";
                this.service.UserLoginLoggingCompleted += new BusinessRules.localhost.UserLoginLoggingCompletedEventHandler(service_UserLoginLoggingCompleted);
            }
            m_LogID = service.UserLoginLogging(DateTime.Now.ToString("s"), m_MachineID, m_PortalID);
            m_Token = new TokenManager.Token(m_UserName, m_Password, m_MachineID.ToString(), ip);
            m_IP = ip;

            // Temp code by Shahid
           // mblnIsTokenSet = true;

            return true;
        }
        #endregion

        #region -- Log off --
        public bool LogOff()
        {
            service.UserLogofLoggingAsync(m_LogID.ToString().ToString(), DateTime.Now.ToString());
            return true;
        }
        #endregion

        #region -- Properties --

        #region -- User Name --
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName
        {
            get
            {
                return m_UserName;
            }
        }
        #endregion

        #region -- Password --
        /// <summary>
        /// Password
        /// </summary>
        public string Password
        {
            get
            {
                return m_Password;
            }
        }
        #endregion

        #region -- User ID --
        /// <summary>
        /// UserID
        /// </summary>
        public int UserID
        {
            get
            {
                return m_Userid;
            }
        }


        #endregion

        #region -- Is Administrator --
        /// <summary>
        /// Returns whether user is Administrator or not.
        /// </summary>
        public bool IsAdministrator
        {
            get { return m_IsAdministrator; }
            set { m_IsAdministrator = value; }
        }


        #endregion

        #region -- User Image --
        /// <summary>
        /// User Image
        /// </summary>
        public System.Drawing.Image UserImage
        {
            get { return m_Image; }
        } 
        #endregion

        #region -- Machine ID --
        /// <summary>
        /// Machine ID
        /// </summary>
        public int MachineID
        {
            get
            {
                return m_MachineID;
            }
        }
        #endregion

        #region -- Log at Client --
        /// <summary>
        /// Log at client
        /// </summary>
        public string LogAtClient
        {
            get
            {
                return m_LogAtClient;
            }
        }
        #endregion

        #region -- Log at Server --
        /// <summary>
        /// Log At Server
        /// </summary>
        public string LogAtServer
        {
            get
            {
                return m_LogAtServer;
            }
        }
        #endregion

        #region -- Machine Name --
        /// <summary>
        /// Machine Name
        /// </summary>
        public string MachineName
        {
            get
            {
                return m_MachineName;
            }
        }
        #endregion

        #region -- Portal ID --
        public int PortalId
        {
            get { return m_PortalID; }
        }
        #endregion
        
        #region -- Token Xml --
        /// <summary>
        /// Token Xml
        /// </summary>
        public string TokenXml
        {
            get
            {
                //if (clsGeneral.IsInDesignMode)
                //    return string.Empty;
                if (m_Token == null)
                    m_Token = new TokenManager.Token(m_UserName, m_Password, m_MachineID.ToString(), m_IP);

                if (m_SysCrypt == null)
                    m_SysCrypt = new SysCrypt();

                return m_SysCrypt.EncryptingString(m_Token.ToString());
            }
        } 
        #endregion

        #region -- IP --
        /// <summary>
        /// IP
        /// </summary>
        public string IP
        {
            get
            {
                return this.m_IP;
            }

        } 
        #endregion

        //public string Version {
        //    get {
        //        return m_Version;
        //    }
        //}

        #endregion

    }
}