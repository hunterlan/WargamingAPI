using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using WargamingAPI.Models.Clans;
using WargamingAPI.WoT.Exceptions;
using WargamingAPI.WoT.Models.Clans;
using WargamingAPI.WoT.Models.Clans.ClanReserves;
using WargamingAPI.WoT.Models.Clans.Strongholds;

namespace WargamingAPI.WoT.Actions
{
	public class InfoStrongholds
	{
		private readonly string _strongholdsLink;
		private readonly List<string> _typesRequest;

		public InfoStrongholds()
		{
			_strongholdsLink = "https://api.worldoftanks.ru/wot/stronghold/";
			_typesRequest = new List<string>() { "claninfo/?", "clanreserve/?", "activateclanreserve/" };
		}

		public Stronghold GetStronghold(string applicationId, Clan clan)
		{
			string finalUrlRequest = string.Concat(_strongholdsLink, _typesRequest[0], "application_id=", applicationId, "&clan_id=", clan.ClanId);

			string response = Utils.GetResponse(finalUrlRequest);
			string innerJson = JObject.Parse(response)["data"][clan.ClanId.ToString()].ToString();
			return JsonConvert.DeserializeObject<Stronghold>(innerJson);
		}

		//Not sure about it. Can't check, because, I'm not in clan
		public ClanReserve GetClanReserve(string applicationId, string accessToken)
		{
			string finalUrlRequest = string.Concat(_strongholdsLink, _typesRequest[1],
				"application_id=", applicationId, "&access_token=", accessToken);

			string response = Utils.GetResponse(finalUrlRequest);
			string innerJson = JObject.Parse(response)["data"].ToString();
			return JsonConvert.DeserializeObject<ClanReserve>(innerJson);
		}

		//TO-DO: In future, add function, which activate reserves
	}
}
