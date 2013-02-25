using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookApi
{
    public class FacebookConnection
    {
        public string Appid { get; set; }
        public string Appsecret { get; set; }
        public string Scope { get; set; }

        private string oauthToken;

        public string OAuthToken
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.oauthToken))
                {
                    throw new InvalidProgramException("Missing OAuthToken");
                }

                return this.oauthToken;
            }
            set
            {
                this.oauthToken = value;
            }
        }

        public FacebookConnection(string appId)
            :this(appId, "")
        {
        }

        public FacebookConnection(string appId, string appSecret)
            : this(appId, appSecret, "")
        {
        }

        public FacebookConnection(string appId, string appSecret, string scope)
        {
             this.Appid = Appid;
             this.Appsecret = appSecret;
             this.Scope = scope;
        }

        public string GetLoginUrl(string returnUrl)
        {
            return "https://www.facebook.com/dialog/oauth?client_id=" + this.Appid + "&redirect_uri=" + returnUrl + "&scope=" + this.Scope;
        }

        public FacebookSignedRequest DecodeSignedRequest(string signed_request)
        {
            string[] explode = signed_request.Split('.');

            string encoded_sig = explode[0].Replace('-', '+').Replace('_', '/').PadRight(explode[0].Length + (4 - explode[0].Length % 4) % 4, '=');
            string payload = explode[1].PadRight(explode[1].Replace('-', '+').Replace('_', '/').Length + (4 - explode[1].Length % 4) % 4, '=');

            string sig = Encoding.UTF8.GetString(Convert.FromBase64String(encoded_sig));
            Newtonsoft.Json.Linq.JObject data = Newtonsoft.Json.Linq.JObject.Parse(Encoding.UTF8.GetString(Convert.FromBase64String(payload)));

            if (data["algorithm"].ToString().ToUpper() != "HMAC-SHA256")
            {
               throw new InvalidOperationException();
            }

            System.Security.Cryptography.HMACSHA256 crypt = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(this.Appsecret));

            crypt.ComputeHash(Encoding.UTF8.GetBytes(payload));
            string expectedSig = Encoding.UTF8.GetString(crypt.Hash);

            if (sig != expectedSig)
            {
               throw new InvalidOperationException();
            }

            this.OAuthToken = data["oauth_token"].ToString();

            return new FacebookSignedRequest(data);
        }

        public FacebookDyamicTable Query(string query)
        {
            string url = "https://graph.facebook.com/fql?q=" + query + "&access_token=" + this.OAuthToken;

            string json = string.Empty;
            using (var webClient = new System.Net.WebClient())
            {

                json = webClient.DownloadString(url);
            }

            return new FacebookDyamicTable(json);
        }
    }
}
