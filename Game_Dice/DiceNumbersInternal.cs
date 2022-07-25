namespace Game_Dice
{
    public class DiceNumbersInternal : IDiceNumbers
    {
        public int[] GetDiceNumbers()
        {
            int[] diceNumbers = new int[4];
            int index = 0;
            diceNumbers[index++] = GetRandomNumber();
            diceNumbers[index++] = GetRandomNumber();
            diceNumbers[index++] = GetRandomNumber();
            diceNumbers[index++] = GetRandomNumber();

            return diceNumbers.OrderBy(o => o).ToArray();
        }

        private int GetRandomNumber()
        {
            /*
             * 老師提醒
             * return r.Next(diceMin, diceMax); 這應該是錯的, 因為你傳入1, 6, 它其實是傳回 [1, 6)
             * 
             * 官方文件說明
             * A 32-bit signed integer greater than or equal to minValue and less than maxValue; 
             * that is, the range of return values includes minValue but not maxValue. 
             * If minValue equals maxValue, minValue is returned.
             */

            int diceMin = 1;
            int diceMax = 7;
            Random r = new Random(Guid.NewGuid().GetHashCode());
            return r.Next(diceMin, diceMax);
        }
    }
}
