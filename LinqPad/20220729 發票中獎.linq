<Query Kind="Program" />

void Main()
{
	/* Q1
		今天看到一個題目(其實有三個子題), 就自己練習了一下, 有興趣的也可以寫看看,先寫第一題
		比對發票是否中獎: 中獎號碼為 132, 672, 780, 981, 220; 您手邊的發票號碼為: 222,672, 119, 431, 901, 981, 002,811
		中獎結果是: 672, 981
	*/

	var bonusNO = new List<string>() { "132", "672", "780", "981", "220" };
	var recepitNO = new List<string>() { "222", "672", "119", "431", "901", "981", "002", "811" };
	bonusNO.Intersect(recepitNO).Dump("第一題結果");

	/* Q2
		比對發票是否中獎: 中獎號碼為 132, 672, 780, 981, 220; 您手邊的發票號碼為: 222 , 001 , 672 , 119 , 431 , 901 , 981 , 002 , 811
		中獎結果是: 672(第3筆), 981(第7筆)
	*/

	var bonusNO2 = new List<string>() { "132", "672", "780", "981", "220" };
	var recepitNO2 = new List<string>() { "222", "001", "672", "119", "431", "901", "981", "002", "811" };

	recepitNO2
		.Select((no, index) => new { NO = no, NOIndex = index })
		.Where(w => bonusNO2.Any(b => b == w.NO))
		.Select(r => $"{r.NO} ( 第 {r.NOIndex + 1} 筆 )")
		.Dump("第二題結果");

	/* Q3
		比對發票是否中獎, 號碼有五位數, 只要最後3,4,5碼相同,都算中獎:
		中獎號碼為 82132, 02672, 09780, 66981, 54321,00220; 假設末三碼都不會相同
		您手邊的發票號碼為:66222,81001,52672, 10119, 85431, 95901, 06981, 77002,54321,51811, 99672
		中獎結果是: 52672(第3筆, 中獎號碼是02672), 06981(第7筆, 中獎號碼是66981) , 54321(第9筆, 中獎號碼是54321) ,99672(第 11 筆, 中獎號碼是 02672)
	*/
	var bonusNO3 = new List<string>() { "82132", "02672", "09780", "66981", "54321", "00220" };
	var recepitNO3 = new List<string>() { "66222", "81001", "52672", "10119", "85431", "95901", "06981", "77002", "54321", "51811", "99672" };

	recepitNO3.Select((no, index) => new { NO = no, NOIndex = index })
		.Join(
			bonusNO3,
			r => r.NO.Substring(2, 3),
			b => b.Substring(2, 3),
			(r, b) => new { recepitNO = r.NO, recepitIndex = r.NOIndex, bonusNO = b })
		.Select(s => $"{s.recepitNO} ( 第 {s.recepitIndex + 1} 筆，中獎號碼為 {s.bonusNO} )")
		.Dump("第三題結果");
}



