using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Configuration;

namespace BusinessRules.TokenManager
{
    public class TokenCashe
    {
        private Hashtable tokens;
        private static TokenCashe m_TokenCashe;
        private TimeSpan m_SessionTime;
        private System.Timers.Timer m_Timer;
        private Settings m_Settings;
        
        private TokenCashe() { 
            tokens = new Hashtable();
            m_Settings = new Settings();
            m_Timer = new System.Timers.Timer(m_Settings.GCIntervalinMilliseconds);
            m_Timer.Elapsed += new System.Timers.ElapsedEventHandler(m_Timer_Elapsed);
            m_SessionTime = new TimeSpan(0,m_Settings.SessionTimeinMinutes,0);            
        }

        
        #region -- Validate --
        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public TokenInfo Validate(Token token)
        {
            string key = token.Key;
            object obj = tokens[key];
            
            if (obj != null)
            {
                TokenSession tokenSession = (TokenSession)obj;
                if (tokenSession.Token != token) {
                    this.tokens.Remove(key);
                    return TokenInfo.NotValidated;
                }
                    
                if (tokenSession.SessionExpired)
                {
                    tokens.Remove(key);
                    return TokenInfo.Expired;
                }
                return TokenInfo.Validated;
            }

            return TokenInfo.NotValidated;
            
        }
        
        #endregion

        public void AddToken(Token token) {
            object obj = tokens[token.Key];
            if(obj == null)
                tokens.Add(token.Key, new TokenSession(token, m_SessionTime));
        }

       
        /// <summary>
        /// Token Cashe
        /// </summary>
        public static TokenCashe Current{
            get {
                if (m_TokenCashe == null) {
                    m_TokenCashe = new TokenCashe();
                }
                return m_TokenCashe;
            }            
        }

        void m_Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CollectGarbage();
        }

        /// <summary>
        /// Collect Garbage
        /// </summary>
        private void CollectGarbage() {       
            
            IDictionaryEnumerator iterator = tokens.GetEnumerator();
            do
            {
                TokenSession t = (TokenSession)iterator.Value;
                if (t.SessionExpired)
                {
                    tokens.Remove(iterator.Key);
                }
            } while (iterator.MoveNext());
            
        }

    }

    
}
