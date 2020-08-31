using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models.Clans.Strongholds
{
    public class BuildingSlot
    {
        public string direction { get; set; }
        public string arena_id { get; set; }
        public string reserve_title { get; set; }
        public int? building_level { get; set; }
        public string position { get; set; }
        public string building_title { get; set; }
    }
}
