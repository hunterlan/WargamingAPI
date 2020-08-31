using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models.Clans.Strongholds
{
    public class Stronghold
    {
        public string command_center_arena_id { get; set; }
        public int stronghold_buildings_level { get; set; }
        public SkirmishStatistics skirmish_statistics { get; set; }
        public BattlesSeriesForStrongholdsStatistics battles_series_for_strongholds_statistics { get; set; }
        public string clan_name { get; set; }
        public int stronghold_level { get; set; }
        public int clan_id { get; set; }
        public BattlesForStrongholdsStatistics battles_for_strongholds_statistics { get; set; }
        public List<BuildingSlot> building_slots { get; set; }
        public string clan_tag { get; set; }
    }
}
