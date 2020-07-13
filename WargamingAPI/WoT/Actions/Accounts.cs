using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WargamingAPI.WoT.Exceptions;
using WargamingAPI.WoT.Models;

namespace WargamingAPI.WoT.Actions
{
    public class Accounts
    {
        public readonly string accountLink;
        public readonly List<string> typesInqury;

        public Accounts()
        {
            accountLink = "https://api.worldoftanks.ru/wot/account/";
            typesInqury = new List<string>() { "list/?", "info/?", "tanks/?", "achievements/?" };
        }

        public List<Player> GetPlayers(string application_id, string search,
            List<string> fields = null, string language = "", int limit = -1, string type = "")
        {
            List<Player> gotPlayers = new List<Player>();
            string finalUrlRequest = string.Concat(accountLink, typesInqury[0], 
                "application_id=", application_id, "&search=", search);
            if(fields == null)
            {
                string response = Request.GetResponse(finalUrlRequest);
                dynamic parsed = JsonConvert.DeserializeObject(response);
                string status = parsed.status;

                if (status == "ok")
                {
                    int count = parsed.meta.count;
                    for(int i = 0; i < count; i++)
                    {
                        Player player = new Player()
                        {
                            nickname = parsed.data[0].nickname,
                            account_id = parsed.data[0].account_id
                        };
                        gotPlayers.Add(player);
                    }
                }
                else
                {
                    string error = parsed.error.message;
                    if (error == "NOT_ENOUGH_SEARCH_LENGTH")
                    {
                        throw new SearchException("Минимум три символа требуется");
                    }
                    else if (error == "INVALID_SEARCH")
                    {
                        throw new SearchException("Неверный поиск");
                    }
                    else if (error == "SEARCH_NOT_SPECIFIED")
                    {
                        throw new SearchException("Пустой никнейм");
                    }
                    else
                    {
                        throw new Exception("Something went wrong.");
                    }
                }
            }
            else
            {
                //TO-DO: Do non required fields
            }

            return gotPlayers;
        }

        public PublicAccountInfo GetPPA(string application_id, Player player, string access_token = "", 
            string extra = "", List<string> fields = null, string language = "")
        {
            //TO-DO: Do non required fields
            PublicAccountInfo accountInfo = new PublicAccountInfo();
            accountInfo.player = player;

            string finalUrlRequest = string.Concat(accountLink, typesInqury[0],
                "application_id=", application_id, "&account_id=", player.account_id);

            string response = Request.GetResponse(finalUrlRequest);
            dynamic parsed = JsonConvert.DeserializeObject(response);
            string status = parsed.status;

            if (status == "ok")
            {
                string strAccId = accountInfo.player.account_id.ToString();

                accountInfo.global_rating = 
                    (int)parsed["data"][strAccId]["global_rating"];
                accountInfo.clan_id = (int)parsed["data"][strAccId]["clan_id"];
                accountInfo.last_battle_time = 
                    Request.ConvertFromTimestamp((int)parsed["data"][strAccId]["last_battle_time"]);
                accountInfo.logout_at = 
                    Request.ConvertFromTimestamp((int)parsed["data"][strAccId]["logout_at"]);
                accountInfo.created_at =
                    Request.ConvertFromTimestamp((int)parsed["data"][strAccId]["created_at"]);
                accountInfo.updated_at =
                    Request.ConvertFromTimestamp((int)parsed["data"][strAccId]["updated_at"]);
            }
            else
            {
                string error = parsed.error.message;
                if (error == "NOT_ENOUGH_SEARCH_LENGTH")
                {
                    throw new SearchException("Минимум три символа требуется");
                }
                else if (error == "INVALID_SEARCH")
                {
                    throw new SearchException("Неверный поиск");
                }
                else if (error == "SEARCH_NOT_SPECIFIED")
                {
                    throw new SearchException("Пустой никнейм");
                }
                else
                {
                    throw new Exception("Something went wrong.");
                }
            }

            return accountInfo;
        }
    }
}


