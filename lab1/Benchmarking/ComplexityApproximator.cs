using System;
using System.Collections.Generic;
using System.Linq;

namespace lab1.Benchmarking;

public static class ComplexityApproximator
{
    private readonly record struct FitResult(string Name, double Scale, double R2);

    private static IEnumerable<(string name, Func<double, double> f)> DefaultCandidates() =>
    [
        ("1", _ => 1),
        ("log n", x => x > 1 ? Math.Log(x) : 0.0),
        ("n", x => x),
        ("n log n", x => x > 1 ? x * Math.Log(x) : 0.0),
        ("n^2", x => x * x),
        ("n^3", x => x * x * x)
    ];

    public static (string, List<Point>) Approximate(
        IEnumerable<Point> data)
    {
        var pts = data.ToArray();

        // Разворачиваем в массивы для МНК
        var xs = new double[pts.Length];
        var ys = new double[pts.Length];
        for (var i = 0; i < pts.Length; i++)
        {
            xs[i] = pts[i].XAxis;
            ys[i] = pts[i].YAxis;
        }

        var candidates = DefaultCandidates();
        FitResult? best = null;
        Func<double, double>? bestFunc = null;
        foreach (var (name, f) in candidates)
        {
            var fvals = new double[pts.Length];
            var allZero = true;
            for (var i = 0; i < pts.Length; i++)
            {
                var v = f(xs[i]);
                if (double.IsNaN(v) || double.IsInfinity(v)) v = 0.0;
                fvals[i] = v;
                if (v != 0.0) allZero = false;
            }

            if (allZero) continue;
            var c = FitScaleLeastSquares(ys, fvals);
            var yPred = new double[pts.Length];
            for (var i = 0; i < pts.Length; i++) yPred[i] = c * fvals[i];
            var r2 = RSquared(ys, yPred);
            var fit = new FitResult(name, c, r2);
            if (best is null || fit.R2 > best.Value.R2)
            {
                best = fit;
                bestFunc = f;
            }
        }

        if (best is null || bestFunc is null)
            return ("none", []);
        // throw new InvalidOperationException(
        //     "Не удалось подобрать валидного кандидата. Проверьте данные (x>0 для лог-функций).");

        // Возвращаем те же x, но с аппроксимированными ŷ
        var result = new List<Point>(pts.Length);
        for (var i = 0; i < pts.Length; i++)
        {
            var fx = bestFunc(xs[i]);
            if (double.IsNaN(fx) || double.IsInfinity(fx)) fx = 0.0;
            var yHat = best.Value.Scale * fx;
            result.Add(new Point((int)xs[i], yHat));
        }

        return (best.Value.Name, result);
    }

    public static List<Point> Approximate2D(
        IEnumerable<Point> data,
        Func<double, double, double> func)
    {
        var pts = data.ToArray();

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