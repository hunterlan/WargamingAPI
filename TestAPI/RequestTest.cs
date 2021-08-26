using NUnit.Framework;
using System;
using WargamingAPI;

namespace TestAPI
{
    public class RequestTest
    {
        [Test]
        public void TestConvertingFromTimeStamp()
        {
            int timestamp = 1593301376;

            DateTime expectedTime = new(2020, 6, 27, 23, 42, 56);
            DateTime resultTime = Utils.ConvertFromTimestamp(timestamp);

            Assert.AreEqual(expectedTime, resultTime);
        }
    }
}
