using GoogleServices.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace GoogleServices
{
    public class GoogleApis
    {
        public enum GoogleApiResult
        {
            ExceptionError,
            LackOfInfo,
            AuthorizeFailed,
            AuthorizeSuccess,
            GetUserInfoFailed,
            GetUserInfoSuccess,
            NotAuthorize,
        }

        #region Singleton
        private static volatile GoogleApis instance;
        private static object syncRoot = new Object();

        public static GoogleApis Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GoogleApis();
                    }
                }
                return instance;
            }
        }
        #endregion

        #region Properties
        public string ClientId { get; set; }

        public string SerectKey { get; set; }

        public Session CurrentSession { get; set; }

        public Profile UserProfile { get; set; }

        public bool IsAuthorized
        {
            get
            {
                return (CurrentSession != null && !String.IsNullOrEmpty(CurrentSession.AccessToken));
            }
        }
        #endregion

        public GoogleApis()
        {
            Logout();
        }

        public void SetGoogleAppInfo(string clientId, string serectKey)
        {
            ClientId = clientId;
            SerectKey = serectKey;
        }

        public void Logout()
        {
            ClientId = string.Empty;
            SerectKey = string.Empty;
            CurrentSession = null;
            UserProfile = null;
        }

        #region Authorize Methods
        public async Task<GoogleApiResult> LoginGoogle()
        {
            if (String.IsNullOrEmpty(ClientId) || String.IsNullOrEmpty(SerectKey))
                return GoogleApiResult.LackOfInfo;

            if (CurrentSession != null)
                return GoogleApiResult.AuthorizeSuccess;

            var googleUrl = new StringBuilder();
            googleUrl.Append("https://accounts.google.com/o/oauth2/auth?client_id=");
            googleUrl.Append(Uri.EscapeDataString(ClientId));
            googleUrl.Append("&scope=");
            googleUrl.Append(GoogleScopes.UserinfoEmail);
            googleUrl.Append("&redirect_uri=");
            googleUrl.Append(Uri.EscapeDataString(Constants.GoogleCallbackUrl));
            googleUrl.Append("&state=foobar");
            googleUrl.Append("&response_type=code");

            string endURL = "https://accounts.google.com/o/oauth2/approval?";

            Uri startUri = new Uri(googleUrl.ToString());
            Uri endUri = new Uri(endURL);

            try
            {
                var webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.UseTitle, startUri, endUri);

                var session = await GetSession(webAuthenticationResult);

                if (session != null && !String.IsNullOrEmpty(session.AccessToken))
                {
                    CurrentSession = session;
                    return GoogleApiResult.AuthorizeSuccess;
                }
                else
                {
                    return GoogleApiResult.AuthorizeFailed;
                }
            }
            catch (Exception)
            {
                return GoogleApiResult.ExceptionError;
            }
        }

        private async Task<Session> GetSession(WebAuthenticationResult result)
        {
            if (result.ResponseStatus == WebAuthenticationStatus.Success)
            {
                var code = GetCode(result.ResponseData);
                var serviceRequest = await GetToken(code);

                return new Session
                {
                    AccessToken = serviceRequest.AccessToken,
                    ExpireIn = new TimeSpan(0, 0, int.Parse(serviceRequest.ExpiresIn)),
                    Id = serviceRequest.TokenId,
                    Provider = Constants.GoogleProvider
                };
            }
            if (result.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
            {
                throw new Exception("Error http");
            }
            if (result.ResponseStatus == WebAuthenticationStatus.UserCancel)
            {
                throw new Exception("User Canceled.");
            }
            return null;
        }

        private async Task<ServiceResponse> GetToken(string code)
        {
            const string TokenUrl = "https://accounts.google.com/o/oauth2/token";

            var body = new StringBuilder();
            body.Append(code);
            body.Append("&client_id=");
            body.Append(Uri.EscapeDataString(ClientId));
            body.Append("&client_secret=");
            body.Append(Uri.EscapeDataString(SerectKey));
            body.Append("&redirect_uri=");
            body.Append(Uri.EscapeDataString(Constants.GoogleCallbackUrl));
            body.Append("&grant_type=authorization_code");

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(TokenUrl))
            {
                Content = new StringContent(body.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded")
            };
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            var serviceTequest = JsonConvert.DeserializeObject<ServiceResponse>(content);
            return serviceTequest;
        }

        private string GetCode(string webAuthResultResponseData)
        {
            var split = webAuthResultResponseData.Split('&');

            return split.FirstOrDefault(value => value.Contains("code"));
        }
        #endregion

        #region Get User Profile Methods
        public async Task<GoogleApiResult> LoadUserProfile()
        {
            if (!IsAuthorized)
            {
                return GoogleApiResult.NotAuthorize;
            }
            else
            {
                var httpClient = new HttpClient();

                var url = String.Format("https://www.googleapis.com/oauth2/v1/userinfo?access_token={0}", CurrentSession.AccessToken);

                string profileResultString = await httpClient.GetStringAsync(url);

                if (!String.IsNullOrEmpty(profileResultString))
                {
                    UserProfile = JsonConvert.DeserializeObject<Profile>(profileResultString);
                    return GoogleApiResult.GetUserInfoSuccess;
                }
                return GoogleApiResult.GetUserInfoFailed;
            }
        }
        #endregion
    }
}


