using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models.Accounts
{
    public class PublicAccountInfo
    {
        public Player Player { get; set; }

        public int? ClanId { get; set; }

        public DateTime CreatedAt { get; set; }

        public int GlobalRating { get; set; }

        public DateTime LastBattleTime { get; set; }

        public DateTime LogoutAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
