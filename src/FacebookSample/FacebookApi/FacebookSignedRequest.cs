using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookApi
{
    public class FacebookSignedRequest
    {
        private Newtonsoft.Json.Linq.JObject data;

        internal FacebookSignedRequest(Newtonsoft.Json.Linq.JObject data)
        {
            this.data = data;
        }

        public bool IsUserAuthenticated
        {
            get
            {
                return data["user_id"] == null;
            }
        }

        public DateTime GetExpireDateTime()
        {
            string expires = data["expires"].ToString();

            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(Convert.ToInt64(expires)).ToLocalTime();

            return dtDateTime;
        }

        public string GetOAuthToken()
        {
            return data["oauth_token"].ToString();
        }

        public string GetUserId()
        {
            return Convert.ToString(data["user_id"]);
        }
    }
}
