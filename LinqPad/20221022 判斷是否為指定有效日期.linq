<Query Kind="Program" />

void Main()
{

	/* Q：請使用者輸入一個日期, 判斷它是否介於 2 / 3 ~5 / 13 之間,不拘年份 */

	// string userInput = "2022/02/29";
	// string userInput = "2022/02/01";
	string userInput = "2022/04/30";

	bool isInputValid = DateTime.TryParse(userInput , out DateTime dt);
	if (isInputValid == false)
	{
		"不是有效日期".Dump();
		return;
	}
	
	int yy = dt.Year;
	DateTime validStart = new DateTime(yy , 2 , 3);
	DateTime validEnd = new DateTime(yy , 5 , 13);
	
	if (dt >= validStart && dt <= validEnd)
		"使用者輸入日期在指定範圍內".Dump();
	else 
		"使用者輸入日期不在指定範圍內".Dump();
	
}

