<Query Kind="Program" />

void Main()
{
	// 宣告一個 [2, 4] 的二維陣列
	int[,] items = new int[,]
	{
		{0,1,2,3},
		{4,5,6,7},
	};

	bool result = IsIn(items, 0, 1); // true
	Console.WriteLine(result);

	result = IsIn(items, 0, 5); // false
	Console.WriteLine(result);

	result = IsIn(items, 2, 0); // false
	Console.WriteLine(result);

	result = IsIn(items, 1, 3); // true
	Console.WriteLine(result);

	static bool IsIn(int[,] array, int rowIndex, int colIndex)
	{
		bool inArray = true;
		int length_Index_Diff = 1;
		int firstDimension = 0;
		int secondDimension = 1;

		if (rowIndex < 0 ||
			rowIndex > array.GetLength(firstDimension) - length_Index_Diff)
			return !inArray;

		if (colIndex < 0 ||
			colIndex > array.GetLength(secondDimension) - length_Index_Diff)
			return !inArray;

		return inArray;
	}
}