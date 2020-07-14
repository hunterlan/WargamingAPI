using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Actions
{
    public class Auth
    {
        public readonly string authLink;
        public readonly List<string> typesInqury;

        public Auth()
        {
            authLink = "https://api.worldoftanks.ru/wot/auth";
            typesInqury = new List<string>() { "login/?", "prolongate/?", "logout/?" };
        }
        //Token expires at 2 weeks
        public string GetAccessToken(string application_id)
        {
            string token = "";
            string finalUrlRequest = string.Concat(authLink, typesInqury[0],
                "application_id=", application_id);

            string response = Request.GetResponse(finalUrlRequest);
            dynamic parsed = JsonConvert.DeserializeObject(response);
            string status = parsed.status;

            if(status == "ok")
            {
                token = parsed.access_token;
            }

            return token;
        }
    }
}
