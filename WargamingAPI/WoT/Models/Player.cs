using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models
{
    public class Player
    {
        public int account_id { get; set; }

        public string nickname { get; set; }

        public bool Equals(Player comparor)
        {
            if(string.Equals(comparor.nickname, nickname) && account_id == comparor.account_id)
            {
                return true;
            }
            return false;
        }
    }
}
