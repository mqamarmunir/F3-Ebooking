using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace BusinessRules.TokenManager
{
    public class Settings
    {
        private int m_SessionTimeMinutes;
        private int m_GarbageCollectionInterval;
        
        public Settings() {
            m_SessionTimeMinutes = int.Parse(ConfigurationSettings.AppSettings["SessionTime"]);
            m_GarbageCollectionInterval = int.Parse(ConfigurationSettings.AppSettings["GarbageCollectionTime"]);
        }

        /// <summary>
        /// Session Time in Minutes
        /// </summary>
        public int SessionTimeinMinutes {
            get {
                return m_SessionTimeMinutes;
            }
        }

        public Double GCIntervalinMilliseconds {
            get {
                return m_GarbageCollectionInterval * 3600000;
            }
        }
    }
}
