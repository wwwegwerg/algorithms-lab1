using System.Collections.Generic;

namespace lab1.Benchmarking;

public class ChartData
{
    public readonly string Title;
    public readonly IList<Point> EmpiricalResults;
    public readonly IList<Point> TheoreticalResults;
    public readonly string ApproximationFunction;
    public readonly string XAxisTitle;
    public readonly string YAxisTitle;
    public readonly string ZAxisTitle;

    public ChartData(string title, IList<Point> empiricalResults, IList<Point> theoreticalResults, string approximationFunction, string xAxisTitle, string yAxisTitle, string zAxisTitle = "")
    {
        Title = title;
        EmpiricalResults = empiricalResults;
        TheoreticalResults = theoreticalResults;
        ApproximationFunction = approximationFunction;
        XAxisTitle = xAxisTitle;
        YAxisTitle = yAxisTitle;
        ZAxisTitle = zAxisTitle;
    }
}