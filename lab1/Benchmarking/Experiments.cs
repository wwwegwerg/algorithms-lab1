using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using lab1.Algorithms;

namespace lab1.Benchmarking;

public static class Experiments
{
    private static readonly int[] Data = Helper.ReadData();

    public static ChartData BuildChartDataForConstant(int warmupCount, int repetitionsCount, int dataSize)
    {
        return Build1DTime(
            "Постоянная функция",
            "Размерность вектора v",
            "Время (мс)",
            warmupCount,
            repetitionsCount,
            dataSize,
            arr => new Constant(arr));
    }

    public static ChartData BuildChartDataForSum(int warmupCount, int repetitionsCount, int dataSize)
    {
        return Build1DTime(
            "Сумма элементов",
            "Размерность вектора v",
            "Время (мс)",
            warmupCount,
            repetitionsCount,
            dataSize,
            arr => new Sum(arr));
    }

    public static ChartData BuildChartDataForProduct(int warmupCount, int repetitionsCount, int dataSize)
    {
        return Build1DTime(
            "Произведение элементов",
            "Размерность вектора v",
            "Время (мс)",
            warmupCount,
            repetitionsCount,
            dataSize,
            arr => new Product(arr));
    }

    public static ChartData BuildChartDataForNaivePolynomial(int warmupCount, int repetitionsCount, int dataSize)
    {
        return Build1DTime(
            "Наивное вычисление многочлена",
            "Размерность вектора v",
            "Время (мс)",
            warmupCount,
            repetitionsCount,
            dataSize,
            arr => new NaivePolynomial(arr, 1.5));
    }

    public static ChartData BuildChartDataForHornersMethod(int warmupCount, int repetitionsCount, int dataSize)
    {
        return Build1DTime(
            "Метод Горнера",
            "Размерность вектора v",
            "Время (мс)",
            warmupCount,
            repetitionsCount,
            dataSize,
            arr => new HornersMethod(arr, 1.5));
    }

    public static ChartData BuildChartDataForBubbleSort(int warmupCount, int repetitionsCount, int dataSize)
    {
        return Build1DTime(
            "Сортировка пузырьком (BubbleSort)",
            "Размерность вектора v",
            "Время (мс)",
            warmupCount,
            repetitionsCount,
            dataSize,
            arr => new BubbleSort((int[])arr.Clone()));
    }

    public static ChartData BuildChartDataForQuickSort(int warmupCount, int repetitionsCount, int dataSize)
    {
        return Build1DTime(
            "Быстрая сортировка (QuickSort)",
            "Размерность вектора v",
            "Время (мс)",
            warmupCount,
            repetitionsCount,
            dataSize,
            arr => new QuickSort((int[])arr.Clone()));
    }

    public static ChartData BuildChartDataForTimSort(int warmupCount, int repetitionsCount, int dataSize)
    {
        return Build1DTime(
            "Гибридная сортировка TimSort",
            "Размерность вектора v",
            "Время (мс)",
            warmupCount,
            repetitionsCount,
            dataSize,
            arr => new TimSort((int[])arr.Clone()));
    }

    public static ChartData BuildChartDataForNaivePow(int dataSize)
    {
        var b = Data[0];
        return Build1DSteps(
            "Простое (наивное) возведение в степень",
            "Размерность вектора v",
            "Количество шагов",
            dataSize,
            arr => new NaivePow(arr, b));
    }

    public static ChartData BuildChartDataForRecPow(int dataSize)
    {
        var b = Data[0];
        return Build1DSteps(
            "Рекурсивное возведение в степень",
            "Размерность вектора v",
            "Количество шагов",
            dataSize,
            arr => new RecPow(arr, b));
    }

    public static ChartData BuildChartDataForQuickPow(int dataSize)
    {
        var b = Data[0];
        return Build1DSteps(
            "Быстрое возведение в степень",
            "Размерность вектора v",
            "Количество шагов",
            dataSize,
            arr => new QuickPow(arr, b));
    }

    public static ChartData BuildChartDataForClassicQuickPow(int dataSize)
    {
        var b = Data[0];
        return Build1DSteps(
            "Классическое быстрое возведение в степень",
            "Размерность вектора v",
            "Количество шагов",
            dataSize,
            arr => new ClassicQuickPow(arr, b));
    }

    public static ChartData BuildChartDataForMatrixMultiplication(int warmupCount, int repetitionsCount, int dataSize)
    {
        var times = new List<Point>(dataSize * dataSize);

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
                        var v = Data[i * m + j];
                        data1[i, j] = v;
                        data2[j, i] = v;
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
            "n^2·m",
            "n (строки A / столбцы B)",
            "m (столбцы A / строки B)",
            "Время (мс)"
        );
    }

    public static ChartData BuildChartDataForMax(int warmupCount, int repetitionsCount, int dataSize)
    {
        return Build1DTime(
            "Линейное нахождение максимума",
            "Размерность вектора v",
            "Время (мс)",
            warmupCount,
            repetitionsCount,
            dataSize,
            arr => new Max(arr));
    }

    public static ChartData BuildChartDataForShuffle(int warmupCount, int repetitionsCount, int dataSize)
    {
        return Build1DTime(
            "Перемешивание Фишера-Йейтса",
            "Размерность вектора v",
            "Время (мс)",
            warmupCount,
            repetitionsCount,
            dataSize,
            arr => new Shuffle((int[])arr.Clone()));
    }

    public static ChartData BuildChartDataForXor(int warmupCount, int repetitionsCount, int dataSize)
    {
        // Спец-генерация входа: числа 0..i с одним пропуском
        var times = new List<Point>(dataSize);
        for (var i = 1; i <= dataSize; i++)
        {
            var arr = GenerateSequenceWithOneMissing(i);
            var task = new Xor(arr);
            Benchmark.Warmup(task, warmupCount);
            var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
            times.Add(new Point(i, time));
        }

        var (funcName, approx) = ComplexityApproximator.Approximate(times);
        return new ChartData(
            "Поиск пропущенного числа через XOR",
            times,
            approx,
            funcName,
            "Размерность вектора v",
            "Время (мс)"
        );
    }

    public static ChartData BuildChartDataForPrefixSums(int warmupCount, int repetitionsCount, int dataSize)
    {
        return Build1DTime(
            "Префиксные суммы",
            "Размерность вектора v",
            "Время (мс)",
            warmupCount,
            repetitionsCount,
            dataSize,
            arr => new PrefixSums(arr));
    }

    // -------- Универсальные билдеры --------

    private static ChartData Build1DTime(
        string title,
        string xLabel,
        string yLabel,
        int warmupCount,
        int repetitionsCount,
        int dataSize,
        Func<int[], ITask> taskFactory)
    {
        var inputs = BuildPrefixInputs(dataSize);
        var times = new List<Point>(dataSize);

        for (var i = 1; i <= dataSize; i++)
        {
            var task = taskFactory(inputs[i]);
            Benchmark.Warmup(task, warmupCount);
            var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
            times.Add(new Point(i, time));
        }

        var (funcName, approx) = ComplexityApproximator.Approximate(times);
        return new ChartData(title, times, approx, funcName, xLabel, yLabel);
    }

    private static ChartData Build1DSteps(
        string title,
        string xLabel,
        string yLabel,
        int dataSize,
        Func<int[], ITaskWithSteps> taskFactory)
    {
        var inputs = BuildPrefixInputs(dataSize);
        var steps = new List<Point>(dataSize);

        for (var i = 1; i <= dataSize; i++)
        {
            var task = taskFactory(inputs[i]);
            var step = Benchmark.MeasureStepsCount(task);
            steps.Add(new Point(i, step));
        }

        var (funcName, approx) = ComplexityApproximator.Approximate(steps);
        return new ChartData(title, steps, approx, funcName, xLabel, yLabel);
    }

    // -------- Генераторы входов --------

    /// <summary>
    /// inputs[k] -> массив из первых k чисел из Data (k от 0 до dataSize).
    /// inputs[0] = пустой массив, inputs[1] = [Data[0]], inputs[2] = [Data[0], Data[1]], ...
    /// </summary>
    private static int[][] BuildPrefixInputs(int dataSize)
    {
        var inputs = new int[dataSize + 1][];
        inputs[0] = Array.Empty<int>();
        var current = new int[dataSize];

        // Копируем ровно один раз
        Array.Copy(Data, 0, current, 0, dataSize);

        for (var k = 1; k <= dataSize; k++)
        {
            var arr = new int[k];
            Array.Copy(current, 0, arr, 0, k);
            inputs[k] = arr;
        }

        return inputs;
    }

    private static int[] GenerateSequenceWithOneMissing(int n)
    {
        // Должен получиться массив длины n из чисел 0..n, где одно число пропущено
        // Пропуск выбираем случайно из диапазона 0..n
        var miss = Random.Shared.Next(n + 1);
        var arr = new int[n];
        for (int v = 0, idx = 0; v <= n; v++)
        {
            if (v == miss) continue;
            arr[idx++] = v;
        }

        return arr;
    }

    private static class Helper
    {
        public static int[] ReadData()
        {
            var numbers = File.ReadAllText("Benchmarking/data.txt")
                .Split([' ', '\t', '\r', '\n'], StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s, CultureInfo.InvariantCulture)).ToArray();
            Console.WriteLine($"OK. Прочитано чисел: {numbers.Length}");
            return numbers;
        }
    }
}