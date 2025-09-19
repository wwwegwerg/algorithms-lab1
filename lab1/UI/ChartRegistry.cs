using System;
using System.Collections.Generic;
using lab1.Benchmarking;
using Plotly.NET;

namespace lab1.UI;

public static class ChartRegistry
{
    private static readonly ChartBuilder Builder = new();

    public static Dictionary<string, Func<GenericChart>> All { get; } =
        new()
        {
            ["const"] = () =>
            {
                var chartData = Experiments.BuildChartDataForConstant(5, 5, 2000);
                var chart = Builder.Build2DLineChart(chartData);
                return chart;
            },

            ["sum"] = () =>
            {
                var chartData = Experiments.BuildChartDataForSum(5, 5, 2000);
                var chart = Builder.Build2DLineChart(chartData);
                return chart;
            },

            ["product"] = () =>
            {
                var chartData = Experiments.BuildChartDataForProduct(5, 5, 2000);
                var chart = Builder.Build2DLineChart(chartData);
                return chart;
            },
            
            ["matrix"] = () =>
            {
                var chartData = Experiments.BuildChartDataForMatrixMultiplication(5, 5, 2000);
                var chart = Builder.Build3DSurfaceChart(chartData);
                return chart;
            }
        };
}