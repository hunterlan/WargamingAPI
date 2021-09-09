using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WargamingAPI.WoT.Exceptions;
using WargamingAPI.WoT.Models.Accounts;

namespace WargamingAPI.WoT.Actions
{
	public class InfoAccounts
	{
		private readonly string _accountLink;
		private readonly List<string> _typesInqury;

		public InfoAccounts()
		{
			_accountLink = "https://api.worldoftanks.ru/wot/account/";
			_typesInqury = new List<string>() { "list/?", "info/?", "tanks/?", "achievements/?" };
		}

		/// <summary>
		/// Method returns partial list of players.
		/// The list is filtered by initial characters of user name and sorted alphabetically.
		/// </summary>
		/// <param name="applicationId">Application key</param>
		/// <param name="search">Player name search string</param>
		/// <param name="fields">Non required fields, enumerated in API.
		/// Key use as name, mentioned in Wargaming API site.</param>
		/// <returns>List of <see cref="Player"/></returns>
		public List<Player> GetPlayers(string applicationId, string search, Dictionary<string, string> fields)
		{
			List<Player> gotPlayers = new();
			string finalUrlRequest = string.Concat(_accountLink, _typesInqury[0],
				"application_id=", applicationId, "&search=", search);
			if (fields is not null)
			{
				foreach (var field in fields)
				{
					finalUrlRequest = string.Concat(finalUrlRequest, $"&{field.Key}={field.Value}");
				}
			}
			
			string response = Utils.GetResponse(finalUrlRequest);
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

			return gotPlayers;
		}

		/// <summary>
		/// Method returns player details.
		/// </summary>
		/// <param name="applicationId">Application key</param>
		/// <param name="player">Specified <see cref="Player"/>. To get it, see <see cref="GetPlayers"/></param>
		/// <returns>Details information about player. Fields specified in <see cref="PublicAccountInfo"/></returns>
		public PublicAccountInfo GetPPA(string applicationId, Player player)
		{
			//TO-DO: Do non required fields
			PublicAccountInfo accountInfo = new() {	Player = player };

			string finalUrlRequest = string.Concat(_accountLink, _typesInqury[1],
				"application_id=", applicationId, "&account_id=", player.AccountId);

			string response = Utils.GetResponse(finalUrlRequest);
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
					Utils.ConvertFromTimestamp((int)parsed["data"][strAccId]["last_battle_time"]);
				accountInfo.LogoutAt =
					Utils.ConvertFromTimestamp((int)parsed["data"][strAccId]["logout_at"]);
				accountInfo.CreatedAt =
					Utils.ConvertFromTimestamp((int)parsed["data"][strAccId]["created_at"]);
				accountInfo.UpdatedAt =
					Utils.ConvertFromTimestamp((int)parsed["data"][strAccId]["updated_at"]);
			}
			else
			{
				new CauseException().Cause(parsed.error.message);
			}

			return accountInfo;
		}

		/// <summary>
		/// Method returns details on player's vehicles.
		/// </summary>
		/// <param name="applicationId">Application key</param>
		/// <param name="player">Specified <see cref="Player"/>. To get it, see <see cref="GetPlayers"/></param>
		/// <returns>List of player's <see cref="Tank"/></returns>
		public List<Tank> GetPlayersTank(string applicationId, Player player)
		{
			List<Tank> tanks = new();

			string finalUrlRequest = string.Concat(_accountLink, _typesInqury[2],
				"application_id=", applicationId, "&account_id=", player.AccountId);

			string response = Utils.GetResponse(finalUrlRequest);
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

		/// <summary>
		/// Method returns player's achievement details.
		/// </summary>
		/// <param name="applicationId">Application key</param>
		/// <param name="player">Specified <see cref="Player"/>. To get it, see <see cref="GetPlayers"/></param>
		/// <returns>Series, frags and achievements, specified in class <see cref="PlayersAchievements"/></returns>
		public PlayersAchievements GetPlayersAchievement(string applicationId, Player player)
		{
			PlayersAchievements playersAchievements = new();

			string finalUrlRequest = string.Concat(_accountLink, _typesInqury[3],
				"application_id=", applicationId, "&account_id=", player.AccountId);

			string response = Utils.GetResponse(finalUrlRequest);
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