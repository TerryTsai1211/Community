using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    [TestFixture]
    public class Q3Test
    {
        private ParkingFeeBiz biz { get; set; }

        [SetUp]
        public void SetUp()
        {
            biz = new ParkingFeeBiz();
        }

        #region Q3 標準
        [TestCase("2022/5/1 09:00:00", "2022/5/1 09:10:59", 0, 1)] // 同一天
        [TestCase("2022/5/1 09:00:00", "2022/5/1 09:11:59", 7, 1)] // 同一天
        [TestCase("2022/5/1 09:00:00", "2022/5/1 10:00:59", 10, 1)] // 同一天
        [TestCase("2022/5/1 23:49:00", "2022/5/2 00:10:59", 0, 2)] // 跨一天,免費
        [TestCase("2022/5/1 23:48:00", "2022/5/2 00:00:00", 7, 2)] // 跨一天,收費
        [TestCase("2022/5/1 23:48:00", "2022/5/2 00:11:59", 14, 2)] // 跨一天,收費
        [TestCase("2022/5/1 00:00:00", "2022/5/2 00:11:59", 57, 2)] // 跨一天,收費
        [TestCase("2022/5/1 23:49:00", "2022/5/3 00:11:59", 57, 3)] // 跨2天,收費
        [TestCase("2022/5/1 22:59:00", "2022/5/3 00:11:59", 67, 3)] // 跨2天,收費
        [TestCase("2022/5/1 00:00:00", "2022/5/3 00:11:59", 107, 3)] // 跨2天,收費
        public void AllenKuoTest(string startValue, string endValue, int expectedFee, int expectedDays)
        {
            DateTime start = DateTime.Parse(startValue);
            DateTime end = DateTime.Parse(endValue);

            var actual = biz.CalcFeeForMultiDays_STD(start, end);

            Assert.AreEqual(expectedFee, actual.Sum(x => x.Fee));
            Assert.AreEqual(expectedDays, actual.Count());
        }

        [Test]
        [TestCase("2022-5-2", "2022-5-1")]
        [TestCase("2022-5-1 00:01:00", "2022-5-1 00:00:00")]
        public void 結束日期早於起始日期_Exception(string startValue, string endValue)
        {
            DateTime start = Convert.ToDateTime(startValue);
            DateTime end = Convert.ToDateTime(endValue);

            var ex = Assert.Throws<Exception>(() => biz.CalcFeeForMultiDays_STD(start, end));
            StringAssert.Contains("早於", ex.Message);
        }

        [Test]
        [TestCase("2022/5/1 00:00:00", "2022/5/2 00:11:59")] // 57
        public void 跨一天_Case1(string startValue, string endValue)
        {
            DateTime start = Convert.ToDateTime(startValue);
            DateTime end = Convert.ToDateTime(endValue);

            var actual = biz.CalcFeeForMultiDays_STD(start, end).ToList();

            var expected = new List<SingleDayFee>()
            {
                new SingleDayFee() {
                    StartTime = new DateTime(2022,5,1,0,0,0) ,
                    EndTime = new DateTime(2022,5,1,23,59,0) ,
                    Fee = 50 },
                new SingleDayFee() {
                    StartTime = new DateTime(2022,5,2,0,0,0) ,
                    EndTime = new DateTime(2022,5,2,0,11,59) ,
                    Fee = 7 }

            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("2022/5/1 23:48:00", "2022/5/2 00:00:00")] // 7
        public void 跨一天_Case2(string startValue, string endValue)
        {
            DateTime start = Convert.ToDateTime(startValue);
            DateTime end = Convert.ToDateTime(endValue);

            var actual = biz.CalcFeeForMultiDays_STD(start, end).ToList();

            var expected = new List<SingleDayFee>()
            {
                new SingleDayFee() {
                    StartTime = new DateTime(2022,5,1,23,48,0) ,
                    EndTime = new DateTime(2022,5,1,23,59,0) ,
                    Fee = 7 },
                new SingleDayFee() {
                    StartTime = new DateTime(2022,5,2,0,0,0) ,
                    EndTime = new DateTime(2022,5,2,0,0,0) ,
                    Fee = 0 }

            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("2022/5/1 23:48:00", "2022/5/2 00:11:59")] // 14
        public void 跨一天_Case3(string startValue, string endValue)
        {
            DateTime start = Convert.ToDateTime(startValue);
            DateTime end = Convert.ToDateTime(endValue);

            var actual = biz.CalcFeeForMultiDays_STD(start, end).ToList();

            var expected = new List<SingleDayFee>()
            {
                new SingleDayFee() {
                    StartTime = new DateTime(2022,5,1,23,48,0) ,
                    EndTime = new DateTime(2022,5,1,23,59,0) ,
                    Fee = 7 },
                new SingleDayFee() {
                    StartTime = new DateTime(2022,5,2,0,0,0) ,
                    EndTime = new DateTime(2022,5,2,0,11,59) ,
                    Fee = 7 }

            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("2022/5/1 23:49:00", "2022/5/2 00:10:59")] // 0
        public void 跨一天_時間極短_0(string startValue, string endValue)
        {
            DateTime start = Convert.ToDateTime(startValue);
            DateTime end = Convert.ToDateTime(endValue);

            var actual = biz.CalcFeeForMultiDays_STD(start, end).ToList();

            var expected = new List<SingleDayFee>()
            {
                new SingleDayFee() {
                    StartTime = new DateTime(2022,5,1,23,49,0) ,
                    EndTime = new DateTime(2022,5,1,23,59,0) ,
                    Fee = 0 },
                new SingleDayFee() {
                    StartTime = new DateTime(2022,5,2,0,0,0) ,
                    EndTime = new DateTime(2022,5,2,0,10,59) ,
                    Fee = 0 }

            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("2022/5/1 00:00:00", "2022/5/3 00:11:59")] // 107
        public void 跨兩天_Case1(string startValue, string endValue)
        {
            DateTime start = Convert.ToDateTime(startValue);
            DateTime end = Convert.ToDateTime(endValue);

            var actual = biz.CalcFeeForMultiDays_STD(start, end).ToList();

            var expected = new List<SingleDayFee>()
            {
                new SingleDayFee() {
                    StartTime = new DateTime(2022,5,1,0,0,0) ,
                    EndTime = new DateTime(2022,5,1,23,59,0) ,
                    Fee = 50 },
                new SingleDayFee() {
                    StartTime = new DateTime(2022,5,2,0,0,0) ,
                    EndTime = new DateTime(2022,5,2,23,59,0) ,
                    Fee = 50 },
                new SingleDayFee() {
                    StartTime = new DateTime(2022,5,3,0,0,0) ,
                    EndTime = new DateTime(2022,5,3,0,11,59) ,
                    Fee = 7 }

            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase("2022/5/1 22:59:00", "2022/5/3 00:11:59")] // 67
        public void 跨兩天_Case2(string startValue, string endValue)
        {
            DateTime start = Convert.ToDateTime(startValue);
            DateTime end = Convert.ToDateTime(endValue);

            var actual = biz.CalcFeeForMultiDays_STD(start, end).ToList();

            var expected = new List<SingleDayFee>()
            {
                new SingleDayFee() {
                    StartTime = new DateTime(2022,5,1,22,59,0) ,
                    EndTime = new DateTime(2022,5,1,23,59,0) ,
                    Fee = 10 },
                new SingleDayFee() {
                    StartTime = new DateTime(2022,5,2,0,0,0) ,
                    EndTime = new DateTime(2022,5,2,23,59,0) ,
                    Fee = 50 },
                new SingleDayFee() {
                    StartTime = new DateTime(2022,5,3,0,0,0) ,
                    EndTime = new DateTime(2022,5,3,0,11,59) ,
                    Fee = 7 }

            };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase("2022/5/1 23:49:00", "2022/5/3 00:11:59")] // 57
        public void 跨兩天_Case3(string startValue, string endValue)
        {
            DateTime start = Convert.ToDateTime(startValue);
            DateTime end = Convert.ToDateTime(endValue);

            var actual = biz.CalcFeeForMultiDays_STD(start, end).ToList();

            var expected = new List<SingleDayFee>()
            {
                new SingleDayFee() {
                    StartTime = new DateTime(2022,5,1,23,49,0) ,
                    EndTime = new DateTime(2022,5,1,23,59,0) ,
                    Fee = 0 },
                new SingleDayFee() {
                    StartTime = new DateTime(2022,5,2,0,0,0) ,
                    EndTime = new DateTime(2022,5,2,23,59,0) ,
                    Fee = 50 },
                new SingleDayFee() {
                    StartTime = new DateTime(2022,5,3,0,0,0) ,
                    EndTime = new DateTime(2022,5,3,0,11,59) ,
                    Fee = 7 }

            };

            CollectionAssert.AreEqual(expected, actual);
        } 
        #endregion


    }
}
