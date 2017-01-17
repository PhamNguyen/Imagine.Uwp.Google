// ******************************************************************
// Copyright (c) 2017 by Nguyen Pham. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagine.Uwp.Google
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
