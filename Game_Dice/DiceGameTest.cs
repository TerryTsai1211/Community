namespace Game_Dice
{
    public class DiceGameTest
    {

        [Test]
        public void 手動測試觀察結果()
        {
            DiceGame gameDemo = new DiceGame();
            var result = gameDemo.PlayAndGetResult();

            foreach (int number in result.DiceNumbers)
                TestContext.WriteLine($"數字：{number}");

            TestContext.WriteLine($"總和：{result.Total}");
        }

        [Test]
        [TestCase(1, 1, 1, 1, 2)]
        [TestCase(2, 2, 2, 2, 4)]
        [TestCase(3, 3, 3, 3, 6)]
        [TestCase(4, 4, 4, 4, 8)]
        [TestCase(5, 5, 5, 5, 10)]
        [TestCase(6, 6, 6, 6, 12)]
        public void 四位數字都一樣(int no1, int no2, int no3, int no4, int expected)
        {
            AssertMethod(no1, no2, no3, no4, expected);
        }

        [Test]
        [TestCase(1, 1, 1, 6, 7)]
        [TestCase(2, 2, 2, 6, 8)]
        [TestCase(3, 3, 3, 6, 9)]
        [TestCase(4, 4, 4, 6, 10)]
        [TestCase(5, 5, 5, 6, 11)]
        public void 單一數字出現三次(int no1, int no2, int no3, int no4, int expected)
        {
            AssertMethod(no1, no2, no3, no4, expected);
        }

        [Test]
        [TestCase(1, 1, 2, 3, 5)]
        [TestCase(2, 2, 3, 4, 7)]
        [TestCase(3, 3, 4, 5, 9)]
        [TestCase(4, 4, 5, 6, 11)]
        public void 數字成對_一組(int no1, int no2, int no3, int no4, int expected)
        {
            AssertMethod(no1, no2, no3, no4, expected);
        }

        [Test]
        [TestCase(1, 1, 2, 2, 4)]
        [TestCase(2, 2, 3, 3, 6)]
        [TestCase(3, 3, 4, 4, 8)]
        [TestCase(4, 4, 5, 5, 10)]
        [TestCase(5, 5, 6, 6, 12)]
        public void 數字成對_二組(int no1, int no2, int no3, int no4, int expected)
        {
            AssertMethod(no1, no2, no3, no4, expected);
        }

        private void AssertMethod(int no1, int no2, int no3, int no4, int expected)
        {
            IDiceNumbers diceNumbers = Substitute.For<IDiceNumbers>();
            diceNumbers.GetDiceNumbers().Returns(new int[] { no1, no2, no3, no4 });

            var diceGame = new DiceGame(diceNumbers);
            var actual = diceGame.PlayAndGetResult();
            Assert.IsTrue(expected == actual.Total);
        }
    }
}