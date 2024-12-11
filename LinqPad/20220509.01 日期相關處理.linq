<Query Kind="Program" />

void Main()
{
	#region 日期函數相關

	// 1：取得今天日期
	DateTime.Today.Dump("Q1：今天日期");

	// 2：取得現在的時間
	DateTime.Now.Dump("Q2：現在時間");

	// 7：判斷今年是不是閏年
	DateTime.IsLeapYear(DateTime.Today.Year).Dump("Q7：今年是不是閏年");
	#endregion

	#region 日月年日期相關

	// 5：計算出本月一日是哪一天	
	new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).Dump("Q5：本月第一天");

	// 10：計算本年最後一天是哪一天
	var NextYear = DateTime.Today.AddYears(1);
	(new DateTime(NextYear.Year, 1, 1)).AddDays(-1).Dump("Q10：本年最後一天");

	#endregion

	#region 日月年日期相關 - 計算邏輯相關

	// 9：計算出下個月一日是哪一天
	var DateTimeOfNextMonth = DateTime.Today.AddMonths(1);
	DateTime 下個月一號 = new DateTime(DateTimeOfNextMonth.Year, DateTimeOfNextMonth.Month, 1).Dump("Q9：下個月一號");

	// 8：計算出本月最後一天是哪一天
	DateTime 本月最後一天 = 下個月一號.AddDays(-1).Dump("Q8：本月最後一天");

	// 6：計算本月一共有幾天
	int days = 本月最後一天.Day.Dump("Q6：本月一共有幾天");

	#endregion

	#region 星期相關

	// 3：取得今天是星期幾
	DateTime.Today.DayOfWeek.Dump("Q3：今天星期幾");

	// 4：假設一星期的第一天是星期日,請寫程式求得本星期的第一天
	 int deleteDays = ((int)DateTime.Today.DayOfWeek % 7) * -1;
	 DateTime.Today.AddDays(deleteDays).Dump("Q4：本星期第一天");

	// 11：取得 2022 年裡,每一個星期日的記錄, 傳回型別是 DateTime[]
	DateTime 今年第一天 = new DateTime(2022, 1, 1); // 偷懶直寫死
	DateTime 今年第一週日 = 年度第一個周日(今年第一天);

	DateTime 明年第一天 = new DateTime(2023, 1, 1); // 偷懶直寫死
	DateTime 明年第一週日 = 年度第一個周日(明年第一天);

	int sundayCount = ((int)(明年第一週日.Subtract(今年第一週日).TotalDays) / 7);
	
	Enumerable.Range(0, sundayCount)
		.Select(offset => 今年第一週日.AddDays(offset * 7))
		.Select(dt => (dt.ToString("yyyy/MM/dd") , dt.DayOfWeek))
		.Dump("Q11：2022 年每一個星期日");

	#endregion
}

private DateTime 年度第一個周日(DateTime 每年第一天)
{
	int 一月 = 1;
	int 一號 = 1;
	
	if (每年第一天.Month ==  一月 && 每年第一天.Day == 一號)
		throw new Exception("不是該年第一天");
	
	if (每年第一天.DayOfWeek == DayOfWeek.Sunday)
		return 每年第一天;

	int 一周有七天 = 7;
	int addDays = 一周有七天 - (int)每年第一天.DayOfWeek;
	return 每年第一天.AddDays(addDays);
}