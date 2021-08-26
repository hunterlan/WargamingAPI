using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WargamingAPI.Models.Clans;
using WargamingAPI.WoT.Exceptions;
using WargamingAPI.WoT.Models.Clans;

namespace WargamingAPI.WoT.Actions
{
	public class ClanRating
	{
		private readonly string _clanRatingLink;
		private readonly List<string> _requestTypes;

		/// <summary>
		/// Init links and types of requests.
		/// </summary>
		public ClanRating()
		{
			_clanRatingLink = "https://api.worldoftanks.ru/wot/clanratings/";
			_requestTypes = new List<string> { "types/?", "dates/?", "clans/?", "neighbors/?", "top/?" };
		}

		/// <summary>
		/// Method returns list of types of rating.
		/// </summary>
		/// <param name="applicationId">Application key</param>
		/// <returns>List of strings, which contain all types of rating</returns>
		public List<string> GetRankFields(string applicationId)
		{
			List<string> rankFields = new();
			var finalUrlRequest = string.Concat(_clanRatingLink, _requestTypes[0],
				"application_id=", applicationId);

			var response = Utils.GetResponse(finalUrlRequest);
			dynamic parsed = JsonConvert.DeserializeObject(response);
			string status = parsed?.status;

			if(status == "ok")
			{
				rankFields = (List<string>)parsed["data"]["all"];
			}
			else
			{
				new CauseException().Cause(parsed?.error.message);
			}

			return rankFields;
		}

		/// <summary>
		/// Method returns dates with available rating data.
		/// </summary>
		/// <param name="applicationId">Application key</param>
		/// <param name="limit">Count of returned values, but not bigger, than 365.
		/// Otherwise, default value is 7.</param>
		/// <returns>List of timestamp integers</returns>
		public List<int> GetAvailableDate(string applicationId, int limit = 7)
		{
			List<int> availableDates = new();
			var finalUrlRequest = string.Concat(_clanRatingLink, _requestTypes[0],
				"application_id=", applicationId, "&limit=", limit.ToString());

			string response = Utils.GetResponse(finalUrlRequest);
			dynamic parsed = JsonConvert.DeserializeObject(response);
			string status = parsed.status;

			if (status == "ok")
			{
				availableDates = (List<int>)parsed["data"]["all"];
			}
			else
			{
				new CauseException().Cause(parsed.error.message);
			}

			return availableDates;
		}

		/// <summary>
		/// Method returns clan's rating.
		/// </summary>
		/// <param name="applicationId">Application key</param>
		/// <param name="clan">Specified <see cref="Clan"/>, for getting rating. To get it, see
		/// <see cref="InfoClans.GetClans"/></param>
		/// <returns>List of strings, which contain all types of rating</returns>
		public List<Rating> GetRatingClans(string applicationId, Clan clan)
		{
			List<Rating> ratingsOfClan = new();
			string finalUrlRequest = string.Concat(_clanRatingLink, _requestTypes[0],
			   "application_id=", applicationId, "&clan_id=", clan.ClanId.ToString());

			string response = Utils.GetResponse(finalUrlRequest);
			dynamic parsed = JsonConvert.DeserializeObject(response);
			string status = parsed.status;

			if(status == "ok")
			{
				var rankFields = GetRankFields(applicationId);
				foreach (var rankField in rankFields)
				{
					Rating clanRating = new()
					{
						Rank = parsed["data"][clan.ClanId][rankField]["rank"],
						RankDelta = parsed["data"][clan.ClanId][rankField]["rank_delta"],
						Value = parsed["data"][clan.ClanId][rankField]["value"],
						NameRating = rankField
					};

					ratingsOfClan.Add(clanRating);
				}
			}
			else
			{
				new CauseException().Cause(parsed.error.message);
			}

			return ratingsOfClan;
		}
		
		/// <summary>
		/// Method will return top clans for specified field.
		/// Work in progress.
		/// </summary>
		public void GetTopClans(string application_id, string rank_filed, int date)
		{
			//Something strange in this function API. It returns nothing. 
			List<Rating> ratingsOfClan = new();
			string finalUrlRequest = string.Concat(_clanRatingLink, _requestTypes[0],
			   "application_id=", application_id, "&rank_field=", rank_filed, "&date=", date);

			string response = Utils.GetResponse(finalUrlRequest);
			dynamic parsed = JsonConvert.DeserializeObject(response);
			string status = parsed.status;

			if (status == "ok")
			{
				throw new NotImplementedException();
			}
		}
	}
}
