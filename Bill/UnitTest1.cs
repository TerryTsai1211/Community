namespace Bill
{
    public class Tests
    {
        private Dictionary<string, Func<int, decimal, int, IEnumerable<int>>> _delegateFactory { get; set; } = new Dictionary<string, Func<int, decimal, int, IEnumerable<int>>>();

        [OneTimeSetUp]
        public void Setup()
        {
            _delegateFactory.Add(nameof(BillCalculatorHelper.最後一個人多出錢), (totalAmount, tipRate, numberOfPeople) => BillCalculatorHelper.最後一個人多出錢(totalAmount, tipRate, numberOfPeople));
            _delegateFactory.Add(nameof(BillCalculatorHelper.前面N個人多出一元), (totalAmount, tipRate, numberOfPeople) => BillCalculatorHelper.前面N個人多出一元(totalAmount, tipRate, numberOfPeople));
        }

        [Test]
        [TestCase(1000, 10, 4, 275)]
        [TestCase(1200, 12, 8, 168)]
        public void Q1_Interface(int totalAmount, decimal tipRate, int numberOfPeople, int expected)
        {
            Q1 cal = new Q1();
            List<int> result = cal.CalculateSplitAmount(totalAmount, tipRate, numberOfPeople).ToList();

            bool actual = (result.Count(x => x == expected) == numberOfPeople);
            Assert.IsTrue(actual);
        }

        [Test]
        public void Q2_Interface()
        {
            Q2 cal = new Q2();
            List<int> result = cal.CalculateSplitAmount(1000, 10, 3).ToList();
            int pay366 = result.Count(x => x == 366);
            int pay368 = result.Count(x => x == 368);
            bool actual = (pay366 == 2 && pay368 == 1);

            Assert.IsTrue(actual);
        }

        [Test]
        public void Q3_Interface()
        {
            Q3 cal = new Q3();
            List<int> result = cal.CalculateSplitAmount(1000, 10, 3).ToList();
            int pay367 = result.Count(x => x == 367);
            int pay366 = result.Count(x => x == 366);
            bool actual = (pay367 == 2 && pay366 == 1);

            Assert.IsTrue(actual);
        }

        [Test]
        public void Q2_Delegate()
        {
            var value = _delegateFactory[nameof(BillCalculatorHelper.最後一個人多出錢)];
            var result = value.Invoke(1000, 10, 3);
            int pay366 = result.Count(x => x == 366);
            int pay368 = result.Count(x => x == 368);
            bool actual = (pay366 == 2 && pay368 == 1);

            Assert.IsTrue(actual);
        }

        [Test]
        public void Q3_Delegate()
        {
            var value = _delegateFactory[nameof(BillCalculatorHelper.前面N個人多出一元)];
            var result = value.Invoke(1000, 10, 3);

            int pay367 = result.Count(x => x == 367);
            int pay366 = result.Count(x => x == 366);
            bool actual = (pay367 == 2 && pay366 == 1);

            Assert.IsTrue(actual);
        }
    }
}