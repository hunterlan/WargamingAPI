using System.IO;
using System.Xml;

namespace TestAPI
{
    public class Helper
    {
        public static string GetApiKey()
        {
            XmlDocument xDoc = new();
            string apiKey = "";

            string path = "";

            for (int i = 0; i < 3; i++)
            {
                path = string.Concat(path, "..", Path.DirectorySeparatorChar);
            }
            path = string.Concat(path, "data.xml");
            xDoc.Load(path);
            XmlElement xRoot = xDoc.DocumentElement;
            XmlNodeList nodes = xRoot.SelectNodes("/wot/apiKey");
            foreach (XmlElement node in nodes)
            {
                apiKey = node.InnerText;
            }

            return apiKey;
        }
    }
}