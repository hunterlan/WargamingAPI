using System;
using System.IO;
using System.Net;

namespace WargamingAPI
{
	public class Request
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
			//UTC
			DateTime origin = DateTime.UnixEpoch;   // Use DateTime.UnixEpoch. See : https://docs.microsoft.com/en-gb/dotnet/api/system.datetime.unixepoch?view=net-5.0
			return origin.AddSeconds(timestamp);
		}
	}
}