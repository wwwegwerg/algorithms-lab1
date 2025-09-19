using System;
using System.Collections.Generic;
using System.Linq;

namespace lab1.Benchmarking;

/// <summary>
/// Аппроксимация экспериментального графика теоретическими функциями сложности.
/// Находит лучший шаблон f(n) (по R²), подбирает масштаб c и возвращает
/// новую последовательность точек (x_i, c * f(x_i)) той же длины.
/// </summary>
public static class ComplexityApproximator
{
    private readonly record struct FitResult(string Name, double Scale, double R2);

    /// <summary>
    /// Аппроксимирует существующие точки, не предсказывая за их пределами.
    /// </summary>
    /// <param name="data">Точки эксперимента (x: размер, y: время)</param>
    /// <param name="candidates">
    /// Необязательный список кандидатов (name, f). Если null — используются дефолтные:
    /// "log n", "n", "n log n", "n^2", "n^3".
    /// </param>
    /// <returns>Новый List&lt;ExperimentResult2D&gt; той же длины с ŷ на лучшей кривой.</returns>
    public static List<ExperimentResult2D> Approximate(
        IEnumerable<ExperimentResult2D> data,
        IEnumerable<(string name, Func<double, double> f)>? candidates = null)
    {
        if (data is null) throw new ArgumentNullException(nameof(data));

        var pts = data.ToArray();
        if (pts.Length == 0) throw new ArgumentException("Пустые данные.", nameof(data));

        // Разворачиваем в массивы для МНК
        var n = new double[pts.Length];
        var t = new double[pts.Length];
        for (var i = 0; i < pts.Length; i++)
        {
            n[i] = pts[i].XAxis;
            t[i] = pts[i].YAxis;
        }

        candidates ??= DefaultCandidates();

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
            throw new InvalidOperationException(
                "Не удалось подобрать валидного кандидата. Проверьте данные (x>0 для лог-функций).");

        // Возвращаем те же x, но с аппроксимированными ŷ
        var result = new List<ExperimentResult2D>(pts.Length);
        for (var i = 0; i < pts.Length; i++)
        {
            var fx = bestFunc(n[i]);
            if (double.IsNaN(fx) || double.IsInfinity(fx)) fx = 0.0;
            var yHat = best.Value.Scale * fx;
            result.Add(new ExperimentResult2D((int)n[i], yHat));
        }

        return result;
    }

    // ----- Вспомогательные -----

    private static IEnumerable<(string name, Func<double, double> f)> DefaultCandidates() =>
    [
        // Для логарифмических шаблонов возвращаем 0 при x <= 1,
        // чтобы избежать NaN/-Inf и «плохих» кандидатов.
        ("log n", x => x > 1 ? Math.Log(x) : 0.0),
        ("n", x => x),
        ("n log n", x => x > 1 ? x * Math.Log(x) : 0.0),
        ("n^2", x => x * x),
        ("n^3", x => x * x * x)
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