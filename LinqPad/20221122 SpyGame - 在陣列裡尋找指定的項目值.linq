<Query Kind="Program" />

void Main()
{
	List<SpyGame> source = new List<SpyGame>();
	source.Add(new SpyGame(new int[] { 1, 2, 4, 0, 0, 7, 5 }));
	source.Add(new SpyGame(new int[] { 1, 0, 2, 4, 0, 5, 7 }));
	source.Add(new SpyGame(new int[] { 1, 7, 2, 0, 4, 5, 0 }));

	foreach (SpyGame spyGame in source)
		spyGame.IsValidResult().Dump();

	SpyGameAllenKuo.TryFind007(new int[] { 1, 2, 4, 0, 0, 7, 5 }).Dump();
	SpyGameAllenKuo.TryFind007(new int[] { 1, 0, 2, 4, 0, 5, 7 }).Dump();
	SpyGameAllenKuo.TryFind007(new int[] { 1, 7, 2, 0, 4, 5, 0 }).Dump();
}

public class SpyGame
{
	private readonly int[] _source;
	private const int _targetNumber_Zero = 0;
	private const int _targetNumber_Seven = 7;

	public SpyGame(int[] source)
	{
		_source = source;
	}

	public string IsValidResult()
	{
		string sourceComma = string.Join(",", _source);
		bool valid = isValid();
		return $"{sourceComma}：{valid}";
	}

	private bool isValid()
	{
		if (isTwoZeroExist() == false)
			return false;

		if (isSevenExist() == false)
			return false;

		return is007();
	}

	private bool isTwoZeroExist()
	{
		int targetNumberCalc = 2;
		return _source.Count(s => s == _targetNumber_Zero) == targetNumberCalc;
	}

	private bool isSevenExist()
	{
		return _source.Any(s => s == _targetNumber_Seven);
	}

	/// <summary>
	/// 找出 0 和 7 資料並判斷 7 的 index 為最大值
	/// </summary>
	/// <returns></returns>
	private bool is007()
	{
		var result = _source
			.Select((s, index) => new { s, index })
			.Where(w =>
					w.s == _targetNumber_Zero ||
					w.s == _targetNumber_Seven)
			.MaxBy(w => w.index);

		int maxIndexNumber = 7;
		return (result != null &&
			result.s == maxIndexNumber);
	}
}

/// <summary>
/// 來源：https://hackmd.io/@ucPglBJsTT2LEYis44uI7A/Hyqxc9F8j
/// </summary>
public class SpyGameAllenKuo
{
	/// <summary>
	/// 判斷序列是否依序出現 0，0，7
	/// </summary>
	/// <param name="numbers"></param>
	/// <returns></returns>
	public static bool TryFind007(IEnumerable<int> numbers)
	{
		Stack<int> targets = new Stack<int>();
		targets.Push(7);
		targets.Push(0);
		targets.Push(0);

		int target = targets.Peek();
		foreach (int number in numbers)
		{
			if (number != target)
				continue;

			targets.Pop();

			if (targets.Count == 0)
				return true;

			target = targets.Peek();
		}

		return false;
	}
}
