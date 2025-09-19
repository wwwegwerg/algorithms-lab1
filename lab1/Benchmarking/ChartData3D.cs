using System.Collections.Generic;

namespace lab1.Benchmarking;

public class ChartData3D(
    string title,
    string xAxisTitle,
    string yAxisTitle,
    string zAxisTitle,
    IList<ExperimentResult3D> empiricalPoints,
    IList<ExperimentResult3D> theoreticalPoints)
{
    public readonly string Title = title;
    public readonly string XAxisTitle = xAxisTitle;
    public readonly string YAxisTitle = yAxisTitle;
    public readonly string ZAxisTitle = zAxisTitle;
    public readonly IList<ExperimentResult3D> EmpiricalPoints = empiricalPoints;
    public readonly IList<ExperimentResult3D> TheoreticalPoints = theoreticalPoints;
}