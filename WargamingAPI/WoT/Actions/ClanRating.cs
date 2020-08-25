using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WargamingAPI.WoT.Models.Clans;

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

        public List<Rating> GetRatingClans(string application_id, Clan clan)
        {
            List<Rating> ratingsOfClan = new List<Rating>();
            string finalUrlRequest = string.Concat(_clanRatingLink, typesInqury[0],
               "application_id=", application_id, "&clan_id=", clan.clan_id.ToString());

            string response = Request.GetResponse(finalUrlRequest);
            dynamic parsed = JsonConvert.DeserializeObject(response);
            string status = parsed.status;

            if(status == "ok")
            {
                GetRankFields(application_id);
                foreach(string rank_field in _rankFields)
                {
                    Rating clanRating = new Rating();

                    clanRating.rank = parsed["data"][clan.clan_id][rank_field]["rank"];
                    clanRating.rank_delta = parsed["data"][clan.clan_id][rank_field]["rank_delta"];
                    clanRating.value = parsed["data"][clan.clan_id][rank_field]["value"];
                    clanRating.name_rating = rank_field;

                    ratingsOfClan.Add(clanRating);
                }
            }

            return ratingsOfClan;
        }

        //Something strange in this function API. It returns nothing. 
        public void GetTopClans(string application_id, string rank_filed, int date)
        {
            List<Rating> ratingsOfClan = new List<Rating>();
            string finalUrlRequest = string.Concat(_clanRatingLink, typesInqury[0],
               "application_id=", application_id, "&rank_field=", rank_filed, "&date=", date);

            string response = Request.GetResponse(finalUrlRequest);
            dynamic parsed = JsonConvert.DeserializeObject(response);
            string status = parsed.status;

            if (status == "ok")
            {
                throw new NotImplementedException();
            }
        }
    }
}
