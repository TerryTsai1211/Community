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
            BaseDiceNumbersHandler handler = new 四位數字都一樣Handler(new 單一數字出現三次Handler(new 數字成對一組成對Handler(new 數字成對二組成對Handler(null))));
            return handler.HandleRequest(diceNumbers);
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
