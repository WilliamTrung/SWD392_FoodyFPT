using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helper
{
    public class Constants
    {
        private static Constants instance = new Constants();
        private Constants()
        {
        }
        public static Constants Instance { 
            get { 
                return instance;
            } 
        }

        public string GOOGLE_CLIENT_ID = "219129927259-v1pu3dn7kq20fkbpo951khil69phl1cr.apps.googleusercontent.com";
        public string GOOGLE_CLIENT_SECRET = "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";
    }
}
