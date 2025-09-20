using System;
using System.Collections.Generic;
using System.Linq;

namespace lab1.Benchmarking;

public static class ComplexityApproximator
{
    private readonly record struct FitResult(string Name, double Scale, double R2);

    public static List<Point> Approximate(
        IEnumerable<Point> data,
        Func<double, double> func)
    {
        if (data is null) throw new ArgumentNullException(nameof(data));

        var pts = data.ToArray();
        if (pts.Length == 0) throw new ArgumentException("Пустые данные.", nameof(data));

        var xs = new double[pts.Length];
        var ys = new double[pts.Length];
        for (var i = 0; i < pts.Length; i++)
        {
            xs[i] = pts[i].XAxis; // x = n
            ys[i] = pts[i].YAxis; // y = время
        }

        var fvals = new double[pts.Length];
        for (var i = 0; i < pts.Length; i++)
        {
            var v = func(xs[i]);
            if (double.IsNaN(v) || double.IsInfinity(v)) v = 0.0;
            fvals[i] = v;
        }

        var scale = FitScaleLeastSquares(ys, fvals);

        var result = new List<Point>(pts.Length);
        for (var i = 0; i < pts.Length; i++)
        {
            var yHat = scale * fvals[i];
            result.Add(new Point(pts[i].XAxis, yHat));
        }

        return result;
    }

    public static List<Point> Approximate2D(
        IEnumerable<Point> data,
        Func<double, double, double> func)
    {
        if (data is null) throw new ArgumentNullException(nameof(data));

        var pts = data.ToArray();
        if (pts.Length == 0) throw new ArgumentException("Пустые данные.", nameof(data));

        var xs = new double[pts.Length];
        var ys = new double[pts.Length];
        var zs = new double[pts.Length];
        for (var i = 0; i < pts.Length; i++)
        {
            xs[i] = pts[i].XAxis; // x = n
            ys[i] = pts[i].YAxis; // y = m
            zs[i] = pts[i].ZAxis; // z = время
        }

        var fvals = new double[pts.Length];
        for (var i = 0; i < pts.Length; i++)
        {
            var v = func(xs[i], ys[i]);
            if (double.IsNaN(v) || double.IsInfinity(v)) v = 0.0;
            fvals[i] = v;
        }

        var scale = FitScaleLeastSquares(zs, fvals);

        var result = new List<Point>(pts.Length);
        for (var i = 0; i < pts.Length; i++)
        {
            var zHat = scale * fvals[i];
            result.Add(new Point(xs[i], ys[i], zHat));
        }

        return result;
    }

    // private static IEnumerable<(string name, Func<double, double> f)> DefaultCandidates1D() =>
    // [
    //     ("log n", x => x > 1 ? Math.Log(x) : 0.0),
    //     ("n", x => x),
    //     ("n log n", x => x > 1 ? x * Math.Log(x) : 0.0),
    //     ("n^2", x => x * x),
    //     ("n^3", x => x * x * x)
    // ];
    //
    // private static IEnumerable<(string name, Func<double, double, double> f)> DefaultCandidates2D() =>
    // [
    //     ("n", (n, m) => n),
    //     ("m", (n, m) => m),
    //     ("n + m", (n, m) => n + m),
    //     ("n m", (n, m) => n * m),
    //     ("n m log n", (n, m) => (n > 1 ? n * Math.Log(n) : 0.0) * m),
    //     ("n m log m", (n, m) => (m > 1 ? m * Math.Log(m) : 0.0) * n),
    //     ("n^2 m", (n, m) => n * n * m), // стандартное O(n^2 m) для (n×m)·(m×n)
    //     ("n m^2", (n, m) => n * m * m),
    //     ("n^2 m + n m^2", (n, m) => n * n * m + n * m * m),
    //     ("n^3", (n, m) => n * n * n) // на случай квадратных n≈m
    // ];

    // МНК-оценка масштаба: c = (Σ t_i f_i)/(Σ f_i^2)
    private static double FitScaleLeastSquares(IReadOnlyList<double> t, IReadOnlyList<double> f)
    {
        double num = 0, den = 0;
        for (var i = 0; i < t.Count; i++)
        {
            num += t[i] * f[i];
            den += f[i] * f[i];
        }

        return den == 0 ? 0 : num / den;
    }
}