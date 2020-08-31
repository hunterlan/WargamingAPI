using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models.Clans.ClanReserves
{
    public class InStock
    {
        public int action_time { get; set; }

        public int activated_at { get; set; }

        public int active_till { get; set; }

        public int amount { get; set; }

        public int level { get; set; }

        public string status { get; set; }

        public bool x_level_only { get; set; }

        public BonusValues bonusValues { get; set; }
    }
}
