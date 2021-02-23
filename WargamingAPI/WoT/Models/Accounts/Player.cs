using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Models.Accounts
{
	public class Player
	{
		public int AccountId { get; set; }

		public string Nickname { get; set; }

		public bool Equals(Player comparor) => comparor.Nickname == Nickname && AccountId == comparor.AccountId;
	}
}
