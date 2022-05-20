using NUnit.Framework;
using System;

namespace Parking
{
    [TestFixture]
    public class Q1Test
    {

        public void AssertMethod(string startValue, string endValue, int expected)
        {
            string date = "2022/5/6 ";
            startValue = date + startValue;
            endValue = date + endValue;

            DateTime start = Convert.ToDateTime(startValue);
            DateTime end = Convert.ToDateTime(endValue);

            int actual = ParkingFeeBiz.StatWorkFlow(start, end);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("9:00:00", "9:00:59", 0)]
        [TestCase("9:00:00", "9:01:59", 1)]
        [TestCase("9:00:59", "9:01:00", 1)]
        [TestCase("9:59:00", "10:00:01", 1)]
        [TestCase("9:00:00", "10:00:59", 60)]
        public void AllenKuoTest_CalcFee(string startValue, string endValue, int expected)
        {
            AssertMethod(startValue, endValue, expected);
        }

        [Test]
        [TestCase("13:10:59", "13:11:10", 1)]
        [TestCase("14:14:50", "17:52:10", 218)]
        public void ¦Û¦æ´ú¸Õ(string startValue, string endValue, int expected)
        {
            AssertMethod(startValue, endValue, expected);
        }
    }
}