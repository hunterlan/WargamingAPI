﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WargamingAPI.WoT.Exceptions;
using WargamingAPI.WoT.Models.Accounts;

namespace WargamingAPI.WoT.Actions
{
	public class InfoAccounts
	{
		public readonly string accountLink;
		public readonly List<string> typesInqury;

		public InfoAccounts()
		{
			accountLink = "https://api.worldoftanks.ru/wot/account/";
			typesInqury = new List<string>() { "list/?", "info/?", "tanks/?", "achievements/?" };
		}

		public List<Player> GetPlayers(string application_id, string search, List<string> fields)
		{
			List<Player> gotPlayers = new();
			string finalUrlRequest = string.Concat(accountLink, typesInqury[0],
				"application_id=", application_id, "&search=", search);
			if (fields is null)
			{
				string response = Request.GetResponse(finalUrlRequest);
				dynamic parsed = JsonConvert.DeserializeObject(response);
				string status = parsed.status;

				if (status == "ok")
				{
					int count = parsed.meta.count;
					for (int i = 0; i < count; i++)
					{
						Player player = new()
						{
							Nickname = parsed.data[i].nickname,
							AccountId = parsed.data[i].account_id
						};
						gotPlayers.Add(player);
					}
				}
				else
				{
					new CauseException().Cause((string)parsed.error.message);
				}
			}
/*			else
			{
				//TO-DO: Do non required fields
			}
*/

			return gotPlayers;
		}

		public PublicAccountInfo GetPPA(string application_id, Player player)
		{
			//TO-DO: Do non required fields
			PublicAccountInfo accountInfo = new() {	Player = player };

			string finalUrlRequest = string.Concat(accountLink, typesInqury[1],
				"application_id=", application_id, "&account_id=", player.AccountId);

			string response = Request.GetResponse(finalUrlRequest);
			dynamic parsed = JsonConvert.DeserializeObject(response);
			string status = parsed.status;

			if (status == "ok")
			{
				string strAccId = accountInfo.Player.AccountId.ToString();

				accountInfo.GlobalRating =
					(int)parsed["data"][strAccId]["global_rating"];
				var clan = parsed["data"][strAccId]["clan_id"];
				if (clan.Value == null)
				{
					accountInfo.ClanId = null;
				}
				else
				{
					accountInfo.ClanId = Convert.ToInt32(clan);
				}
				accountInfo.LastBattleTime =
					Request.ConvertFromTimestamp((int)parsed["data"][strAccId]["last_battle_time"]);
				accountInfo.LogoutAt =
					Request.ConvertFromTimestamp((int)parsed["data"][strAccId]["logout_at"]);
				accountInfo.CreatedAt =
					Request.ConvertFromTimestamp((int)parsed["data"][strAccId]["created_at"]);
				accountInfo.UpdatedAt =
					Request.ConvertFromTimestamp((int)parsed["data"][strAccId]["updated_at"]);
			}
			else
			{
				new CauseException().Cause(parsed.error.message);
			}

			return accountInfo;
		}

		public List<Tank> GetPlayersTank(string application_id, Player player)
		{
			List<Tank> tanks = new();

			string finalUrlRequest = string.Concat(accountLink, typesInqury[2],
				"application_id=", application_id, "&account_id=", player.AccountId);

			string response = Request.GetResponse(finalUrlRequest);
			dynamic parsed = JsonConvert.DeserializeObject(response);
			string status = parsed.status;

			if (status == "ok")
			{
				string strAccId = player.AccountId.ToString();
				int countTanks = (int)parsed["data"][strAccId].Count;
				for (int i = 0; i < countTanks; i++)
				{
					Tank tank = new()
					{
						MarkOfMastery = parsed["data"][strAccId][i]["mark_of_mastery"],
						TankId = parsed["data"][strAccId][i]["tank_id"],
						Battles = parsed["data"][strAccId][i]["statistics"]["battles"],
						Wins = parsed["data"][strAccId][i]["statistics"]["wins"]
					};

					tanks.Add(tank);
				}
			}
			else
			{
				new CauseException().Cause(parsed.error.message);
			}

			return tanks;
		}

		public PlayersAchievements GetPlayersAchivment(string application_id, Player player)
		{
			PlayersAchievements playersAchievements = new();

			string finalUrlRequest = string.Concat(accountLink, typesInqury[3],
				"application_id=", application_id, "&account_id=", player.AccountId);

			string response = Request.GetResponse(finalUrlRequest);
			dynamic parsed = JsonConvert.DeserializeObject(response);
			string status = parsed.status;

			if (status == "ok")
			{
				string strAccId = player.AccountId.ToString();
				playersAchievements = (PlayersAchievements)parsed["data"][strAccId];
			}
			else
			{
				new CauseException().Cause(parsed.error.message);
			}

			return playersAchievements;
		}
	}
}