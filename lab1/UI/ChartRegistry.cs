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
            
            ["naive polynomial"] = () =>
            {
                var chartData = Experiments.BuildChartDataForNaivePolynomial(5, 5, 2000);
                var chart = Builder.Build2DLineChart(chartData);
                return chart;
            },
            
            ["horner"] = () =>
            {
                var chartData = Experiments.BuildChartDataForHornersMethod(5, 5, 2000);
                var chart = Builder.Build2DLineChart(chartData);
                return chart;
            },
            
            ["bubblesort"] = () =>
            {
                var chartData = Experiments.BuildChartDataForBubbleSort(5, 5, 2000);
                var chart = Builder.Build2DLineChart(chartData);
                return chart;
            },
            
            ["quicksort"] = () =>
            {
                var chartData = Experiments.BuildChartDataForQuickSort(5, 5, 2000);
                var chart = Builder.Build2DLineChart(chartData);
                return chart;
            },
            
            ["timsort"] = () =>
            {
                var chartData = Experiments.BuildChartDataForTimSort(5, 5, 2000);
                var chart = Builder.Build2DLineChart(chartData);
                return chart;
            },
            
            ["naive pow"] = () =>
            {
                var chartData = Experiments.BuildChartDataForNaivePow(2000);
                var chart = Builder.Build2DLineChart(chartData);
                return chart;
            },
            
            ["rec pow"] = () =>
            {
                var chartData = Experiments.BuildChartDataForRecPow(2000);
                var chart = Builder.Build2DLineChart(chartData);
                return chart;
            },
            
            ["quick pow"] = () =>
            {
                var chartData = Experiments.BuildChartDataForQuickPow(2000);
                var chart = Builder.Build2DLineChart(chartData);
                return chart;
            },
            
            ["classic quick pow"] = () =>
            {
                var chartData = Experiments.BuildChartDataForClassicQuickPow(2000);
                var chart = Builder.Build2DLineChart(chartData);
                return chart;
            },
            
            ["matrix"] = () =>
            {
                var chartData = Experiments.BuildChartDataForMatrixMultiplication(5, 5, 20);
                var chart = Builder.Build3DSurfaceChart(chartData);
                return chart;
            }
        };
}