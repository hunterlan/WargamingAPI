using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using WargamingAPI.WoT.Actions;
using WargamingAPI.WoT.Models.Accounts;

namespace TestAPI.WoT
{
	public class AccountTest
	{
		private string apiKey;
		InfoAccounts accounts;
		[SetUp]
		public void Setup()
		{
			apiKey = GetApiKey();
			accounts = new InfoAccounts();
		}

		public static string GetApiKey()
		{
			XmlDocument xDoc = new();
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

			List<Player> players = accounts.GetPlayers(apiKey, nicknamePlayer, null);
			if (players.Count > 1)
			{
				Assert.Fail("Count of players more than one!");
			}
			else if (players[0].AccountId != expectedIdPlayer || !string.Equals(players[0].Nickname, nicknamePlayer))
			{
				string message = string.Concat("Wrong data.\nExpected player: ", nicknamePlayer,
					" ", expectedIdPlayer, "\nReal player: ", players[0].Nickname, " ",
					players[0].AccountId);
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
			Player player = new() { AccountId = 30373360, Nickname = "Hunterlan2000" };
			PublicAccountInfo expectedInfo = new()
			{
				Player = player,
				CreatedAt = new System.DateTime(2014, 6, 1, 18, 24, 56)
			};

			PublicAccountInfo realInfo = accounts.GetPPA(apiKey, player);
			Assert.IsTrue(expectedInfo.Equals(realInfo));
		}

		[Test]
		public void TestGettingPlayersTank()
		{
			Player player = new() { AccountId = 30373360, Nickname = "Hunterlan2000" };
			Tank expectedTank = new() { TankId = 11777 };

			List<Tank> tanks = accounts.GetPlayersTank(apiKey, player);
			foreach(Tank tank in tanks)
			{
				if(tank.TankId == expectedTank.TankId)
				{
					Assert.Pass();
				}
			}

			Assert.Fail();
		}
	}
}