using Newtonsoft.Json;
using System.Collections.Generic;

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
        //TO-DO: Add non required fields
        public string GetAccessToken(string application_id)
        {
            string token = "";
            string finalUrlRequest = string.Concat(authLink, typesInqury[0],
                "application_id=", application_id);

            string response = Request.GetResponse(finalUrlRequest);
            dynamic parsed = JsonConvert.DeserializeObject(response);
            string status = parsed.status;

            if (status == "ok")
            {
                token = parsed.access_token;
            }
            else
            {
                //TO-DO: Work with exceptions
            }

            return token;
        }

        public bool ProlongateToken(string application_id, string access_token)
        {
            string finalUrlRequest = string.Concat(authLink, typesInqury[1],
                "application_id=", application_id, "&access_token=", access_token);

            string response = Request.GetResponse(finalUrlRequest);
            dynamic parsed = JsonConvert.DeserializeObject(response);
            string status = parsed.status;

            if (status == "ok")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DestroyToken(string application_id, string access_token)
        {
            string finalUrlRequest = string.Concat(authLink, typesInqury[2],
                "application_id=", application_id, "&access_token=", access_token);

            Request.GetResponse(finalUrlRequest);
        }
    }
}