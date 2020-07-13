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

        public void GetPPA(string application_id, string account_id, string access_token = "", 
            string extra = "", List<string> fields = null, string language = "")
        {
            //TO-DO: Do non required fields
        }
    }
}


