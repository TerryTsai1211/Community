namespace Game_Dice
{
    public class DiceGame
    {
        private readonly IDiceNumbers _diceNumbers;

        /// <summary>
        /// 直接玩遊戲
        /// </summary>
        public DiceGame()
        {
            _diceNumbers = new DiceNumbersInternal();
        }

        /// <summary>
        /// 寫測試用，需從外部直接 mock
        /// </summary>
        /// <param name="diceNumbers"></param>
        public DiceGame(IDiceNumbers diceNumbers)
        {
            _diceNumbers = diceNumbers;
        }

        public PlayResult PlayAndGetResult()
        {
            int[] diceNumbers = DiceNumbersValid(_diceNumbers.GetDiceNumbers());

            PlayResult playResult = new PlayResult();
            playResult.DiceNumbers = diceNumbers;
            playResult.Total = ScoreCal(diceNumbers);
            return playResult;
        }

        private int ScoreCal(int[] diceNumbers)
        {
            var group = diceNumbers.GroupBy(no => no);

            // 四位數字都一樣
            if (GroupNumber(4).Count() == 1)
                return diceNumbers.Sum() / 2;

            // 單一數字出現三次
            if (GroupNumber(3).Any())
            {
                List<int> result = new List<int>();
                int no1 = GroupNumber(3).First().Key;
                result.Add(no1);

                int no2 = diceNumbers.Except(new int[] { no1 }).Single();
                result.Add(no2);

                return result.Sum();
            }

            // 數字成對
            if (GroupNumber(2).Count() == 1)
            {
                // 一組成對：加總非成對數字
                int no = GroupNumber(2).Single().Key;
                return diceNumbers.Except(new int[] { no }).Sum();
            }
            else
            {
                // 兩組成對：取數字較大組別總和
                return GroupNumber(2).Max(g => g.Key) * 2;
            }

            IEnumerable<IGrouping<int, int>> GroupNumber(int number)
            {
                return group.Where(g => g.Count() == number);
            }
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
                        throw new Exception("遊戲中止");

                    diceNumbersRunning = _diceNumbers.GetDiceNumbers();
                }
            }

            return diceNumbersRunning;
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
