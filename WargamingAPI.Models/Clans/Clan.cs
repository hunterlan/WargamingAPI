using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models.Clans
{
    public class Clan
    {
        public int MembersCount { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int CreatedAt { get; set; }
        public string Tag { get; set; }
        public int ClanId { get; set; }
    }
}
