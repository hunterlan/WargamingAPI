using System.Linq;
using NUnit.Framework;
using WargamingAPI.Models.Clans;
using WargamingAPI.WoT.Actions;

namespace TestAPI.WoT
{
    public class InfoClansTest
    {
        private string _apiKey;
        private InfoClans _infoClans;
        
        [SetUp]
        public void Setup()
        {
            _apiKey = Helper.GetApiKey();
            _infoClans = new InfoClans();
        }

        [Test]
        public void TestGetClans()
        {
            Clan expectedClan = new() { ClanId = 101221 };
            var listClans = _infoClans.GetClans(_apiKey, "MERCY");
            
            if (listClans.Any(clan => clan.Equals(expectedClan)))
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail("Clan wasn't found");
            }
        }
    }
}