using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WargamingAPI.WoT.Exceptions;
using WargamingAPI.WoT.Models.Clans;

namespace WargamingAPI.WoT.Actions
{
    public class InfoClans
    {
        public readonly string clanLink;
        public readonly List<string> typesInqury;

        public InfoClans()
        {
            clanLink = "https://api.worldoftanks.ru/wot/clans/";
            typesInqury = new List<string>() { "list/?", "info/?" };
        }

        public List<Clan> GetClans(string application_id, string search)
        {
            List<Clan> clans = new();

            string finalUrlRequest = string.Concat(clanLink, typesInqury[0],
                "application_id=", application_id, "&search=", search);

            string response = Utils.GetResponse(finalUrlRequest);
            dynamic parsed = JsonConvert.DeserializeObject(response);
            string status = parsed.status;

            if (status == "ok")
            {
                int count = parsed.meta.count;
                for (int i = 0; i < count; i++)
                {
                    Clan clan = new()
                    {
                        ClanId = parsed.data[i].clan_id,
                        Name = parsed.data[i].name,
                        Tag = parsed.data[i].tag,
                        MembersCount = parsed.data[i].members_count,
                        Color = parsed.data[i].color,
                        CreatedAt = parsed.data[i].created_at
                    };
                    clans.Add(clan);
                }
            }
            else
            {
                new CauseException().Cause(parsed.error.message);
            }

            return clans;
        }

        public ClanInfo GetClanInfo(string application_id, Clan clan)
        {
            ClanInfo info = new();

            string finalUrlRequest = string.Concat(clanLink, typesInqury[1],
               "application_id=", application_id, "&clan_id=", clan.ClanId);

            string response = Utils.GetResponse(finalUrlRequest);
            dynamic parsed = JsonConvert.DeserializeObject(response);
            string status = parsed.status;

            if (status == "ok")
            {
                string strClanId = clan.ClanId.ToString();

                info.LeaderId = (int)parsed["data"][strClanId]["leader_id"];
                info.LeaderName = parsed["data"][strClanId]["leader_name"];
                info.Color = parsed["data"][strClanId]["color"];
                info.UpdatedAt = Utils.ConvertFromTimestamp((int)parsed["data"][strClanId]["updated_at"]);
                info.Tag = parsed["data"][strClanId]["tag"];
                info.Name = parsed["data"][strClanId]["name"];
                info.Motto = parsed["data"][strClanId]["motto"];
                info.CreatorName = parsed["data"][strClanId]["creator_name"];
                info.CreatorId = info.LeaderId;
                info.CreatedAt = Utils.ConvertFromTimestamp((int)parsed["data"][strClanId]["created_at"]);
                info.MembersCount = (int)parsed["data"][strClanId]["members_count"];
                info.DescriptionHtml = parsed["data"][strClanId]["description_html"];
                info.Description = parsed["data"][strClanId]["description"];
                info.AcceptsJoinRequests = (bool)parsed["data"][strClanId]["accepts_join_requests"];
                info.RenamedAt = Utils.ConvertFromTimestamp((int)parsed["data"][strClanId]["renamed_at"]);
                info.OldTag = parsed["data"][strClanId]["old_tag"];
                info.OldName = parsed["data"][strClanId]["old_name"];
                info.IsClanDisbanded = (bool)parsed["data"][strClanId]["is_clan_disbanded"];

                List<Member> membersClan = new();
                for (int i = 0; i < info.MembersCount; i++)
                {
                    Member member = new()
                    {
                        Role = parsed["data"][strClanId]["leader_id"]["members"][i]["role"],
                        Role_i18n = parsed["data"][strClanId]["leader_id"]["members"][i]["role_i18n"],
                        JoinedAt =
                        Utils.ConvertFromTimestamp
                        ((int)parsed["data"][strClanId]["leader_id"]["members"][i]["joined_at"]),
                        AccountId = (int)parsed["data"][strClanId]["leader_id"]["members"][i]["account_id"],
                        AccountName = parsed["data"][strClanId]["leader_id"]["members"][i]["account_name"]
                    };

                    membersClan.Add(member);
                }

                info.Members = membersClan;
            }
            else
            {
                new CauseException().Cause(parsed.error.message);
            }

            return info;
        }
    }
}