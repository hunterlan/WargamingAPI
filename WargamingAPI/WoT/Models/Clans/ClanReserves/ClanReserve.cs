using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models.Clans.ClanReserves
{
    public class ClanReserve
    {
        public string BonusType { get; set; }

        public bool Disposable { get; set; }
        
        public string Icon { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public InStock InStock { get; set; }
    }
}
