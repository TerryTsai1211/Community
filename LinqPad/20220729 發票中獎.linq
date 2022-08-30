<Query Kind="Program" />

void Main()
{
	#region Q1
	
	/* Q1
		今天看到一個題目(其實有三個子題), 就自己練習了一下, 有興趣的也可以寫看看,先寫第一題
		比對發票是否中獎: 中獎號碼為 132, 672, 780, 981, 220; 您手邊的發票號碼為: 222,672, 119, 431, 901, 981, 002,811
		中獎結果是: 672, 981
	*/

	var bonusNO = new List<string>() { "132", "672", "780", "981", "220" };
	var recepitNO = new List<string>() { "222", "672", "119", "431", "901", "981", "002", "811" };
	bonusNO.Intersect(recepitNO).Dump("第一題結果");
	
	#endregion

	#region Q2

	/* Q2
		比對發票是否中獎: 中獎號碼為 132, 672, 780, 981, 220; 您手邊的發票號碼為: 222 , 001 , 672 , 119 , 431 , 901 , 981 , 002 , 811
		中獎結果是: 672(第3筆), 981(第7筆)
	*/

	var bonusNO2 = new List<string>() { "132", "672", "780", "981", "220" };
	var recepitNO2 = new List<string>() { "222", "001", "672", "119", "431", "901", "981", "002", "811" };

	// A2-2：根據老師解法建立 InvoiceComparar 練習
	var bonusMapper = bonusNO2.Select((s, index) => new Invoice() { NOIndex = index, NO = s });
	var recepitMapper = recepitNO2.Select((s, index) => new Invoice() { NOIndex = index, NO = s });
	
	recepitMapper.Intersect(bonusMapper, new InvoiceComparar())
		.Select(r => r.ToString())
		.Dump("第二題結果");

	// A2-1：直接 Linq 求出
	recepitNO2
		.Select((no, index) => new { NO = no, NOIndex = index })
		.Where(w => bonusNO2.Any(b => b == w.NO))
		.Select(r => $"{r.NO} ( 第 {r.NOIndex + 1} 筆 )")
		.Dump("第二題結果");


	#endregion

	#region Q3

	/* Q3
		比對發票是否中獎, 號碼有五位數, 只要最後3,4,5碼相同,都算中獎:
		中獎號碼為 82132, 02672, 09780, 66981, 54321,00220; 假設末三碼都不會相同
		您手邊的發票號碼為:66222,81001,52672, 10119, 85431, 95901, 06981, 77002,54321,51811, 99672
		中獎結果是: 52672(第3筆, 中獎號碼是02672), 06981(第7筆, 中獎號碼是66981) , 54321(第9筆, 中獎號碼是54321) ,99672(第 11 筆, 中獎號碼是 02672)
	*/
	var bonusNO3 = new List<string>() { "82132", "02672", "09780", "66981", "54321", "00220" };
	var recepitNO3 = new List<string>() { "66222", "81001", "52672", "10119", "85431", "95901", "06981", "77002", "54321", "51811", "99672" };

	// A3-2：根據老師解法建立 LuckyInvoice 來處理，老師 C# 課程內也有該練習題，作法是先轉成 Invoice Class 在來進行處理
	recepitNO3.Select((no, index) => new { NO = no, NOIndex = index })
		.Join(
			bonusNO3,
			r => r.NO.Last3Value(),
			b => b.Last3Value(),
			(r, b) => new LuckyInvoice(r.NOIndex, r.NO, b))
		.Select(s => s.ToString())
		.Dump("第三題結果");

	// A3-1：直接 Linq 求出
	recepitNO3.Select((no, index) => new { NO = no, NOIndex = index })
		.Join(
			bonusNO3,
			r => r.NO.Substring(2, 3),
			b => b.Substring(2, 3),
			(r, b) => new { recepitNO = r.NO, recepitIndex = r.NOIndex, bonusNO = b })
		.Select(s => $"{s.recepitNO} ( 第 {s.recepitIndex + 1} 筆，中獎號碼為 {s.bonusNO} )")
		.Dump("第三題結果");

	#endregion

}


public class Invoice
{
	public int NOIndex { get; set; }

	public string NO { get; set; }

	public override string ToString()
	{
		return $"{NO} ( 第 {NOIndex + 1} 筆 )";
	}
}

public class InvoiceComparar : IEqualityComparer<Invoice>
{
	public bool Equals(Invoice x, Invoice y)
	{
		return x.NO == y.NO;
	}

	public int GetHashCode(Invoice obj)
	{
		return obj.NO.GetHashCode();
	}
}

public class LuckyInvoice
{
	private int _recepitNOIndex;
	private string _recepitNO;
	private string _bonusNO;

	public LuckyInvoice(int recepitNOIndex, string recepitNO, string bonusNO)
	{
		_recepitNOIndex = recepitNOIndex;
		_recepitNO = recepitNO;
		_bonusNO = bonusNO;
	}

	public override string ToString()
	{
		return $"{_recepitNO} ( 第 {_recepitNOIndex + 1} 筆，中獎號碼為 {_bonusNO} )";
	}
}

public static class StirngExtension
{
	public static string Last3Value(this string value)
	{
		return value.Substring(value.Length - 3, 3);
	}
}