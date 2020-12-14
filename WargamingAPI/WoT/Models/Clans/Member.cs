using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models.Clans
{
    public class Member
    {
        public string Role { get; set; }
        public string Role_i18n { get; set; }
        public DateTime JoinedAt { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
    }
}
