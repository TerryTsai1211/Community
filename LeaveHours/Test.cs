using NUnit.Framework;

namespace LeaveHours
{
    public class Tests
    {
        private LeaveService leaveService { get; set; }

        [SetUp]
        public void SetUp()
        {
            leaveService = new LeaveService();
        }

        [Test]
        [TestCase(9, 18, 8)]
        [TestCase(9, 17, 7)]
        [TestCase(9, 12, 3)]
        [TestCase(9, 13, 3)]
        [TestCase(12, 14, 1)]
        [TestCase(8, 18, 8)]
        [TestCase(9, 23, 8)]
        public void CalcTest(int startHour, int endHour, int expected)
        {
            int actual = leaveService.CalcTotalLeaveHours(startHour, endHour);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(9, 18, 8)]
        [TestCase(9, 17, 7)]
        [TestCase(9, 12, 3)]
        [TestCase(9, 13, 3)]
        [TestCase(12, 14, 1)]
        [TestCase(8, 18, 8)]
        [TestCase(9, 23, 8)]
        public void LinqTest(int startHour, int endHour, int expected)
        {
            int actual = leaveService.CalcTotalLeaveHoursByLinq(startHour, endHour);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}