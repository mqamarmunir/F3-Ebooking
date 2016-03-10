using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRules.TokenManager
{
    public class TokenSession
    {
        private Token m_Token;
        private DateTime m_InitialTime;
        private TimeSpan m_SessionTime;
        
        public TokenSession(Token token, TimeSpan sessionTime) {
            m_Token = token;
            m_SessionTime = sessionTime;
            m_InitialTime = DateTime.Now;
        }

        /// <summary>
        /// Key
        /// </summary>
        public string Key {
            get {
                return m_Token.Key;
            }
            set {
                m_Token.Key = value;
            }
        }

        /// <summary>
        /// Session Expired
        /// </summary>
        public bool SessionExpired{
            get {
                TimeSpan timespent = DateTime.Now.Subtract(m_InitialTime);
                return timespent.CompareTo(m_SessionTime) > 0;                                
            }
        }

        /// <summary>
        /// Token
        /// </summary>
        public Token Token {
            get {
                return m_Token;
            }
        }
    }
}
