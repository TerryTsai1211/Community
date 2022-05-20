using NUnit.Framework;
using System;
using System.Linq;

namespace Parking.Test
{
    [TestFixture]
    public class Q4Test
    {
        #region ParkingB

        private void ParkingBAssertMethod(string startValue, string endValue, int expectedTotalFee, int expectedDays)
        {
            DateTime start = Convert.ToDateTime(startValue);
            DateTime end = Convert.ToDateTime(endValue);

            var feeStat = new ParkingBFeeStat();
            var parking = new Parking(feeStat);

            var actual = parking.CalcParkingFee(start, end);
            Assert.AreEqual(expectedTotalFee, actual.TotalFee);
            Assert.AreEqual(expectedDays, actual.Items.Count());
        }

        // 平日
        [TestCase("2022/5/2 09:00:00", "2022/5/2 09:10:59", 0, 1)] // 同一天
        [TestCase("2022/5/2 09:00:00", "2022/5/2 09:11:59", 7, 1)] // 同一天
        [TestCase("2022/5/2 09:00:00", "2022/5/2 10:00:59", 10, 1)] // 同一天
        [TestCase("2022/5/2 23:49:00", "2022/5/3 00:10:59", 0, 2)] // 跨一天,免費
        [TestCase("2022/5/2 23:48:00", "2022/5/3 00:00:00", 7, 2)] // 跨一天,收費
        [TestCase("2022/5/2 23:48:00", "2022/5/3 00:11:59", 14, 2)] // 跨一天,收費
        [TestCase("2022/5/2 00:00:00", "2022/5/3 00:11:59", 57, 2)] // 跨一天,收費
        [TestCase("2022/5/2 23:49:00", "2022/5/4 00:11:59", 57, 3)] // 跨2天,收費
        [TestCase("2022/5/2 22:59:00", "2022/5/4 00:11:59", 67, 3)] // 跨2天,收費
        [TestCase("2022/5/2 00:00:00", "2022/5/4 00:11:59", 107, 3)] // 跨2天,收費
        // 假日
        [TestCase("2022/5/7 09:00:00", "2022/5/7 09:01:59", 15, 1)] // 1分鐘,15元
        [TestCase("2022/5/7 09:00:00", "2022/5/7 10:00:59", 15, 1)] // 60分鐘,15元
        [TestCase("2022/5/7 09:00:00", "2022/5/7 09:00:59", 0, 1)] // 0分, 0元
        [TestCase("2022/5/7 09:00:00", "2022/5/7 10:01:59", 30, 1)] // 61分, 30元
        [TestCase("2022/5/7 23:58:00", "2022/5/8 00:01:00", 30, 2)] // 跨一天,收費
        [TestCase("2022/5/7 00:00:00", "2022/5/8 00:11:59", 265, 2)] // 250 + 15
        public void AllenKuoTest_ParkingB(string startValue, string endValue, int expectedTotalFee, int expectedDays)
        {
            ParkingBAssertMethod(startValue, endValue, expectedTotalFee, expectedDays);
        }

        [Test]
        [TestCase("2022/5/8 23:58:00", "2022/5/9 00:10:00", 15, 2)]
        [TestCase("2022/5/8 23:58:00", "2022/5/9 00:11:00", 22, 2)]
        public void 水無痕Test_ParkingB_假日_跨_平日(string startValue, string endValue, int expectedTotalFee, int expectedDays)
        {
            ParkingBAssertMethod(startValue, endValue, expectedTotalFee, expectedDays);
        }

        [Test]
        [TestCase("2022/5/6 23:48:00", "2022/5/7 00:01:00", 22, 2)]
        [TestCase("2022/5/6 23:59:00", "2022/5/7 00:01:00", 15, 2)]
        public void 水無痕Test_ParkingB_平日_跨_假日(string startValue, string endValue, int expectedTotalFee, int expectedDays)
        {
            ParkingBAssertMethod(startValue, endValue, expectedTotalFee, expectedDays);
        }

        #endregion

        #region ParkingC

        private void ParkingCAssertMethod(string startValue, string endValue, int expectedTotalFee, int expectedDays)
        {
            DateTime start = Convert.ToDateTime(startValue);
            DateTime end = Convert.ToDateTime(endValue);

            var feeStat = new ParkingCFeeStat();
            var parking = new Parking(feeStat);

            var actual = parking.CalcParkingFee(start, end);
            Assert.AreEqual(expectedTotalFee, actual.TotalFee);
            Assert.AreEqual(expectedDays, actual.Items.Count());
        }

        // 平日
        [TestCase("2022/5/2 09:00:00", "2022/5/2 10:00:59", 20, 1)] // 同一天, 第一小時為20元
        [TestCase("2022/5/2 09:00:00", "2022/5/2 10:01:00", 50, 1)] // 同一天, 61分,第一小時20,第二小時起,每小時30
        [TestCase("2022/5/2 09:00:00", "2022/5/2 11:01:00", 80, 1)] // 2小又1分,第一小時20,第二小時起,每小時30
        [TestCase("2022/5/2 09:00:00", "2022/5/2 12:01:00", 110, 1)] // 3小又1分,第一小時20,第二小時起,每小時30
        [TestCase("2022/5/2 23:48:00", "2022/5/3 00:00:00", 20, 2)] // 跨一天,20 + 0
        [TestCase("2022/5/2 23:48:00", "2022/5/3 00:11:59", 40, 2)] // 跨一天,20 + 20
        [TestCase("2022/5/2 00:00:00", "2022/5/3 01:01:00", 350, 2)] // 跨一天,第二天1小時又1分, 300 + 50
        [TestCase("2022/5/2 23:49:00", "2022/5/4 00:11:59", 340, 3)] // 跨2天,20 + 300 + 20
        // 假日
        [TestCase("2022/5/7 00:00:00", "2022/5/7 23:59:59", 0, 1)] // 1分鐘,假日免費
        [TestCase("2022/5/7 00:00:00", "2022/5/8 23:59:59", 0, 2)] // 跨一天,假日免費
        public void AllenKuoTest_ParkingC(string startValue, string endValue, int expectedTotalFee, int expectedDays)
        {
            ParkingCAssertMethod(startValue, endValue, expectedTotalFee, expectedDays);
        }


        [Test]
        [TestCase("2022/5/8 23:58:00", "2022/5/9 00:01:00", 20, 2)]
        public void 水無痕Test_ParkingC_假日_跨_平日(string startValue, string endValue, int expectedTotalFee, int expectedDays)
        {
            ParkingCAssertMethod(startValue, endValue, expectedTotalFee, expectedDays);
        }

        [Test]
        [TestCase("2022/5/6 23:58:00", "2022/5/7 00:01:00", 20, 2)]
        public void 水無痕Test_ParkingC_平日_跨_假日(string startValue, string endValue, int expectedTotalFee, int expectedDays)
        {
            ParkingCAssertMethod(startValue, endValue, expectedTotalFee, expectedDays);
        }

        [Test]
        [TestCase("2022/5/6 00:00:00.0000000", "2022/5/6 00:00:00.0000001", 0, 1)]
        public void 水無痕Test_ParkingC_同分不同秒(string startValue, string endValue, int expectedTotalFee, int expectedDays)
        {
            // 這案例不知道是該算 1 天還是 0 天說

            ParkingCAssertMethod(startValue, endValue, expectedTotalFee, expectedDays);
        }

        #endregion
    }
}
