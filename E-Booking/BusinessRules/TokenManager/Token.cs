using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace BusinessRules.TokenManager
{

    public class Token
    {
        private string m_UserName;
        private string m_Password;
        private string m_MachineID;
        private string m_IP;
        private string m_Key;
        private string m_TokanXml;
        private SysCrypt m_SysCrypt;


        private Token()
        {
            m_SysCrypt = new SysCrypt();
        }

        public Token(string userName, string password, string machineID, string IP)
            : this()
        {
            // UserName
            this.m_UserName = userName;
            // Password
            this.m_Password = password;
            // Machine ID
            this.m_MachineID = machineID;
            // IP
            this.m_IP = IP;
            // key
            this.m_Key = Guid.NewGuid().ToString();
            // Token Xml
            this.tokenXml();

        }

        public Token(string token)
            : this()
        {
            XmlDataDocument xToken = new XmlDataDocument();
            xToken.InnerXml = m_SysCrypt.DecryptingString(token);
            try
            {
                XmlElement root = (XmlElement)xToken.SelectSingleNode("credential");
                // User Name
                m_UserName = root.SelectSingleNode("username").InnerText;
                // Password
                m_Password = root.SelectSingleNode("password").InnerText;
                // MachineID
                m_MachineID = root.SelectSingleNode("machineID").InnerText;
                // IP
                m_IP = root.SelectSingleNode("IP").InnerText;
                // Key
                m_Key = root.SelectSingleNode("key").InnerText;

            }
            catch
            {
                Exception tokenException = new Exception("Invalid Xml");
                throw tokenException;
            }

            // Token Xml
            this.tokenXml();
        }

        public Token(XmlDataDocument token)
            : this(token.InnerText)
        {

        }


        #region -- Token xml --
        /// <summary>
        /// Token Xml
        /// </summary>
        private void tokenXml()
        {
            //
            // Xml Data Document
            //
            XmlDataDocument xDoc;
            XmlElement root, element;
            xDoc = new XmlDataDocument();
            // Credentials
            root = xDoc.CreateElement("credential");
            xDoc.AppendChild(root);
            // User Name
            element = xDoc.CreateElement("username");
            element.InnerText = this.m_UserName;
            root.AppendChild(element);
            // Password
            element = xDoc.CreateElement("password");
            element.InnerText = this.m_Password;
            root.AppendChild(element);
            // Machine ID
            element = xDoc.CreateElement("machineID");
            element.InnerText = this.m_MachineID;
            root.AppendChild(element);
            // IP
            element = xDoc.CreateElement("IP");
            element.InnerText = this.m_IP;
            root.AppendChild(element);
            // Key
            element = xDoc.CreateElement("key");
            element.InnerText = this.m_Key;
            root.AppendChild(element);

            m_TokanXml = xDoc.InnerXml;

        }
        #endregion

        #region -- Properties --
        /// <summary>
        /// User Name
        /// </summary>
        public string UserName
        {
            get
            {
                return m_UserName;
            }
            set
            {
                m_UserName = value;
                // Token Xml
                this.tokenXml();
            }
        }

        /// <summary>
        /// Password
        /// </summary>
        public string Password
        {
            get
            {
                return m_Password;
            }
            set
            {
                m_Password = value;
                // Token Xml
                this.tokenXml();
            }

        }

        /// <summary>
        /// Machine ID
        /// </summary>
        public string MachineID
        {
            get
            {
                return m_MachineID;
            }
            set
            {
                m_MachineID = value;
                // Token Xml
                this.tokenXml();
            }
        }

        /// <summary>
        /// IP
        /// </summary>
        public string IPs
        {
            get
            {
                return m_IP;
            }
            set
            {

                m_IP = value;
                // Token Xml
                this.tokenXml();
            }
        }

        /// <summary>
        /// Key
        /// </summary>
        public string Key
        {
            get
            {
                return m_Key;
            }
            set
            {
                m_Key = value;
                // Token Xml
                this.tokenXml();
            }
        }
        #endregion

        #region -- ToString --
        /// <summary>
        /// ToString 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return m_TokanXml;
        }
        #endregion

        public static bool operator ==(Token token1, Token token2)
        {

            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(token1, token2))
            {
                return true;
            }
            // If one is null, but not both, return false.
            if (((object)token1 == null) || ((object)token2 == null))
            {
                return false;
            }

            // Return true if the fields match:
            return (token1.UserName == token2.UserName && token1.Password == token2.Password && token1.MachineID == token2.MachineID
                && token1.MachineID == token2.MachineID);

        }

        public static bool operator !=(Token token1, Token token2)
        {
            return !(token1 == token2);
        }

    }
}
