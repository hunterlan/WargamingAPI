using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using WargamingAPI;
using WargamingAPI.WoT.Actions;
using WargamingAPI.WoT.Models;

namespace TestAPI
{
    public class RequestTest
    {
        [Test]
        public void TestConvertingFromTimeStamp()
        {
            int timestamp = 1593301376;

            DateTime expectedTime = new DateTime(2020, 6, 27, 23, 42, 56);
            DateTime resultTime = Request.ConvertFromTimestamp(timestamp);

            Assert.AreEqual(expectedTime, resultTime);
        }
    }
}
