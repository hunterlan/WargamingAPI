using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models.Clans
{
    public class Member
    {
        public string role { get; set; }
        public string role_i18n { get; set; }
        public int joined_at { get; set; }
        public int account_id { get; set; }
        public string account_name { get; set; }

    }
}
