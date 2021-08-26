using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models.Clans.Strongholds
{
    public class Stronghold
    {
        public string CommandCenterArenaId { get; set; }
        public int StrongholdBuildingsLevel { get; set; }
        public SkirmishStatistics SkirmishStatistics { get; set; }
        public BattlesSeriesForStrongholdsStatistics BattlesSeriesForStrongholdsStatistics { get; set; }
        public string ClanName { get; set; }
        public int StrongholdLevel { get; set; }
        public int ClanId { get; set; }
        public BattlesForStrongholdsStatistics BattlesForStrongholdsStatistics { get; set; }
        public List<BuildingSlot> BuildingSlots { get; set; }
        public string ClanTag { get; set; }
    }
}
