using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models.Clans
{
    public class Rating
    {
        public int? rank_delta { get; set; }

        public int? rank { get; set; }

        public int value { get; set; }

        public string name_rating { get; set; }
    }
}
