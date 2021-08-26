using System;
using System.IO;
using System.Net;

namespace WargamingAPI
{
	public class Utils
	{
		public static string GetResponse(string urlRequest)
		{
			string resultResponse = string.Empty;

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlRequest);
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();

			using (StreamReader sr = new(response.GetResponseStream()))
			{
				resultResponse = sr.ReadToEnd();
			}

			return resultResponse;
		}

		public static DateTime ConvertFromTimestamp(int timestamp)
		{
			DateTime origin = DateTime.UnixEpoch;
			return origin.AddSeconds(timestamp);
		}
	}
}