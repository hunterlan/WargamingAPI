using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models
{
    public class Tank
    {
        public int mark_of_mastery {get; set;}

        public int tank_id { get; set; }

        public int battles { get; set; }

        public int wins { get; set; }

        public bool Equals(Tank comparor)
        {
            if(comparor.tank_id == tank_id)
            {
                return true;
            }
            return false;
        }
    }
}
