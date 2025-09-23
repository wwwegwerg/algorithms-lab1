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

    public static ChartData BuildChartDataForConstant(
        int warmupCount, int repetitionsCount, int dataSize)
    {
        var times = new List<Point>();
        var data = new List<int>();
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Data[i]);

            var task = new Constant(data.ToArray());
            Benchmark.Warmup(task, warmupCount);
            var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
            times.Add(new Point(i, time));
        }

        var (funcName, approx) = ComplexityApproximator.Approximate(times);

        return new ChartData(
            "Постоянная функция",
            times,
            approx,
            funcName,
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
            data.Add(Data[i]);

            var task = new Sum(data.ToArray());
            Benchmark.Warmup(task, warmupCount);
            var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
            times.Add(new Point(i, time));
        }

        var (funcName, approx) = ComplexityApproximator.Approximate(times);

        return new ChartData(
            "Сумма элементов",
            times,
            approx,
            funcName,
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
            data.Add(Data[i]);

            var task = new Product(data.ToArray());
            Benchmark.Warmup(task, warmupCount);
            var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
            times.Add(new Point(i, time));
        }

        var (funcName, approx) = ComplexityApproximator.Approximate(times);

        return new ChartData(
            "Произведение элементов",
            times,
            approx,
            funcName,
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
            data.Add(Data[i]);

            var task = new NaivePolynomial(data.ToArray(), 1.5);
            Benchmark.Warmup(task, warmupCount);
            var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
            times.Add(new Point(i, time));
        }

        var (funcName, approx) = ComplexityApproximator.Approximate(times);

        return new ChartData(
            "Наивное вычисление многочлена",
            times,
            approx,
            funcName,
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
            data.Add(Data[i]);

            var task = new HornersMethod(data.ToArray(), 1.5);
            Benchmark.Warmup(task, warmupCount);
            var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
            times.Add(new Point(i, time));
        }

        var (funcName, approx) = ComplexityApproximator.Approximate(times);

        return new ChartData(
            "Метод Горнера",
            times,
            approx,
            funcName,
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
            data.Add(Data[i]);

            var task = new BubbleSort(data.ToArray());
            Benchmark.Warmup(task, warmupCount);
            var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
            times.Add(new Point(i, time));
        }

        var (funcName, approx) = ComplexityApproximator.Approximate(times);

        return new ChartData(
            "Сортировка пузырьком (BubbleSort)",
            times,
            approx,
            funcName,
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
            data.Add(Data[i]);

            var task = new QuickSort(data.ToArray());
            Benchmark.Warmup(task, warmupCount);
            var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
            times.Add(new Point(i, time));
        }

        var (funcName, approx) = ComplexityApproximator.Approximate(times);

        return new ChartData(
            "Быстрая сортировка (QuickSort)",
            times,
            approx,
            funcName,
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
            data.Add(Data[i]);

            var task = new TimSort(data.ToArray());
            Benchmark.Warmup(task, warmupCount);
            var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
            times.Add(new Point(i, time));
        }

        var (funcName, approx) = ComplexityApproximator.Approximate(times);

        return new ChartData(
            "Гибридная сортировка TimSort",
            times,
            approx,
            funcName,
            "Размерность вектора v",
            "Время (мс)"
        );
    }

    public static ChartData BuildChartDataForNaivePow(int dataSize)
    {
        var steps = new List<Point>();
        var data = new List<int>();
        var b = Data[0];
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Data[i]);

            var task = new NaivePow(data.ToArray(), b);
            var step = Benchmark.MeasureStepsCount(task);
            steps.Add(new Point(i, step));
        }

        var (funcName, approx) = ComplexityApproximator.Approximate(steps);

        return new ChartData(
            "Простое (наивное) возведение в степень",
            steps,
            approx,
            funcName,
            "Размерность вектора v",
            "Количество шагов"
        );
    }

    public static ChartData BuildChartDataForRecPow(int dataSize)
    {
        var steps = new List<Point>();
        var data = new List<int>();
        var b = Data[0];
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Data[i]);

            var task = new RecPow(data.ToArray(), b);
            var step = Benchmark.MeasureStepsCount(task);
            steps.Add(new Point(i, step));
        }

        var (funcName, approx) = ComplexityApproximator.Approximate(steps);

        return new ChartData(
            "Рекурсивное возведение в степень",
            steps,
            approx,
            funcName,
            "Размерность вектора v",
            "Количество шагов"
        );
    }

    public static ChartData BuildChartDataForQuickPow(int dataSize)
    {
        var steps = new List<Point>();
        var data = new List<int>();
        var b = Data[0];
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Data[i]);

            var task = new QuickPow(data.ToArray(), b);
            var step = Benchmark.MeasureStepsCount(task);
            steps.Add(new Point(i, step));
        }

        var (funcName, approx) = ComplexityApproximator.Approximate(steps);

        return new ChartData(
            "Быстрое возведение в степень",
            steps,
            approx,
            funcName,
            "Размерность вектора v",
            "Количество шагов"
        );
    }

    public static ChartData BuildChartDataForClassicQuickPow(int dataSize)
    {
        var steps = new List<Point>();
        var data = new List<int>();
        var b = Data[0];
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Data[i]);

            var task = new ClassicQuickPow(data.ToArray(), b);
            var step = Benchmark.MeasureStepsCount(task);
            steps.Add(new Point(i, step));
        }

        var (funcName, approx) = ComplexityApproximator.Approximate(steps);

        return new ChartData(
            "Классическое быстрое возведение в степень",
            steps,
            approx,
            funcName,
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
                        var val = Data[i];
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

    public static ChartData BuildChartDataForMax(
        int warmupCount, int repetitionsCount, int dataSize)
    {
        var times = new List<Point>();
        var data = new List<int>();
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Data[i]);
    
            var task = new Max(data.ToArray());
            Benchmark.Warmup(task, warmupCount);
            var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
            times.Add(new Point(i, time));
        }
    
        var (funcName, approx) = ComplexityApproximator.Approximate(times);
    
        return new ChartData(
            "Линейное нахождение максимума",
            times,
            approx,
            funcName,
            "Размерность вектора v",
            "Время (мс)"
        );
    }
    
    public static ChartData BuildChartDataForShuffle(
        int warmupCount, int repetitionsCount, int dataSize)
    {
        var times = new List<Point>();
        var data = new List<int>();
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Data[i]);
    
            var task = new Shuffle(data.ToArray());
            Benchmark.Warmup(task, warmupCount);
            var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
            times.Add(new Point(i, time));
        }
    
        var (funcName, approx) = ComplexityApproximator.Approximate(times);
    
        return new ChartData(
            "Перемешивание Фишера-Йейтса",
            times,
            approx,
            funcName,
            "Размерность вектора v",
            "Время (мс)"
        );
    }
    
    public static ChartData BuildChartDataForXor(
        int warmupCount, int repetitionsCount, int dataSize)
    {
        var times = new List<Point>();
        var data = new List<int>();
        var rnd = new Random();
        for (var i = 1; i <= dataSize; i++)
        {
            data.Clear();
            for (var j = 0; j <= i; j++)
            {
                data.Add(j);
            }
            
            var idxToRemove = rnd.Next(i);
            data.RemoveAt(idxToRemove);
    
            var task = new Xor(data.ToArray());
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
    
    public static ChartData BuildChartDataForPrefixSums(
        int warmupCount, int repetitionsCount, int dataSize)
    {
        var times = new List<Point>();
        var data = new List<int>();
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Data[i]);
    
            var task = new PrefixSums(data.ToArray());
            Benchmark.Warmup(task, warmupCount);
            var time = Benchmark.MeasureDurationInMs(task, repetitionsCount);
            times.Add(new Point(i, time));
        }
    
        var (funcName, approx) = ComplexityApproximator.Approximate(times);
    
        return new ChartData(
            "Префиксные суммы",
            times,
            approx,
            funcName,
            "Размерность вектора v",
            "Время (мс)"
        );
    }

    private static class Helper
    {
        public static int[] ReadData()
        {
            try
            {
                var numbers = File
                    .ReadAllText("Benchmarking/data.txt")
                    .Split([' ', '\t', '\r', '\n'], StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => int.Parse(s, CultureInfo.InvariantCulture))
                    .ToArray();

                Console.WriteLine($"OK. Прочитано чисел: {numbers.Length}");
                return numbers;
            }
            catch (FileNotFoundException)
            {
                Console.Error.WriteLine("Файл data.txt не найден в текущей папке.");
            }
            catch (FormatException)
            {
                Console.Error.WriteLine("В файле встречено нецелое число или неверный формат.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Ошибка: {ex.Message}");
            }

            return [];
        }
    }
}