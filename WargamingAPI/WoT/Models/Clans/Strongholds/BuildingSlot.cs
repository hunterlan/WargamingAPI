using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models.Clans.Strongholds
{
    public class BuildingSlot
    {
        public string Direction { get; set; }
        public string ArenaId { get; set; }
        public string ReserveTitle { get; set; }
        public int? BuildingLevel { get; set; }
        public string Position { get; set; }
        public string BuildingTitle { get; set; }
    }
}
