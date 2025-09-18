using System.Collections.Generic;

namespace lab1.Benchmarking;

public class ChartData
{
	public string Title;
	public IList<ExperimentResult> EmpiricalPoints;
	public IList<ExperimentResult> TheoreticalPoints;
}