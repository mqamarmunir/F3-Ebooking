using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;

namespace BusinessRules
{
    public class AppCache
    {
        private ObjectCache cache = MemoryCache.Default;
      
        public AppCache()
        {
            string fileContents = cache["filecontents"] as string;
        }

        public void LoadAppCache()
        {
 
        }
    }
}
