using System.Collections.Generic;

namespace lab1.Benchmarking;

public class ChartData2D(
    string title,
    string xAxisTitle,
    string yAxisTitle,
    IList<ExperimentResult2D> empiricalPoints,
    IList<ExperimentResult2D> theoreticalPoints)
{
    public readonly string Title = title;
    public readonly string XAxisTitle = xAxisTitle;
    public readonly string YAxisTitle = yAxisTitle;
    public readonly IList<ExperimentResult2D> EmpiricalPoints = empiricalPoints;
    public readonly IList<ExperimentResult2D> TheoreticalPoints = theoreticalPoints;
}