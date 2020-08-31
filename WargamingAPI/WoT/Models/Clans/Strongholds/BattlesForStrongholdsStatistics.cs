using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models.Clans.Strongholds
{
    public class BattlesForStrongholdsStatistics
    {
        public int win_10 { get; set; }
        public int total_10 { get; set; }
        public int total_10_in_28d { get; set; }
        public int win_10_in_28d { get; set; }
        public int last_time_10 { get; set; }
        public int lose_10 { get; set; }
    }
}
