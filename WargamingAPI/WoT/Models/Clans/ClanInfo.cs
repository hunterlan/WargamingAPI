using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models.Clans
{
    public class ClanInfo
    {
        public int leader_id { get; set; }
        public string color { get; set; }
        public int updated_at { get; set; }
        public string tag { get; set; }
        public int members_count { get; set; }
        public string description_html { get; set; }
        public bool accepts_join_requests { get; set; }
        public string leader_name { get; set; }
        public int clan_id { get; set; }
        public int renamed_at { get; set; }
        public string old_tag { get; set; }
        public string description { get; set; }
        public List<Member> members { get; set; }
        public string old_name { get; set; }
        public bool is_clan_disbanded { get; set; }
        public string motto { get; set; }
        public string name { get; set; }
        public string creator_name { get; set; }
        public int created_at { get; set; }
        public int creator_id { get; set; }

    }
}
