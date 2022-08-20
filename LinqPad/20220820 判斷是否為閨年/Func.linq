<Query Kind="Program" />

void Main()
{
	IsLeap_delegate(1999).Dump("1999");
	IsLeap_delegate(2100).Dump("2100");
	IsLeap_delegate(2004).Dump("2004");
	IsLeap_delegate(2000).Dump("2000");
}

private bool IsLeap_delegate(int input)
{
	// 老師定義的閏年判斷規則
	Func<int, int, bool> 整除 = (input, 被除數) => (input % 被除數 == 0);
	Func<int, bool> 被四整除 = input => 整除(input, 4);
	Func<int, bool> 被一百整除 = input => 整除(input, 100);
	Func<int, bool> 被四百整除 = input => 整除(input, 400);

	List<Func<int, bool>> funcs = new List<Func<int, bool>>();
	funcs.Add(被四整除);
	funcs.Add(被一百整除);
	funcs.Add(被四百整除);

	// 只要任一整除條件為 false 就回傳 false，不會繼續往下驗證剩餘條件
	return funcs.All(f => f.Invoke(input));
}