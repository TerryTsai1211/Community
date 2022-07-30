namespace Game_Dice
{
    public abstract class BaseDiceNumbersHandler
    {
        private BaseDiceNumbersHandler _successor;

        public BaseDiceNumbersHandler(BaseDiceNumbersHandler baseDiceNumbersHandler)
        {
            _successor = baseDiceNumbersHandler;
        }

        protected abstract bool CanHandle(int[] diceNumbers);

        protected abstract int ScoreCal(int[] diceNumbers);

        public int HandleRequest(int[] diceNumbers)
        {
            if (CanHandle(diceNumbers))
                return ScoreCal(diceNumbers);

            return _successor.HandleRequest(diceNumbers);
        }

        protected IEnumerable<IGrouping<int, int>> GroupNumber(int[] diceNumbers, int number)
        {
            return diceNumbers.GroupBy(n => n).Where(g => g.Count() == number);
        }
    }

    public class 四位數字都一樣Handler : BaseDiceNumbersHandler
    {
        public 四位數字都一樣Handler(BaseDiceNumbersHandler nextBaseRule) : base(nextBaseRule)
        {
        }

        protected override bool CanHandle(int[] diceNumbers)
        {
            return (GroupNumber(diceNumbers, 4).Count() == 1);
        }

        protected override int ScoreCal(int[] diceNumbers)
        {
            return diceNumbers.Sum() / 2;
        }
    }

    public class 單一數字出現三次Handler : BaseDiceNumbersHandler
    {
        public 單一數字出現三次Handler(BaseDiceNumbersHandler nextBaseRule) : base(nextBaseRule)
        {
        }

        protected override bool CanHandle(int[] diceNumbers)
        {
            return GroupNumber(diceNumbers, 3).Any();
        }

        protected override int ScoreCal(int[] diceNumbers)
        {
            List<int> result = new List<int>();
            int no1 = GroupNumber(diceNumbers, 3).First().Key;
            result.Add(no1);

            int no2 = diceNumbers.Except(new int[] { no1 }).Single();
            result.Add(no2);

            return result.Sum();
        }
    }

    public class 數字成對一組成對Handler : BaseDiceNumbersHandler
    {
        public 數字成對一組成對Handler(BaseDiceNumbersHandler nextBaseRule) : base(nextBaseRule)
        {
        }

        protected override bool CanHandle(int[] diceNumbers)
        {
            return GroupNumber(diceNumbers, 2).Count() == 1;
        }

        protected override int ScoreCal(int[] diceNumbers)
        {
            // 一組成對：加總非成對數字
            int no = GroupNumber(diceNumbers, 2).Single().Key;
            return diceNumbers.Except(new int[] { no }).Sum();
        }
    }

    public class 數字成對二組成對Handler : BaseDiceNumbersHandler
    {
        public 數字成對二組成對Handler(BaseDiceNumbersHandler nextBaseRule) : base(nextBaseRule)
        {
        }

        protected override bool CanHandle(int[] diceNumbers)
        {
            return GroupNumber(diceNumbers, 2).Count() == 2;

        }

        protected override int ScoreCal(int[] diceNumbers)
        {
            return GroupNumber(diceNumbers, 2).Max(g => g.Key) * 2;
        }
    }

}
