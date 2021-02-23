using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models.Clans.Strongholds
{
    public class BattlesForStrongholdsStatistics
    {
        public int Win10 { get; set; }
        public int Total10 { get; set; }
        public int Total10In28d { get; set; }
        public int Win10in28d { get; set; }
        public int LastTime10 { get; set; }
        public int Lose10 { get; set; }
    }
}
