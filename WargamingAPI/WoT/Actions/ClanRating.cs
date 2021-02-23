using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WargamingAPI.WoT.Exceptions;
using WargamingAPI.WoT.Models.Clans;

namespace WargamingAPI.WoT.Actions
{
	public class ClanRating
	{
		List<string> _rankFields;
		List<int> _avaliableDates;
		public readonly string _clanRatingLink;
		public readonly List<string> typesInqury;

		public ClanRating()
		{
			_rankFields = new List<string>();
			_avaliableDates = new List<int>();
			_clanRatingLink = "https://api.worldoftanks.ru/wot/clanratings/";
			typesInqury = new List<string>() { "types/?", "dates/?", "clans/?", "neighbors/?", "top/?" };
		}

		public void GetRankFields(string application_id)
		{
			string finalUrlRequest = string.Concat(_clanRatingLink, typesInqury[0],
				"application_id=", application_id);

			string response = Request.GetResponse(finalUrlRequest);
			dynamic parsed = JsonConvert.DeserializeObject(response);
			string status = parsed.status;

			if(status == "ok")
			{
				_rankFields = (List<string>)parsed["data"]["all"];
			}
			else
			{
				new CauseException().Cause(parsed.error.message);
			}
		}

		public void GetAvailabeDate(string application_id, int? limit)
		{
			string finalUrlRequest = string.Concat(_clanRatingLink, typesInqury[0],
				"application_id=", application_id);
			if(limit != null)
			{
				finalUrlRequest = string.Concat(finalUrlRequest, "&limit=", limit.ToString());
			}

			string response = Request.GetResponse(finalUrlRequest);
			dynamic parsed = JsonConvert.DeserializeObject(response);
			string status = parsed.status;

			if (status == "ok")
			{
				_avaliableDates = (List<int>)parsed["data"]["all"];
			}
			else
			{
				new CauseException().Cause(parsed.error.message);
			}
		}

		public List<Rating> GetRatingClans(string application_id, Clan clan)
		{
			List<Rating> ratingsOfClan = new();
			string finalUrlRequest = string.Concat(_clanRatingLink, typesInqury[0],
			   "application_id=", application_id, "&clan_id=", clan.ClanId.ToString());

			string response = Request.GetResponse(finalUrlRequest);
			dynamic parsed = JsonConvert.DeserializeObject(response);
			string status = parsed.status;

			if(status == "ok")
			{
				GetRankFields(application_id);
				foreach(string rank_field in _rankFields)
				{
					Rating clanRating = new()
					{
						Rank = parsed["data"][clan.ClanId][rank_field]["rank"],
						RankDelta = parsed["data"][clan.ClanId][rank_field]["rank_delta"],
						Value = parsed["data"][clan.ClanId][rank_field]["value"],
						NameRating = rank_field
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

		//Something strange in this function API. It returns nothing. 
		public void GetTopClans(string application_id, string rank_filed, int date)
		{
			List<Rating> ratingsOfClan = new();
			string finalUrlRequest = string.Concat(_clanRatingLink, typesInqury[0],
			   "application_id=", application_id, "&rank_field=", rank_filed, "&date=", date);

			string response = Request.GetResponse(finalUrlRequest);
			dynamic parsed = JsonConvert.DeserializeObject(response);
			string status = parsed.status;

			if (status == "ok")
			{
				throw new NotImplementedException();
			}
		}
	}
}
