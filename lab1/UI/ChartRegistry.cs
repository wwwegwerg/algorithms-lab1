using System;
using System.Collections.Generic;
using System.Linq;
using lab1.Benchmarking;
using Plotly.NET;
using Chart = Plotly.NET.CSharp.Chart;

namespace lab1.UI;

public static class ChartRegistry
{
    private static readonly ChartBuilder Builder = new();

    public static Dictionary<string, Func<GenericChart>> All { get; } =
        new()
        {
            ["const"] = () =>
            {
                var constantsData = Experiments.BuildChartDataForConstant(new Benchmark(), 5, 2000);
                var constantChart = Builder.Build2DLineChart(constantsData);
                return constantChart;
            },

            ["sum"] = () =>
            {
                var constantsData = Experiments.BuildChartDataForSum(new Benchmark(), 5, 2000);
                var constantChart = Builder.Build2DLineChart(constantsData);
                return constantChart;
            }
        };
}