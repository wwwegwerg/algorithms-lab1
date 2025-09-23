using System;
using System.Collections.Generic;
using lab1.Algorithms;

namespace lab1.Benchmarking;

public static class Experiments
{
    private static readonly Random Rnd = new();

    public static ChartData BuildChartDataForConstant(
        int warmupCount, int repetitionsCount, int dataSize)
    {
        var times = new List<Point>();
        var data = new List<int>();
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Rnd.Next(1, 101));

            var task = new Constant(data.ToArray());
            Benchmark.Warmup(task, warmupCount);
            var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
            times.Add(new Point(i, time));
        }

        var approx = ComplexityApproximator.Approximate(times, _ => 1);

        return new ChartData(
            "Постоянная функция",
            times,
            approx,
            "const",
            "Размерность вектора v",
            "Время (мс)"
        );
    }

    public static ChartData BuildChartDataForSum(
        int warmupCount, int repetitionsCount, int dataSize)
    {
        var times = new List<Point>();
        var data = new List<int>();
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Rnd.Next(1, 101));

            var task = new Sum(data.ToArray());
            Benchmark.Warmup(task, warmupCount);
            var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
            times.Add(new Point(i, time));
        }

        var approx = ComplexityApproximator.Approximate(times, x => x);

        return new ChartData(
            "Сумма элементов",
            times,
            approx,
            "n",
            "Размерность вектора v",
            "Время (мс)"
        );
    }

    public static ChartData BuildChartDataForProduct(
        int warmupCount, int repetitionsCount, int dataSize)
    {
        var times = new List<Point>();
        var data = new List<int>();
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Rnd.Next(1, 101));

            var task = new Product(data.ToArray());
            Benchmark.Warmup(task, warmupCount);
            var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
            times.Add(new Point(i, time));
        }

        var approx = ComplexityApproximator.Approximate(times, x => x);

        return new ChartData(
            "Произведение элементов",
            times,
            approx,
            "n",
            "Размерность вектора v",
            "Время (мс)"
        );
    }

    public static ChartData BuildChartDataForNaivePolynomial(
        int warmupCount, int repetitionsCount, int dataSize)
    {
        var times = new List<Point>();
        var data = new List<int>();
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Rnd.Next(1, 101));

            var task = new NaivePolynomial(data.ToArray(), 1.5);
            Benchmark.Warmup(task, warmupCount);
            var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
            times.Add(new Point(i, time));
        }

        var approx = ComplexityApproximator.Approximate(times, x => x);

        return new ChartData(
            "Наивное вычисление многочлена",
            times,
            approx,
            "n",
            "Размерность вектора v",
            "Время (мс)"
        );
    }

    public static ChartData BuildChartDataForHornersMethod(
        int warmupCount, int repetitionsCount, int dataSize)
    {
        var times = new List<Point>();
        var data = new List<int>();
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Rnd.Next(1, 101));

            var task = new HornersMethod(data.ToArray(), 1.5);
            Benchmark.Warmup(task, warmupCount);
            var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
            times.Add(new Point(i, time));
        }

        var approx = ComplexityApproximator.Approximate(times, x => x);

        return new ChartData(
            "Метод Горнера",
            times,
            approx,
            "n",
            "Размерность вектора v",
            "Время (мс)"
        );
    }

    public static ChartData BuildChartDataForBubbleSort(
        int warmupCount, int repetitionsCount, int dataSize)
    {
        var times = new List<Point>();
        var data = new List<int>();
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Rnd.Next(1, 101));

            var task = new BubbleSort(data.ToArray());
            Benchmark.Warmup(task, warmupCount);
            var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
            times.Add(new Point(i, time));
        }

        var approx = ComplexityApproximator.Approximate(times, x => x);

        return new ChartData(
            "Сортировка пузырьком (BubbleSort)",
            times,
            approx,
            "n",
            "Размерность вектора v",
            "Время (мс)"
        );
    }

    public static ChartData BuildChartDataForQuickSort(
        int warmupCount, int repetitionsCount, int dataSize)
    {
        var times = new List<Point>();
        var data = new List<int>();
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Rnd.Next(1, 101));

            var task = new QuickSort(data.ToArray());
            Benchmark.Warmup(task, warmupCount);
            var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
            times.Add(new Point(i, time));
        }

        var approx = ComplexityApproximator.Approximate(times, x => x);

        return new ChartData(
            "Быстрая сортировка (QuickSort)",
            times,
            approx,
            "n",
            "Размерность вектора v",
            "Время (мс)"
        );
    }

    public static ChartData BuildChartDataForTimSort(
        int warmupCount, int repetitionsCount, int dataSize)
    {
        var times = new List<Point>();
        var data = new List<int>();
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Rnd.Next(1, 101));

            var task = new TimSort(data.ToArray());
            Benchmark.Warmup(task, warmupCount);
            var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
            times.Add(new Point(i, time));
        }

        var approx = ComplexityApproximator.Approximate(times, x => x);

        return new ChartData(
            "Гибридная сортировка TimSort",
            times,
            approx,
            "n",
            "Размерность вектора v",
            "Время (мс)"
        );
    }

    public static ChartData BuildChartDataForNaivePow(int dataSize)
    {
        var steps = new List<Point>();
        var data = new List<int>();
        var b = Rnd.NextDouble();
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Rnd.Next(1, 101));

            var task = new NaivePow(data.ToArray(), b);
            var step = Benchmark.MeasureStepsCount(task);
            steps.Add(new Point(i, step));
        }

        var approx = ComplexityApproximator.Approximate(steps, x => x);

        return new ChartData(
            "Простое (наивное) возведение в степень",
            steps,
            approx,
            "n",
            "Размерность вектора v",
            "Количество шагов"
        );
    }

    public static ChartData BuildChartDataForRecPow(int dataSize)
    {
        var steps = new List<Point>();
        var data = new List<int>();
        var b = Rnd.NextDouble();
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Rnd.Next(1, 101));

            var task = new RecPow(data.ToArray(), b);
            var step = Benchmark.MeasureStepsCount(task);
            steps.Add(new Point(i, step));
        }

        var approx = ComplexityApproximator.Approximate(steps, x => x);

        return new ChartData(
            "Рекурсивное возведение в степень",
            steps,
            approx,
            "n",
            "Размерность вектора v",
            "Количество шагов"
        );
    }

    public static ChartData BuildChartDataForQuickPow(int dataSize)
    {
        var steps = new List<Point>();
        var data = new List<int>();
        var b = Rnd.NextDouble();
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Rnd.Next(1, 101));

            var task = new QuickPow(data.ToArray(), b);
            var step = Benchmark.MeasureStepsCount(task);
            steps.Add(new Point(i, step));
        }

        var approx = ComplexityApproximator.Approximate(steps, x => x);

        return new ChartData(
            "Быстрое возведение в степень",
            steps,
            approx,
            "n",
            "Размерность вектора v",
            "Количество шагов"
        );
    }

    public static ChartData BuildChartDataForClassicQuickPow(int dataSize)
    {
        var steps = new List<Point>();
        var data = new List<int>();
        var b = Rnd.NextDouble();
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Rnd.Next(1, 101));

            var task = new ClassicQuickPow(data.ToArray(), b);
            var step = Benchmark.MeasureStepsCount(task);
            steps.Add(new Point(i, step));
        }

        var approx = ComplexityApproximator.Approximate(steps, x => x);

        return new ChartData(
            "Классическое быстрое возведение в степень",
            steps,
            approx,
            "n",
            "Размерность вектора v",
            "Количество шагов"
        );
    }

    public static ChartData BuildChartDataForMatrixMultiplication(
        int warmupCount, int repetitionsCount, int dataSize)
    {
        var times = new List<Point>();
        for (var n = 1; n <= dataSize; n++)
        {
            for (var m = 1; m <= dataSize; m++)
            {
                var data1 = new int[n, m];
                var data2 = new int[m, n];
                for (var i = 0; i < n; i++)
                {
                    for (var j = 0; j < m; j++)
                    {
                        var val = Rnd.Next(1, 101);
                        data1[i, j] = val;
                        data2[j, i] = val;
                    }
                }

                var task = new MatrixMultiplicationTask(new Matrix(data1), new Matrix(data2));
                Benchmark.Warmup(task, warmupCount);
                var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
                times.Add(new Point(n, m, time));
            }
        }

        var approx = ComplexityApproximator.Approximate2D(times, (x, y) => x * x * y);

        return new ChartData(
            "Матричное произведение",
            times,
            approx,
            "n^2 m",
            "эта шняга не работает",
            "и эта тоже",
            "Время (мс)"
        );
    }
}