using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models
{
    public class Clan
    {
        public int members_count { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public int created_at { get; set; }
        public string tag { get; set; }
        public int clan_id { get; set; }
    }
}
