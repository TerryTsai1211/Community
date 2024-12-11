namespace Bill
{
    public static class BillCalculatorHelper
    {
        private static (int 總費用含小費, int 每個人平均費用, int 剩餘費用) 費用計算(int totalAmount, decimal tipRate, int numberOfPeople)
        {
            int 總費用含小費 = totalAmount + (int)Math.Round(totalAmount * (tipRate / 100), MidpointRounding.AwayFromZero);
            int 每個人平均費用 = (總費用含小費 / numberOfPeople);
            int 剩餘費用 = 總費用含小費 % numberOfPeople;

            return (總費用含小費, 每個人平均費用, 剩餘費用);
        }

        public static IEnumerable<int> 最後一個人多出錢(int totalAmount, decimal tipRate, int numberOfPeople)
        {
            var cal = 費用計算(totalAmount, tipRate, numberOfPeople);

            // 使用 Linq 來產生
            List<int> result = Enumerable.Repeat((int)cal.每個人平均費用, numberOfPeople).ToList();

            if (cal.剩餘費用 > 0)
                result[numberOfPeople - 1] += cal.剩餘費用;

            return result;
        }

        public static IEnumerable<int> 前面N個人多出一元(int totalAmount, decimal tipRate, int numberOfPeople)
        {
            var cal = 費用計算(totalAmount, tipRate, numberOfPeople);

            // 使用迴圈來產生
            List<int> result = new List<int>();
            for (int i = 0; i < numberOfPeople; i++)
                result.Add((int)cal.每個人平均費用);

            if (cal.剩餘費用 > 0)
            {
                for (int i = 0; i < cal.剩餘費用; i++)
                    result[i] += 1;
            }

            return result;
        }
    }
}