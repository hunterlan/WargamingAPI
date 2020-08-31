using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using WargamingAPI.WoT.Models.Clans;
using WargamingAPI.WoT.Models.Clans.ClanReserves;
using WargamingAPI.WoT.Models.Clans.Strongholds;

namespace WargamingAPI.WoT.Actions
{
    public class InfoStrongholds
    {
        public readonly string strongholdsLink;
        public readonly List<string> typesInqury;

        public InfoStrongholds()
        {
            strongholdsLink = "https://api.worldoftanks.ru/wot/stronghold/";
            typesInqury = new List<string>() { "claninfo/?", "clanreserve/?", "activateclanreserve/" };
        }

        public Stronghold GetStronghold(string application_id, Clan clan)
        {
            Stronghold stronghold = new Stronghold();

            string finalUrlRequest = string.Concat(strongholdsLink, typesInqury[0],
                "application_id=", application_id, "&clan_id=", clan.clan_id);

            string response = Request.GetResponse(finalUrlRequest);
            var innerJson = JObject.Parse(response)["data"][clan.clan_id.ToString()].ToString();
            stronghold = JsonConvert.DeserializeObject<Stronghold>(innerJson);

            return stronghold;
        }

        //Not sure about it. Can't check, because, I'm not in clan
        public ClanReserve GetClanReserve(string application_id, string access_token)
        {
            ClanReserve reserve = new ClanReserve();

            string finalUrlRequest = string.Concat(strongholdsLink, typesInqury[1],
                "application_id=", application_id, "&access_token=", access_token);

            string response = Request.GetResponse(finalUrlRequest);
            var innerJson = JObject.Parse(response)["data"].ToString();
            reserve = JsonConvert.DeserializeObject<ClanReserve>(innerJson);

            return reserve;
        }

        //TO-DO: In future, add function, which activate reserves
    }
}
