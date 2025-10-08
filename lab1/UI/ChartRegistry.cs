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
            // ["const"] = () =>
            // {
            //     var cd = Experiments.BuildChartDataForConstant(5, 5, 16_000);
            //     var chart = Builder.Build2DLineChart(cd);
            //     return chart;
            // },
            
            // ["sum"] = () =>
            // {
            //     var cd = Experiments.BuildChartDataForSum(5, 5, 20_000);
            //     var chart = Builder.Build2DLineChart(cd);
            //     return chart;
            // },
            //
            // ["product"] = () =>
            // {
            //     var cd = Experiments.BuildChartDataForProduct(5, 5, 20_000);
            //     var chart = Builder.Build2DLineChart(cd);
            //     return chart;
            // },
            //
            // ["naive polynomial"] = () =>
            // {
            //     var cd = Experiments.BuildChartDataForNaivePolynomial(5, 5, 20_000);
            //     var chart = Builder.Build2DLineChart(cd);
            //     return chart;
            // },
            //
            // ["horner"] = () =>
            // {
            //     var cd = Experiments.BuildChartDataForHornersMethod(5, 5, 20_000);
            //     var chart = Builder.Build2DLineChart(cd);
            //     return chart;
            // },
            //
            // ["bubblesort"] = () =>
            // {
            //     var cd = Experiments.BuildChartDataForBubbleSort(5, 5, 5_000);
            //     var chart = Builder.Build2DLineChart(cd);
            //     return chart;
            // },
            //
            // ["quicksort"] = () =>
            // {
            //     var cd = Experiments.BuildChartDataForQuickSort(5, 5, 20_000);
            //     var chart = Builder.Build2DLineChart(cd);
            //     return chart;
            // },
            //
            // ["timsort"] = () =>
            // {
            //     var cd = Experiments.BuildChartDataForTimSort(5, 5, 20_000);
            //     var chart = Builder.Build2DLineChart(cd);
            //     return chart;
            // },

            ["naive pow"] = () =>
            {
                var cd = Experiments.BuildChartDataForNaivePow(500_000);
                var chart = Builder.Build2DLineChart(cd);
                return chart;
            },
            
            ["rec pow"] = () =>
            {
                var cd = Experiments.BuildChartDataForRecPow(2_000_000);
                var chart = Builder.Build2DLineChart(cd);
                return chart;
            },
            
            ["quick pow"] = () =>
            {
                var cd = Experiments.BuildChartDataForQuickPow(2_000_000);
                var chart = Builder.Build2DLineChart(cd);
                return chart;
            },
            
            ["classic quick pow"] = () =>
            {
                var cd = Experiments.BuildChartDataForClassicQuickPow(2_000_000);
                var chart = Builder.Build2DLineChart(cd);
                return chart;
            },
            
            // ["matrix"] = () =>
            // {
            //     var cd = Experiments.BuildChartDataForMatrixMultiplication(5, 5, 180);
            //     var chart = Builder.Build3DSurfaceChart(cd);
            //     return chart;
            // },

            // ["max"] = () =>
            // {
            //     var cd = Experiments.BuildChartDataForMax(5, 5, 20_000);
            //     var chart = Builder.Build2DLineChart(cd);
            //     return chart;
            // },
            //
            // ["shuffle"] = () =>
            // {
            //     var cd = Experiments.BuildChartDataForShuffle(5, 5, 20_000);
            //     var chart = Builder.Build2DLineChart(cd);
            //     return chart;
            // },
            //
            // ["xor"] = () =>
            // {
            //     var cd = Experiments.BuildChartDataForXor(5, 5, 20_000);
            //     var chart = Builder.Build2DLineChart(cd);
            //     return chart;
            // },
            //
            // ["prefix sums"] = () =>
            // {
            //     var cd = Experiments.BuildChartDataForPrefixSums(5, 5, 20_000);
            //     var chart = Builder.Build2DLineChart(cd);
            //     return chart;
            // },
        };
}