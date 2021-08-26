using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models.Clans
{
    public class ClanInfo
    {
        public int LeaderId { get; set; }
        public string Color { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Tag { get; set; }
        public int MembersCount { get; set; }
        public string DescriptionHtml { get; set; }
        public bool AcceptsJoinRequests { get; set; }
        public string LeaderName { get; set; }
        public int ClanId { get; set; }
        public DateTime RenamedAt { get; set; }
        public string OldTag { get; set; }
        public string Description { get; set; }
        public List<Member> Members { get; set; }
        public string OldName { get; set; }
        public bool IsClanDisbanded { get; set; }
        public string Motto { get; set; }
        public string Name { get; set; }
        public string CreatorName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatorId { get; set; }

    }
}
