using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WargamingAPI.WoT.Actions;
using WargamingAPI.WoT.Models.Accounts;

namespace TestAPI.WoT
{
	public class AccountTest
	{
		private string _apiKey;
		private InfoAccounts _accounts;
		private const string NicknamePlayer = "Hunterlan2000";
		private const int ExpectedIdPlayer = 30373360;
		[SetUp]
		public void Setup()
		{
			_apiKey = Helper.GetApiKey();
			_accounts = new InfoAccounts();
		}

		[Test]
		public void TestGettingAccessToken()
		{
			List<Player> players = _accounts.GetPlayers(_apiKey, NicknamePlayer, null);
			if (players.Count > 1)
			{
				Assert.Fail("Count of players more than one!");
			}
			else if (players[0].AccountId != ExpectedIdPlayer || !string.Equals(players[0].Nickname, NicknamePlayer))
			{
				string message = string.Concat("Wrong data.\nExpected player: ", NicknamePlayer,
					" ", ExpectedIdPlayer, "\nReal player: ", players[0].Nickname, " ",
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
			Player player = new() { AccountId = ExpectedIdPlayer, Nickname = NicknamePlayer };
			PublicAccountInfo expectedInfo = new()
			{
				Player = player,
				CreatedAt = new System.DateTime(2014, 6, 1, 18, 24, 56)
			};

			var realInfo = _accounts.GetPPA(_apiKey, player);
			Assert.IsTrue(realInfo.Player.AccountId == expectedInfo.Player.AccountId &&
			              realInfo.CreatedAt == expectedInfo.CreatedAt);
		}

		[Test]
		public void TestGettingPlayersTank()
		{
			Player player = new() { AccountId = ExpectedIdPlayer, Nickname = NicknamePlayer };
			Tank expectedTank = new() { TankId = 11777 };
			var tanks = _accounts.GetPlayersTank(_apiKey, player);
			
			if (tanks.Any(tank => tank.TankId == expectedTank.TankId))
			{
				Assert.Pass();
			}
			else
			{
				Assert.Fail("Tank wasn't found");
			}
		}
	}
}