using System.Collections.Generic;

namespace lab1.Benchmarking;

public static class Constants
{
	public const int ArraySize = 10000;
	public static IReadOnlyCollection<int> FieldCounts;

	static Constants()
	{
		var fc = new List<int>();
		for (var i = 1; i <= 2000; i++)
			fc.Add(i);
		FieldCounts = fc.AsReadOnly();
	}
}