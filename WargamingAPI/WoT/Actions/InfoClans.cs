using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WargamingAPI.Models.Clans;
using WargamingAPI.WoT.Exceptions;
using WargamingAPI.WoT.Models.Clans;

namespace WargamingAPI.WoT.Actions
{
    public class InfoClans
    {
        private readonly string _clanLink;
        private readonly List<string> _typesInqury;

        public InfoClans()
        {
            _clanLink = "https://api.worldoftanks.ru/wot/clans/";
            _typesInqury = new List<string>() { "list/?", "info/?" };
        }
        
        /// <summary>
        /// Method searches through clans and sorts them in a specified order.
        /// </summary>
        /// <param name="applicationId">Application key</param>
        /// <param name="search">Part of name or tag for clan search. Minimum 2 characters</param>
        /// <returns>List of <see cref="Clan"/>. Doesn't contain emblems. </returns>

        public List<Clan> GetClans(string applicationId, string search)
        {
            List<Clan> clans = new();

            string finalUrlRequest = string.Concat(_clanLink, _typesInqury[0],
                "application_id=", applicationId, "&search=", search);

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

        /// <summary>
        /// Method returns detailed clan information.
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="clan">Specified <see cref="Clan"/>, for getting rating. To get it, see
        /// <see cref="InfoClans.GetClans"/></param>
        /// <returns>Clan's detail, specified in class <see cref="ClanInfo"/></returns>
        public ClanInfo GetClanDetails(string applicationId, Clan clan)
        {
            ClanInfo info = new();

            string finalUrlRequest = string.Concat(_clanLink, _typesInqury[1],
               "application_id=", applicationId, "&clan_id=", clan.ClanId);

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