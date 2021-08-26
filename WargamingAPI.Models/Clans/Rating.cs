using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models.Clans
{
    public class Rating
    {
        public int? RankDelta { get; set; }

        public int? Rank { get; set; }

        public int Value { get; set; }

        public string NameRating { get; set; }
    }
}
