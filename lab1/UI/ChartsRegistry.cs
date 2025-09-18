using System;
using System.Collections.Generic;
using System.Linq;
using Plotly.NET;
using Chart = Plotly.NET.CSharp.Chart;

namespace lab1.UI;

public static class ChartRegistry
{
    public static Dictionary<string, Func<GenericChart>> All { get; } =
        new()
        {
            ["Линия (sin)"] = () =>
            {
                var xs = Enumerable.Range(0, 50).Select(i => i / 10.0).ToArray();
                var ys = xs.Select(Math.Sin).ToArray();
                return Chart
                    .Line<double, double, string>(xs, ys)
                    .WithTraceInfo(Name: "sin(x)")
                    .WithTitle("Line: sin(x)");
            },

            ["TEST"] = () =>
            {
                var xs = Enumerable.Range(0, 200).Select(i => i / 20.0).ToArray();
                var y1 = xs.Select(Math.Sin).ToArray();
                var y2 = xs.Select(Math.Cos).ToArray();
                var y3 = xs.Select(x => 0.5 * Math.Sin(2 * x)).ToArray();

                var s1 = Chart.Line<double, double, string>(xs, y1)
                    .WithTraceInfo(Name: "sin(x)")
                    .WithLine(Line.init(
                        Color: Color.fromHex("#e76f51"),
                        Width: 2.5
                    ));
                var s2 = Chart.Line<double, double, string>(xs, y2)
                    .WithTraceInfo(Name: "cos(x)")
                    .WithLine(Line.init(
                        Color: Color.fromHex("#2a9d8f"),
                        Width: 2.5
                    ));
                var s3 = Chart.Line<double, double, string>(xs, y3)
                    .WithTraceInfo(Name: "0.5·sin(2x)")
                    .WithLine(Line.init(
                        Color: Color.fromHex("#264653"),
                        Width: 2.5
                    ));

                return Plotly.NET.Chart.Combine([s1, s2, s3])
                    .WithTitle("Несколько линий (офлайн)")
                    // .WithLegend(true)
                    .WithXAxisStyle(Title.init("x"))
                    .WithYAxisStyle(Title.init("y"));
            },
        };
}