using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WargamingAPI.WoT.Models;

namespace WargamingAPI.WoT.Actions
{
    public class Clans
    {
        public readonly string clanLink;
        public readonly List<string> typesInqury;

        public Clans()
        {
            clanLink = "https://api.worldoftanks.ru/wot/clans/";
            typesInqury = new List<string>() { "list/?", "info/?", "tanks/?", "achievements/?" };
        }

        public List<Clan> GetClans(string application_id, string search)
        {
            List<Clan> clans = new List<Clan>();

            string finalUrlRequest = string.Concat(clanLink, typesInqury[0],
                "application_id=", application_id, "&search=", search);

            string response = Request.GetResponse(finalUrlRequest);
            dynamic parsed = JsonConvert.DeserializeObject(response);
            string status = parsed.status;

            if(status == "ok")
            {
                int count = parsed.meta.count;
                for(int i = 0; i < count; i++)
                {
                    Clan clan = new Clan()
                    {
                        clan_id = parsed.data[i].clan_id,
                        name = parsed.data[i].name,
                        tag = parsed.data[i].tag,
                        members_count = parsed.data[i].members_count,
                        color = parsed.data[i].color,
                        created_at = parsed.data[i].created_at
                    };
                    clans.Add(clan);
                }
            }

            return clans;
        }
    }
}
