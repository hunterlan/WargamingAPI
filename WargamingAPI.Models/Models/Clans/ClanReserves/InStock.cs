using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models.Clans.ClanReserves
{
    public class InStock
    {
        public int ActionTime { get; set; }

        public int ActivatedAt { get; set; }

        public int ActiveTill { get; set; }

        public int Amount { get; set; }

        public int Level { get; set; }

        public string Status { get; set; }

        public bool XLevelOnly { get; set; }

        public BonusValues BonusValues { get; set; }
    }
}
