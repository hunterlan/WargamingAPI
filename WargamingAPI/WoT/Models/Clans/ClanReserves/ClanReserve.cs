using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models.Clans.ClanReserves
{
    public class ClanReserve
    {
        public string bonus_type { get; set; }

        public bool disposable { get; set; }
        
        public string icon { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public InStock in_stock { get; set; }
    }
}
