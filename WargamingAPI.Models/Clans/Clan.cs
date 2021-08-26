using System.Text;

namespace WargamingAPI.Models.Clans
{
    public class Clan
    {
        public int MembersCount { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int CreatedAt { get; set; }
        public string Tag { get; set; }
        public int ClanId { get; set; }

        public bool Equals(Clan comparorClan)
        {
            return ClanId == comparorClan.ClanId;
        }

        public override string ToString()
        {
            StringBuilder sb = new ();
            sb.Append(Name).Append(Tag).Append(Color).Append(ClanId).Append(CreatedAt);

            return sb.ToString();
        }
    }
}
