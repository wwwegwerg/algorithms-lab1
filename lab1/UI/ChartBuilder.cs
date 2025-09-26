using System;
using System.Linq;
using lab1.Benchmarking;
using Plotly.NET;
using Chart = Plotly.NET.CSharp.Chart;

namespace lab1.UI;

public class ChartBuilder
{
    public GenericChart Build2DLineChart(ChartData cd)
    {
        Console.WriteLine($"{cd.Title} – {cd.TotalExecTimeSeconds}s");

        var s1 = Chart
            .Line<double, double, string>(
                cd.EmpiricalResults.Select(p => p.XAxis),
                cd.EmpiricalResults.Select(p => p.YAxis),
                Name: "Экспериментальные результаты",
                ShowLegend: true,
                LineColor: Color.fromHex("#3E9BCB"),
                LineWidth: 2.5);

        var s2 = Chart
            .Line<double, double, string>(
                cd.TheoreticalResults.Select(p => p.XAxis),
                cd.TheoreticalResults.Select(p => p.YAxis),
                Name: $"Аппроксимация на основе теоретических оценок ({cd.ApproximationFunction})",
                ShowLegend: true,
                LineColor: Color.fromHex("#FF9A3C"),
                LineWidth: 2.5);

        return Plotly.NET.Chart.Combine([s1, s2])
            .WithTitle($"{cd.Title} – {cd.TotalExecTimeSeconds}s")
            .WithXAxisStyle(Title.init(cd.XAxisTitle))
            .WithYAxisStyle(Title.init(cd.YAxisTitle));
    }

    public GenericChart Build3DSurfaceChart(ChartData cd)
    {
        Console.WriteLine($"{cd.Title} – {cd.TotalExecTimeSeconds}s");

        var n = (int)Math.Sqrt(cd.EmpiricalResults.Count);
        var xs = Enumerable.Range(1, n).Select(x => (double)x).ToArray();

        var empiricalZs = new double[n][];
        for (var i = 0; i < n; i++)
        {
            empiricalZs[i] = new double[n];
            for (var j = 0; j < n; j++)
            {
                empiricalZs[i][j] = cd.EmpiricalResults[i * n + j].ZAxis;
            }
        }

        var s1 = Chart
            .Surface<double, double, double, string>(
                empiricalZs,
                xs,
                xs,
                Name: "Экспериментальные результаты",
                ShowLegend: true,
                ShowScale: false);

        var theoreticalZs = new double[n][];
        for (var i = 0; i < n; i++)
        {
            theoreticalZs[i] = new double[n];
            for (var j = 0; j < n; j++)
            {
                theoreticalZs[i][j] = cd.TheoreticalResults[i * n + j].ZAxis;
            }
        }

        var s2 = Chart
            .Surface<double, double, double, string>(
                theoreticalZs,
                xs,
                xs,
                Name: $"Аппроксимация на основе теоретических оценок ({cd.ApproximationFunction})",
                ShowLegend: true,
                ShowScale: false);

        return Plotly.NET.Chart.Combine([s1, s2])
            .WithTitle($"{cd.Title} – {cd.TotalExecTimeSeconds}s")
            .WithXAxisStyle(Title.init(cd.XAxisTitle))
            .WithYAxisStyle(Title.init(cd.YAxisTitle))
            .WithZAxisStyle(Title.init(cd.ZAxisTitle));
    }
}