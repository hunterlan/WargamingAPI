using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace WargamingAPI
{
	[XmlType("error")]
	public class Error
	{
		[XmlElement("name")]
		public string TypeError { get; set; }
		[XmlElement("description")]
		public string Description { get; set; }
	}

	[XmlRoot(ElementName="errors")]
	public class XMLErrors
	{
		[XmlElement("error")]
		public List<Error> Errors { get; set; }

		public void ReadErrors()
		{
			XmlSerializer serializer = new(typeof(List<Error>), new XmlRootAttribute("errors"));

			using var reader = File.Open("ErrorString.xml", FileMode.Open);
			Errors = (List<Error>)serializer.Deserialize(reader);
		}
	}
}
