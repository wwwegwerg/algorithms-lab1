using System;
using System.Collections.Generic;
using System.Linq;

namespace lab1.Benchmarking;

public static class ComplexityApproximator
{
    private readonly record struct FitResult(string Name, double Scale, double R2);

    public static (string, List<Point>) Approximate(
        IEnumerable<Point> data,
        IEnumerable<(string name, Func<double, double> f)>? candidates = null)
    {
        if (data is null) throw new ArgumentNullException(nameof(data));

        var pts = data.ToArray();
        if (pts.Length == 0) throw new ArgumentException("Пустые данные.", nameof(data));

        var n = new double[pts.Length];
        var t = new double[pts.Length];
        for (var i = 0; i < pts.Length; i++)
        {
            n[i] = pts[i].XAxis; // x = n
            t[i] = pts[i].YAxis; // y = время
        }

        candidates ??= DefaultCandidates1D();

        FitResult? best = null;
        Func<double, double>? bestFunc = null;

        foreach (var (name, f) in candidates)
        {
            var fvals = new double[pts.Length];
            var allZero = true;
            for (var i = 0; i < pts.Length; i++)
            {
                var v = f(n[i]);
                if (double.IsNaN(v) || double.IsInfinity(v)) v = 0.0;
                fvals[i] = v;
                if (v != 0.0) allZero = false;
            }

            if (allZero) continue;

            var c = FitScaleLeastSquares(t, fvals);

            var yPred = new double[pts.Length];
            for (var i = 0; i < pts.Length; i++) yPred[i] = c * fvals[i];

            var r2 = RSquared(t, yPred);

            var fit = new FitResult(name, c, r2);
            if (best is null || fit.R2 > best.Value.R2)
            {
                best = fit;
                bestFunc = f;
            }
        }

        if (best is null || bestFunc is null)
            return ("none", []);

        var result = new List<Point>(pts.Length);
        for (var i = 0; i < pts.Length; i++)
        {
            var fx = bestFunc(n[i]);
            if (double.IsNaN(fx) || double.IsInfinity(fx)) fx = 0.0;
            var yHat = best.Value.Scale * fx;
            result.Add(new Point(pts[i].XAxis, yHat));
        }

        return (best.Value.Name, result);
    }

    public static (string, List<Point>) ApproximateNM(
        IEnumerable<Point> data,
        IEnumerable<(string name, Func<double, double, double> f)>? candidates = null)
    {
        if (data is null) throw new ArgumentNullException(nameof(data));

        var pts = data.ToArray();
        if (pts.Length == 0) throw new ArgumentException("Пустые данные.", nameof(data));

        var ns = new double[pts.Length];
        var ms = new double[pts.Length];
        var t = new double[pts.Length];
        for (var i = 0; i < pts.Length; i++)
        {
            ns[i] = pts[i].XAxis; // x = n
            ms[i] = pts[i].YAxis; // y = m
            t[i] = pts[i].ZAxis; // z = время
        }

        candidates ??= DefaultCandidates2D();

        FitResult? best = null;
        Func<double, double, double>? bestFunc = null;

        foreach (var (name, f) in candidates)
        {
            var fvals = new double[pts.Length];
            var allZero = true;
            for (var i = 0; i < pts.Length; i++)
            {
                var v = f(ns[i], ms[i]);
                if (double.IsNaN(v) || double.IsInfinity(v)) v = 0.0;
                fvals[i] = v;
                if (v != 0.0) allZero = false;
            }

            if (allZero) continue;

            var c = FitScaleLeastSquares(t, fvals);

            var zPred = new double[pts.Length];
            for (var i = 0; i < pts.Length; i++) zPred[i] = c * fvals[i];

            var r2 = RSquared(t, zPred);

            var fit = new FitResult(name, c, r2);
            if (best is null || fit.R2 > best.Value.R2)
            {
                best = fit;
                bestFunc = f;
            }
        }

        if (best is null || bestFunc is null)
            return ("none", []);
        
        var result = new List<Point>(pts.Length);
        for (var i = 0; i < pts.Length; i++)
        {
            var fval = bestFunc(ns[^1], ms[^1]);
            if (double.IsNaN(fval) || double.IsInfinity(fval)) fval = 0.0;
            var zHat = best.Value.Scale * fval;
            result.Add(new Point(ns[i], ms[i], zHat));
        }

        return (best.Value.Name, result);
    }

    private static IEnumerable<(string name, Func<double, double> f)> DefaultCandidates1D() =>
    [
        ("log n", x => x > 1 ? Math.Log(x) : 0.0),
        ("n", x => x),
        ("n log n", x => x > 1 ? x * Math.Log(x) : 0.0),
        ("n^2", x => x * x),
        ("n^3", x => x * x * x)
    ];

    private static IEnumerable<(string name, Func<double, double, double> f)> DefaultCandidates2D() =>
    [
        ("n", (n, m) => n),
        ("m", (n, m) => m),
        ("n + m", (n, m) => n + m),
        ("n m", (n, m) => n * m),
        ("n m log n", (n, m) => (n > 1 ? n * Math.Log(n) : 0.0) * m),
        ("n m log m", (n, m) => (m > 1 ? m * Math.Log(m) : 0.0) * n),
        ("n^2 m", (n, m) => n * n * m), // стандартное O(n^2 m) для (n×m)·(m×n)
        ("n m^2", (n, m) => n * m * m),
        ("n^2 m + n m^2", (n, m) => n * n * m + n * m * m),
        ("n^3", (n, m) => n * n * n) // на случай квадратных n≈m
    ];

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

    // Коэффициент детерминации R^2
    private static double RSquared(IReadOnlyList<double> yTrue, IReadOnlyList<double> yPred)
    {
        double mean = 0;
        for (var i = 0; i < yTrue.Count; i++) mean += yTrue[i];
        mean /= yTrue.Count;

        double ssTot = 0, ssRes = 0;
        for (var i = 0; i < yTrue.Count; i++)
        {
            var d1 = yTrue[i] - mean;
            var d2 = yTrue[i] - yPred[i];
            ssTot += d1 * d1;
            ssRes += d2 * d2;
        }

        return ssTot == 0 ? 1 : 1 - ssRes / ssTot;
    }
}