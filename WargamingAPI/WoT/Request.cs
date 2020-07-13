using System;
using System.IO;
using System.Net;

namespace WargamingAPI.WoT
{
    public class Request
    {
        public static string GetResponse(string urlRequest)
        {
            string resultResponse = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlRequest);
            request.ContentType = "application/json; charset=utf-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                resultResponse = sr.ReadToEnd();
            }

            return resultResponse;
        }

        protected DateTime ConvertFromTimestamp(int timestap)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestap).ToUniversalTime();
        }
    }
}
