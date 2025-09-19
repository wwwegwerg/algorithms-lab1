using System;
using System.Collections.Generic;
using lab1.Algorithms;
using lab1.UI;

namespace lab1.Benchmarking;

public interface IFactory
{
    ITask CreateEmpiricalTask(int[] data);
    string GetChartTitle();
    string GetXAxisTitle();
    string GetYAxisTitle();
}

public class ConstantTaskFactory : IFactory
{
    public ITask CreateEmpiricalTask(int[] data) => new ConstantTask(data);
    public string GetChartTitle() => "Постоянная функция";
    public string GetXAxisTitle() => "Размерность вектора v";
    public string GetYAxisTitle() => "Время (мс)";
}

public class SumTaskFactory : IFactory
{
    public ITask CreateEmpiricalTask(int[] data) => new SumTask(data);
    public string GetChartTitle() => "Сумма элементов";
    public string GetXAxisTitle() => "Размерность вектора v";
    public string GetYAxisTitle() => "Время (мс)";
}

public class ProductTaskFactory : IFactory
{
    public ITask CreateEmpiricalTask(int[] data) => new ProductTask(data);
    public string GetChartTitle() => "Произведение элементов";
    public string GetXAxisTitle() => "Размерность вектора v";
    public string GetYAxisTitle() => "Время (мс)";
}

public static class Experiments
{
    private static readonly Random Rnd = new();

    private static ChartData2D BuildChartData2DWithDuration(
        int warmupCount,
        int repetitionsCount,
        IFactory factory,
        int dataSize)
    {
        var times = new List<ExperimentResult2D>();
        var data = new List<int>();
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Rnd.Next());

            var task = factory.CreateEmpiricalTask(data.ToArray());
            var time = Benchmark.MeasureDurationInMs(task, warmupCount, repetitionsCount);
            times.Add(new ExperimentResult2D(i, time));
        }

        var theoreticalTimes = ComplexityApproximator.Approximate(times);

        return new ChartData2D(
            title: factory.GetChartTitle(),
            xAxisTitle: factory.GetXAxisTitle(),
            yAxisTitle: factory.GetYAxisTitle(),
            empiricalPoints: times,
            theoreticalPoints: theoreticalTimes
        );
    }
    
    private static ChartData3D BuildChartData3DWithDuration(
        int warmupCount,
        int repetitionsCount,
        IFactory factory,
        int dataSize)
    {
        var times = new List<ExperimentResult3D>();
        var data = new List<int>();
        for (var i = 1; i <= dataSize; i++)
        {
            data.Add(Rnd.Next());

            var task = factory.CreateEmpiricalTask(data.ToArray());
            var time = Benchmark.MeasureDurationInMs(task, warmupCount, repetitionsCount);
            times.Add(new ExperimentResult3D(i, 0, time));
        }

        // var theoreticalTimes = ComplexityApproximator.Approximate(times);

        return new ChartData3D(
            title: factory.GetChartTitle(),
            xAxisTitle: factory.GetXAxisTitle(),
            yAxisTitle: factory.GetYAxisTitle(),
            zAxisTitle: factory.GetYAxisTitle(),
            empiricalPoints: times,
            theoreticalPoints: times
        );
    }
    
    // private static ChartData2D BuildChartData2DWithSteps(
    //     Benchmark benchmark,
    //     int warmupCount,
    //     int repetitionsCount,
    //     IFactory factory,
    //     int dataSize)
    // {
    //     var times = new List<ExperimentResult2D>();
    //     var data = new List<int>();
    //     for (var i = 1; i <= dataSize; i++)
    //     {
    //         data.Add(Rnd.Next());
    //
    //         var task = factory.CreateEmpiricalTask(data.ToArray());
    //         var time = benchmark.MeasureStepsCount(task);
    //         times.Add(new ExperimentResult2D(i, time));
    //     }
    //
    //     var theoreticalTimes = ComplexityApproximator.Approximate(times);
    //
    //     return new ChartData2D(
    //         title: factory.GetChartTitle(),
    //         xAxisTitle: factory.GetXAxisTitle(),
    //         yAxisTitle: factory.GetYAxisTitle(),
    //         empiricalPoints: times,
    //         theoreticalPoints: theoreticalTimes
    //     );
    // }

    public static ChartData2D BuildChartDataForConstant(
        int warmupCount, int repetitionsCount, int dataSize)
    {
        var factory = new ConstantTaskFactory();
        return BuildChartData2DWithDuration(warmupCount, repetitionsCount, factory, dataSize);
    }

    public static ChartData2D BuildChartDataForSum(
        int warmupCount, int repetitionsCount, int dataSize)
    {
        var factory = new SumTaskFactory();
        return BuildChartData2DWithDuration(warmupCount, repetitionsCount, factory, dataSize);
    }

    public static ChartData2D BuildChartDataForProduct(
        int warmupCount, int repetitionsCount, int dataSize)
    {
        var factory = new ProductTaskFactory();
        return BuildChartData2DWithDuration(warmupCount, repetitionsCount, factory, dataSize);
    }
    
    public static ChartData3D BuildChartDataForMatrixMultiplication(
        int warmupCount, int repetitionsCount, int dataSize)
    {
        var factory = new ProductTaskFactory();
        throw new NotImplementedException();
        return BuildChartData3DWithDuration(warmupCount, repetitionsCount, factory, dataSize);
    }
}