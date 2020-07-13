using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models
{
    public class PublicAccountInfo
    {
        public Player player { get; set; }

        public int? clan_id { get; set; }

        public DateTime created_at { get; set; }

        public int global_rating { get; set; }

        public DateTime last_battle_time { get; set; }

        public DateTime logout_at { get; set; }

        public DateTime updated_at { get; set; }
    }
}
