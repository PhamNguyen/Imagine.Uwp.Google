using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleServices
{
    public class Constants
    {
        /// <summary> 
        /// The google callback url. 
        /// </summary> 
#if !WINDOWS_PHONE_APP
        public const string GoogleCallbackUrl = "urn:ietf:wg:oauth:2.0:oob";
#else
        public const string GoogleCallbackUrl = "http://localhost";
#endif

        /// <summary> 
        /// The facebook app id. 
        /// </summary> 
        public const string FacebookAppId = "<app id>";

        /// <summary> 
        /// The google client identifier. 
        /// </summary> 
        public const string GoogleClientId = "<client id>";

        /// <summary> 
        /// The google client secret. 
        /// </summary> 
        public const string GoogleClientSecret = "<client secret";

        /// <summary> 
        /// The login token. 
        /// </summary> 
        public const string LoginToken = "LoginToken";

        /// <summary> 
        /// The facebook provider. 
        /// </summary> 
        public const string FacebookProvider = "facebook";

        /// <summary> 
        /// The google provider. 
        /// </summary> 
        public const string GoogleProvider = "google";

        /// <summary> 
        /// The microsoft provider. 
        /// </summary> 
        public const string MicrosoftProvider = "microsoft";
    }
}
