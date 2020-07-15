using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using WargamingAPI.WoT.Actions;
using WargamingAPI.WoT.Models;

namespace TestAPI.WoT
{
    public class AccountTest
    {
        private string apiKey;
        Accounts accounts;
        [SetUp]
        public void Setup()
        {
            apiKey = GetApiKey();
            accounts = new Accounts();
        }

        public string GetApiKey()
        {
            XmlDocument xDoc = new XmlDocument();
            string apiKey = "";

            string path = "";

            for (int i = 0; i < 3; i++)
            {
                path = string.Concat(path, "..", Path.DirectorySeparatorChar);
            }
            path = string.Concat(path, "data.xml");
            xDoc.Load(path);
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNodeList nodes = xRoot.SelectNodes("/wot/apiKey");
            foreach (XmlElement node in nodes)
            {
                apiKey = node.InnerText;
            }

            return apiKey;
        }

        [Test]
        public void TestGettingAccessToken()
        {
            string nicknamePlayer = "Hunterlan2000";
            int expectedIdPlayer = 30373360;

            List<Player> players = accounts.GetPlayers(apiKey, nicknamePlayer);
            if (players.Count > 1)
            {
                Assert.Fail("Count of players more than one!");
            }
            else if (players[0].account_id != expectedIdPlayer ||
                !string.Equals(players[0].nickname, nicknamePlayer))
            {
                string message = string.Concat("Wrong data.\nExpected player: ", nicknamePlayer,
                    " ", expectedIdPlayer, "\nReal player: ", players[0].nickname, " ",
                    players[0].account_id);
                Assert.Fail(message);
            }
            else
            {
                Assert.Pass();
            }
        }

        [Test]
        public void TestGettingPPA()
        {
            Player player = new Player() { account_id = 30373360, nickname = "Hunterlan2000" };
            PublicAccountInfo expectedInfo = new PublicAccountInfo()
            {
                player = player,
                created_at = new System.DateTime(2014, 6, 1, 18, 24, 56)
            };

            PublicAccountInfo realInfo = accounts.GetPPA(apiKey, player);
            Assert.IsTrue(expectedInfo.Equals(realInfo));
        }

        [Test]
        public void TestGettingPlayersTank()
        {
            Player player = new Player() { account_id = 30373360, nickname = "Hunterlan2000" };
            Tank expectedTank = new Tank()
            {
                tank_id = 11777
            };

            List<Tank> tanks = accounts.GetPlayersTank(apiKey, player);
            foreach(Tank tank in tanks)
            {
                if(tank.tank_id == expectedTank.tank_id)
                {
                    Assert.Pass();
                }
            }

            Assert.Fail();
        }
    }
}