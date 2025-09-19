using System;
using System.Linq;
using lab1.Benchmarking;
using Plotly.NET;
using Chart = Plotly.NET.CSharp.Chart;

namespace lab1.UI;

public class ChartBuilder
{
    public GenericChart Build2DLineChart(ChartData2D cd)
    {
        var s1 = Chart
            .Line<int, double, string>(
                cd.EmpiricalPoints.Select(p => p.XAxis),
                cd.EmpiricalPoints.Select(p => p.YAxis))
            .WithTraceInfo(Name: "Экспериментальные результаты")
            .WithLine(Line.init(
                Color: Color.fromHex("#3E9BCB"),
                Width: 2.5
            ));

        var s2 = Chart
            .Line<int, double, string>(
                cd.TheoreticalPoints.Select(p => p.XAxis),
                cd.TheoreticalPoints.Select(p => p.YAxis))
            .WithTraceInfo(Name: "Аппроксимация на основе теоретических оценок")
            .WithLine(Line.init(
                Color: Color.fromHex("#FF9A3C"),
                Width: 2.5
            ));

        return Plotly.NET.Chart.Combine([s1, s2])
            .WithTitle(cd.Title)
            .WithXAxisStyle(Title.init(cd.XAxisTitle))
            .WithYAxisStyle(Title.init(cd.YAxisTitle))
            .WithLegend(true);
    }

    public GenericChart Build3DSurfaceChart(ChartData3D cd)
    {
        throw new NotImplementedException();
    }
}