namespace Bill
{
    /*
     * 有一群好朋友去餐廳吃飯, 請幫他們計算
     * 用餐金額 + 小費
     * 每人平均分攤各多少錢
     */

    /// <summary>
    /// 第一階段：
    /// console Application
    /// 操作說明：
    /// 歡迎使用小費計算器
    /// 請輸入總金額？ 1000
    /// 請問幾個人要分攤？ 4
    /// 你想付多少趴的小費？例如 10%，就輸入10: 10
    /// 應付金額是 1,000 + 100 = 1,100
    /// 每人要分攤：275 元
    /// 註: 
    /// 輸入金額時，請尾數二個零，例如 100、1200、3500，方便分攤時可以整除
    /// 輸入人數時，請輸入偶數，方便整除
    /// 輸入小費時，請輸入 10、12 或 15，分別表示 10%、12%、15% 小費
    /// 本題不考慮除不盡的問題
    /// </summary>
    public class Q1 : IBillCalculator
    {
        public IEnumerable<int> CalculateSplitAmount(int totalAmount, decimal tipRate, int numberOfPeople)
        {
            int 總費用含小費 = totalAmount + (int)Math.Round(totalAmount * (tipRate / 100), MidpointRounding.AwayFromZero);
            int 每個人費用 = (總費用含小費 / numberOfPeople);

            List<int> result = new List<int>();
            for (int i = 0; i < numberOfPeople; i++)
            {
                result.Add((int)每個人費用);
            }

            return result;
        }
    }

    /// <summary>
    /// 第二階段：
    /// console Application
    /// 功能大致與上一階段相同，但外加以下功能
    /// 判斷輸入的總金額、小費、人數必需是正整數，如果有誤，就反覆請使用者重新輸入該項數值，直到正確為止
    /// 如果除不盡，最後一個人要付比較多錢
    /// 操作說明:
    /// 歡迎使用小費計算器
    /// 請輸入總金額？ 1000
    /// 請問幾個人要分攤？ 3
    /// 你想付多少趴的小費？ 例如：10%，就輸入10: 10
    /// 應付金額是 1,000 + 100 = 1,100
    /// 前 2 人，每人要分攤： 366 元
    /// 最後 1 人要分攤 368 元
    /// </summary>
    public class Q2 : IBillCalculator
    {
        public IEnumerable<int> CalculateSplitAmount(int totalAmount, decimal tipRate, int numberOfPeople)
        {
            return BillCalculatorHelper.最後一個人多出錢(totalAmount, tipRate, numberOfPeople);
        }
    }

    /// <summary>
    /// 第三階段：
    /// Windows Forms
    /// 功能大致與上一階段相同，但外加以下功能
    /// 判斷輸入的總金額、小費、人數必需是正整數，如果有誤，就用 MessageBox 顯示錯誤訊息，直到全部欄位值都填寫正確，才開始計算
    /// 如果除不盡，餘數 N，則前 N 個人都多出一元
    /// 顯示時，用 TextBox 明確列出每一個人應付金額
    /// 第 1 人，應付 367 元
    /// 第 2 人，應付 367 元
    /// 第 3 人，應付 366 元
    /// </summary>
    public class Q3 : IBillCalculator
    {
        // 傳入參數: 帳單總金額, 小費比率, 總用餐人數
        // 回傳值: IEnumerable<int> 列出每一個人應分攤多少錢
        public IEnumerable<int> CalculateSplitAmount(int totalAmount, decimal tipRate, int numberOfPeople)
        {
            return BillCalculatorHelper.前面N個人多出一元(totalAmount, tipRate, numberOfPeople);
        }
    }


}
