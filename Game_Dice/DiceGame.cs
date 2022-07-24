namespace Game_Dice
{
    public class DiceGame
    {
        private const int diceNumbersLength = 4;
        public int PlayAndGetResult(int[] diceGame)
        {
            if (diceGame.Length != diceNumbersLength)
                throw new Exception("該骰子遊戲數字只能是四個");

            if ((diceGame.GroupBy(g => g).Count() == 4))
                throw new Exception($"骰子數字沒有重覆，不符合遊戲規則");

            var group = diceGame.GroupBy(no => no);

            // 四位數字都一樣
            if (groupNumber(4).Count() == 1)
                return diceGame.Sum() / 2;

            // 單一數字出現三次
            if (groupNumber(3).Any())
            {
                List<int> result = new List<int>();
                int no1 = groupNumber(3).First().Key;
                result.Add(no1);

                int no2 = diceGame.Except(new int[] { no1 }).Single();
                result.Add(no2);

                return result.Sum();
            }

            // 數字成對
            if (groupNumber(2).Count() == 1)
            {
                // 一組成對：加總非成對數字
                int no = groupNumber(2).Single().Key;
                return diceGame.Except(new int[] { no }).Sum();
            }
            else
            {
                // 兩組成對：取數字較大組別總和
                return groupNumber(2).Max(g => g.Key) * 2;
            }

            IEnumerable<IGrouping<int, int>> groupNumber(int numbers)
            {
                return group.Where(g => g.Count() == numbers);
            }
        }

        public PlayResult PlayAndGetResult()
        {
            int[] diceGame = PlayGame();

            PlayResult playResult = new PlayResult();
            playResult.DiceNumbers = diceGame;
            playResult.Total = PlayAndGetResult(diceGame);
            return playResult;
        }

        private int[] PlayGame()
        {
            int[] diceNumbers = CreateDiceNumbers();
            return DiceNumbersValid(diceNumbers);
        }

        private int[] CreateDiceNumbers()
        {
            int[] diceNumbers = new int[diceNumbersLength];
            int index = 0;
            diceNumbers[index++] = GetDiceNumber();
            diceNumbers[index++] = GetDiceNumber();
            diceNumbers[index++] = GetDiceNumber();
            diceNumbers[index++] = GetDiceNumber();

            return diceNumbers.OrderBy(o => o).ToArray();
        }

        private int[] DiceNumbersValid(int[] diceNumbers)
        {
            int[] diceNumbersRunning = diceNumbers;
            int count = 0;
            bool isDiceNumbersValid = false;
            while (isDiceNumbersValid == false)
            {
                isDiceNumbersValid = (diceNumbersRunning.GroupBy(g => g).Count() != 4);

                if (isDiceNumbersValid == false)
                {
                    count++;
                    if (count == 10)
                        break;

                    diceNumbersRunning = CreateDiceNumbers();
                }
            }

            return diceNumbersRunning;
        }

        private int GetDiceNumber()
        {
            int diceMin = 1;
            int diceMax = 6;
            Random r = new Random(Guid.NewGuid().GetHashCode());
            return r.Next(diceMin, diceMax);
        }
    }

    public class PlayResult
    {
        /// <summary>
        /// 總點數
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 四顆骰子的點數
        /// </summary>
        public int[] DiceNumbers { get; set; }
    }
}
