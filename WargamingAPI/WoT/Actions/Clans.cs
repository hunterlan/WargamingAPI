using Newtonsoft.Json;
using System.Collections.Generic;
using WargamingAPI.WoT.Models.Clans;

namespace WargamingAPI.WoT.Actions
{
    public class Clans
    {
        public readonly string clanLink;
        public readonly List<string> typesInqury;

        public Clans()
        {
            clanLink = "https://api.worldoftanks.ru/wot/clans/";
            typesInqury = new List<string>() { "list/?", "info/?" };
        }

        public List<Clan> GetClans(string application_id, string search)
        {
            List<Clan> clans = new List<Clan>();

            string finalUrlRequest = string.Concat(clanLink, typesInqury[0],
                "application_id=", application_id, "&search=", search);

            string response = Request.GetResponse(finalUrlRequest);
            dynamic parsed = JsonConvert.DeserializeObject(response);
            string status = parsed.status;

            if (status == "ok")
            {
                int count = parsed.meta.count;
                for (int i = 0; i < count; i++)
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

        public ClanInfo GetClanInfo(string application_id, Clan clan)
        {
            ClanInfo info = new ClanInfo();

            string finalUrlRequest = string.Concat(clanLink, typesInqury[1],
               "application_id=", application_id, "&clan_id=", clan.clan_id);

            string response = Request.GetResponse(finalUrlRequest);
            dynamic parsed = JsonConvert.DeserializeObject(response);
            string status = parsed.status;

            if (status == "ok")
            {
                string strClanId = clan.clan_id.ToString();

                info.leader_id = (int)parsed["data"][strClanId]["leader_id"];
                info.leader_name = parsed["data"][strClanId]["leader_name"];
                info.color = parsed["data"][strClanId]["color"];
                info.updated_at = Request.ConvertFromTimestamp((int)parsed["data"][strClanId]["updated_at"]);
                info.tag = parsed["data"][strClanId]["tag"];
                info.name = parsed["data"][strClanId]["name"];
                info.motto = parsed["data"][strClanId]["motto"];
                info.creator_name = parsed["data"][strClanId]["creator_name"];
                info.creator_id = info.leader_id;
                info.created_at = Request.ConvertFromTimestamp((int)parsed["data"][strClanId]["created_at"]);
                info.members_count = (int)parsed["data"][strClanId]["members_count"];
                info.description_html = parsed["data"][strClanId]["description_html"];
                info.description = parsed["data"][strClanId]["description"];
                info.accepts_join_requests = (bool)parsed["data"][strClanId]["accepts_join_requests"];
                info.renamed_at = Request.ConvertFromTimestamp((int)parsed["data"][strClanId]["renamed_at"]);
                info.old_tag = parsed["data"][strClanId]["old_tag"];
                info.old_name = parsed["data"][strClanId]["old_name"];
                info.is_clan_disbanded = (bool)parsed["data"][strClanId]["is_clan_disbanded"];

                List<Member> membersClan = new List<Member>();
                for (int i = 0; i < info.members_count; i++)
                {
                    Member member = new Member()
                    {
                        role = parsed["data"][strClanId]["leader_id"]["members"][i]["role"],
                        role_i18n = parsed["data"][strClanId]["leader_id"]["members"][i]["role_i18n"],
                        joined_at =
                        Request.ConvertFromTimestamp
                        ((int)parsed["data"][strClanId]["leader_id"]["members"][i]["joined_at"]),
                        account_id = (int)parsed["data"][strClanId]["leader_id"]["members"][i]["account_id"],
                        account_name = parsed["data"][strClanId]["leader_id"]["members"][i]["account_name"]
                    };

                    membersClan.Add(member);
                }

                info.members = membersClan;
            }

            return info;
        }
    }
}