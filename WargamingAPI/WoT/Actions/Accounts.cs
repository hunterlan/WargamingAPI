using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WargamingAPI.WoT.Exceptions;
using WargamingAPI.WoT.Models.Accounts;

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
                            nickname = parsed.data[i].nickname,
                            account_id = parsed.data[i].account_id
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

            string finalUrlRequest = string.Concat(accountLink, typesInqury[1],
                "application_id=", application_id, "&account_id=", player.account_id);

            string response = Request.GetResponse(finalUrlRequest);
            dynamic parsed = JsonConvert.DeserializeObject(response);
            string status = parsed.status;

            if (status == "ok")
            {
                string strAccId = accountInfo.player.account_id.ToString();

                accountInfo.global_rating = 
                    (int)parsed["data"][strAccId]["global_rating"];
                var clan = parsed["data"][strAccId]["clan_id"];
                if(clan.Value == null)
                {
                    accountInfo.clan_id = null;
                }
                else
                {
                    accountInfo.clan_id = Convert.ToInt32(clan);
                }
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

        public List<Tank> GetPlayersTank(string application_id, Player player)
        {
            List<Tank> tanks = new List<Tank>();

            string finalUrlRequest = string.Concat(accountLink, typesInqury[2],
                "application_id=", application_id, "&account_id=", player.account_id);

            string response = Request.GetResponse(finalUrlRequest);
            dynamic parsed = JsonConvert.DeserializeObject(response);
            string status = parsed.status;

            if (status == "ok")
            {
                string strAccId = player.account_id.ToString();
                int countTanks = (int)parsed["data"][strAccId].Count;
                for (int i = 0; i < countTanks; i++)
                {
                    Tank tank = new Tank();

                    tank.mark_of_mastery = parsed["data"][strAccId][i]["mark_of_mastery"];
                    tank.tank_id = parsed["data"][strAccId][i]["tank_id"];
                    tank.battles = parsed["data"][strAccId][i]["statistics"]["battles"];
                    tank.wins = parsed["data"][strAccId][i]["statistics"]["wins"];

                    tanks.Add(tank);
                }
            }

            return tanks;
        }

        public PlayersAchivment GetPlayersAchivment(string application_id, Player player)
        {
            PlayersAchivment playersAchivment = new PlayersAchivment();

            string finalUrlRequest = string.Concat(accountLink, typesInqury[3],
                "application_id=", application_id, "&account_id=", player.account_id);

            string response = Request.GetResponse(finalUrlRequest);
            dynamic parsed = JsonConvert.DeserializeObject(response);
            string status = parsed.status;
            
            if (status == "ok")
            {
                string strAccId = player.account_id.ToString();
                playersAchivment = (PlayersAchivment)parsed["data"][strAccId];
            }

            return playersAchivment;
        }
    }
}


