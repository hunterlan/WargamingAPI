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
    [XmlRoot("errors")]
    public class Errors
    {
        [XmlElement("error")]
        public List<Error> errors { get; set; }
    }

    public class XMLErrors
    {
        public Errors errors { get; set; }
        public void ReadErrors()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Errors));

            using (var reader = File.Open("ErrorString.xml", FileMode.Open))
            {
                errors = (Errors)serializer.Deserialize(reader);
            }
        }
    }
}
