using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Actions
{
    public class ClanRating
    {
        List<string> _rankFields;
        List<int> _avaliableDates;
        public readonly string _clanRatingLink;
        public readonly List<string> typesInqury;

        public ClanRating()
        {
            _rankFields = new List<string>();
            _avaliableDates = new List<int>();
            _clanRatingLink = "https://api.worldoftanks.ru/wot/clanratings/";
            typesInqury = new List<string>() { "types/?", "dates/?", "clans/?", "neighbors/?", "top/?" };
        }

        public void GetRankFields(string application_id)
        {
            string finalUrlRequest = string.Concat(_clanRatingLink, typesInqury[0],
                "application_id=", application_id);

            string response = Request.GetResponse(finalUrlRequest);
            dynamic parsed = JsonConvert.DeserializeObject(response);
            string status = parsed.status;

            if(status == "ok")
            {
                _rankFields = (List<string>)parsed["data"]["all"];
            }
            else
            {
                //TO-DO: Exception to face
            }
        }

        public void GetAvailabeDate(string application_id, int? limit)
        {
            string finalUrlRequest = string.Concat(_clanRatingLink, typesInqury[0],
                "application_id=", application_id);
            if(limit != null)
            {
                finalUrlRequest = string.Concat(finalUrlRequest, "&limit=", limit.ToString());
            }

            string response = Request.GetResponse(finalUrlRequest);
            dynamic parsed = JsonConvert.DeserializeObject(response);
            string status = parsed.status;

            if (status == "ok")
            {
                _avaliableDates = (List<int>)parsed["data"]["all"];
            }
            else
            {
                //TO-DO: Exception to face
            }
        }
    }
}
