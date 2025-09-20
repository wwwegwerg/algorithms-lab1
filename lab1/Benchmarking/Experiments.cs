using System;
using System.Collections.Generic;
using lab1.Algorithms;

namespace lab1.Benchmarking;

public interface IFactory
{
    ITask CreateTask(int[] data);
    string GetChartTitle();
    string GetXAxisTitle();
    string GetYAxisTitle();
}

public class ConstantTaskFactory : IFactory
{
    public ITask CreateTask(int[] data) => new ConstantTask(data);
    public string GetChartTitle() => "Постоянная функция";
    public string GetXAxisTitle() => "Размерность вектора v";
    public string GetYAxisTitle() => "Время (мс)";
}

public class SumTaskFactory : IFactory
{
    public ITask CreateTask(int[] data) => new SumTask(data);
    public string GetChartTitle() => "Сумма элементов";
    public string GetXAxisTitle() => "Размерность вектора v";
    public string GetYAxisTitle() => "Время (мс)";
}

public class ProductTaskFactory : IFactory
{
    public ITask CreateTask(int[] data) => new ProductTask(data);
    public string GetChartTitle() => "Произведение элементов";
    public string GetXAxisTitle() => "Размерность вектора v";
    public string GetYAxisTitle() => "Время (мс)";
}

public static class Experiments
{
    private static readonly Random Rnd = new();

    private static ChartData BuildChartData2DWithDuration(
        int warmupCount,
        int repetitionsCount,
        IFactory factory,
        int dataSize)
    {
        var times = new List<Point>();
        var data = new List<int>();
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Rnd.Next());

            var task = factory.CreateTask(data.ToArray());
            var time = Benchmark.MeasureDurationInMs(task, warmupCount, repetitionsCount);
            times.Add(new Point(i, time));
        }

        var (funcName, approx) = ComplexityApproximator.Approximate(times);

        return new ChartData(
            factory.GetChartTitle(),
            times,
            approx,
            funcName,
            factory.GetXAxisTitle(),
            factory.GetYAxisTitle()
        );
    }

    public static ChartData BuildChartDataForConstant(
        int warmupCount, int repetitionsCount, int dataSize)
    {
        var factory = new ConstantTaskFactory();
        return BuildChartData2DWithDuration(warmupCount, repetitionsCount, factory, dataSize);
    }

    public static ChartData BuildChartDataForSum(
        int warmupCount, int repetitionsCount, int dataSize)
    {
        var factory = new SumTaskFactory();
        return BuildChartData2DWithDuration(warmupCount, repetitionsCount, factory, dataSize);
    }

    public static ChartData BuildChartDataForProduct(
        int warmupCount, int repetitionsCount, int dataSize)
    {
        var factory = new ProductTaskFactory();
        return BuildChartData2DWithDuration(warmupCount, repetitionsCount, factory, dataSize);
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
                        var val = Rnd.Next();
                        data1[i, j] = val;
                        data2[j, i] = val;
                    }
                }

                var task = new MatrixMultiplicationTask(new Matrix(data1), new Matrix(data2));
                var time = Benchmark.MeasureDurationInMs(task, warmupCount, repetitionsCount);
                times.Add(new Point(n, m, time));
            }
        }

        var (funcName, approx) = ComplexityApproximator.ApproximateNM(times);
        
        return new ChartData(
            "Матричное произведение",
            times,
            approx,
            funcName,
            "12312312312123",
            "m",
            "хуемя"
        );
    }
}