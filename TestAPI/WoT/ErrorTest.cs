using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using WargamingAPI.WoT.Actions;
using WargamingAPI.WoT.Exceptions;
using WargamingAPI.WoT.Models.Accounts;
using WargamingAPI.WoT.Models.Clans;

namespace TestAPI.WoT
{
	class ErrorTest
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
		public void TestForGettingError()
		{
			var ex = Assert.Throws<SearchException>(CauseError);
			Assert.That(ex.Message, Is.EqualTo("Empty search field"));
		}

		public void CauseError()
		{
			string nicknamePlayer = "";

			List<Player> players = accounts.GetPlayers(apiKey, nicknamePlayer, null);
		}
	}
}
