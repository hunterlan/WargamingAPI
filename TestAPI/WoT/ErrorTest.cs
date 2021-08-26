using NUnit.Framework;
using System.Collections.Generic;
using WargamingAPI.WoT.Actions;
using WargamingAPI.WoT.Exceptions;
using WargamingAPI.WoT.Models.Accounts;

namespace TestAPI.WoT
{
	class ErrorTest
	{
		private string _apiKey;
		private InfoAccounts _accounts;
		[SetUp]
		public void Setup()
		{
			_apiKey = Helper.GetApiKey();
			_accounts = new InfoAccounts();
		}

		[Test]
		public void TestForGettingError()
		{
			var ex = Assert.Throws<SearchException>(CauseError);
			Assert.That(ex.Message, Is.EqualTo("Empty search field"));
		}

		private void CauseError()
		{
			var nicknamePlayer = string.Empty;

			_accounts.GetPlayers(_apiKey, nicknamePlayer, null);
		}
	}
}
