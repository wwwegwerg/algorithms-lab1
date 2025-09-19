using System;
using System.Collections.Generic;
using lab1.Algorithms;

namespace lab1.Benchmarking;

public interface IFactory
{
    ITask CreateEmpiricalTask(int[] data);
    ITask CreateTheoreticalTask(int[] data);
    string GetChartTitle();
    string GetXAxisTitle();
    string GetYAxisTitle();
}

public class ConstantTaskFactory : IFactory
{
    public ITask CreateEmpiricalTask(int[] data) => new ConstantTask(data);
    public ITask CreateTheoreticalTask(int[] data) => new ConstantTask(data);
    public string GetChartTitle() => "Постоянная функция";
    public string GetXAxisTitle() => "Размерность вектора v";
    public string GetYAxisTitle() => "Время (мс)";
}

public class SumTaskFactory : IFactory
{
    public ITask CreateEmpiricalTask(int[] data) => new SumTask(data);
    public ITask CreateTheoreticalTask(int[] data) => new SumTask(data);
    public string GetChartTitle() => "Сумма элементов";
    public string GetXAxisTitle() => "Размерность вектора v";
    public string GetYAxisTitle() => "Время (мс)";
}

public class ProductTaskFactory : IFactory
{
    public ITask CreateEmpiricalTask(int[] data) => new ProductTask(data);
    public ITask CreateTheoreticalTask(int[] data) => new ProductTask(data);
    public string GetChartTitle() => "Произведение элементов";
    public string GetXAxisTitle() => "Размерность вектора v";
    public string GetYAxisTitle() => "Время (мс)";
}

public static class Experiments
{
    private static readonly Random Rnd = new();

    private static ChartData2D BuildChartData2D(
        IBenchmark benchmark,
        int repetitionsCount,
        IFactory factory,
        int dataSize)
    {
        var empiricalTimes = new List<ExperimentResult2D>();
        var data = new List<int>();
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Rnd.Next());

            var empiricalTask = factory.CreateEmpiricalTask(data.ToArray());
            var empiricalTime = benchmark.MeasureDurationInMs(empiricalTask, repetitionsCount);

            empiricalTimes.Add(new ExperimentResult2D(i, empiricalTime));
        }
        
        var theoreticalTimes = ComplexityApproximator.Approximate(empiricalTimes);

        return new ChartData2D(
            title: factory.GetChartTitle(),
            xAxisTitle: factory.GetXAxisTitle(),
            yAxisTitle: factory.GetYAxisTitle(),
            empiricalPoints: empiricalTimes,
            theoreticalPoints: theoreticalTimes
        );
    }

    public static ChartData2D BuildChartDataForConstant(
        IBenchmark benchmark, int repetitionsCount, int dataSize)
    {
        var factory = new ConstantTaskFactory();
        return BuildChartData2D(benchmark, repetitionsCount, factory, dataSize);
    }

    public static ChartData2D BuildChartDataForSum(
        IBenchmark benchmark, int repetitionsCount, int dataSize)
    {
        var factory = new SumTaskFactory();
        return BuildChartData2D(benchmark, repetitionsCount, factory, dataSize);
    }

    public static ChartData2D BuildChartDataForProduct(
        IBenchmark benchmark, int repetitionsCount, int dataSize)
    {
        var factory = new ProductTaskFactory();
        return BuildChartData2D(benchmark, repetitionsCount, factory, dataSize);
    }
}