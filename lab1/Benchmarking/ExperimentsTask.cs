using System;
using System.Collections.Generic;
using System.Linq;
using lab1.Algorithms;

namespace lab1.Benchmarking;

public class Experiments
{
    static Random rnd = new();

    public static ChartData BuildChartDataForConstant(
        IBenchmark benchmark, int repetitionsCount)
    {
        var empiricalTimes = new List<ExperimentResult>();
        var theoreticalTimes = new List<ExperimentResult>();

        foreach (var fieldCount in Constants.FieldCounts)
        {
            var data = new double[fieldCount];
            for (var i = 0; i < fieldCount; i++)
            {
                data[i] = rnd.NextDouble();
            }

            var empiricalTask = new ConstantTask(data);
            // var theoreticalTask = new ConstantTask(data);

            var empiricalTime = benchmark.MeasureDurationInMs(empiricalTask, repetitionsCount);
            // var theoreticalTime = benchmark.MeasureDurationInMs(theoreticalTask, repetitionsCount * 1000);

            empiricalTimes.Add(new ExperimentResult(fieldCount, empiricalTime));
            // theoreticalTimes.Add(new ExperimentResult(fieldCount, double.Parse("1,6550199999999993E-06")));
        }

        return new ChartData
        {
            Title = "Постоянная функция",
            EmpiricalPoints = empiricalTimes,
            TheoreticalPoints = theoreticalTimes,
        };
    }

    public static ChartData BuildChartDataForSum(
        IBenchmark benchmark, int repetitionsCount)
    {
        var empiricalTimes = new List<ExperimentResult>();
        var theoreticalTimes = new List<ExperimentResult>();

        foreach (var fieldCount in Constants.FieldCounts)
        {
            var data = new double[fieldCount];
            for (var i = 0; i < fieldCount; i++)
            {
                data[i] = rnd.NextDouble();
            }

            var empiricalTask = new SumTask(data);
            // var structTask = factory.CreateStructTask(fieldCount);

            var empiricalTime = benchmark.MeasureDurationInMs(empiricalTask, repetitionsCount);
            // var structTime = benchmark.MeasureDurationInMs(structTask, repetitionsCount);

            empiricalTimes.Add(new ExperimentResult(fieldCount, empiricalTime));
            // structuresTimes.Add(new ExperimentResult(fieldCount, structTime));
        }

        return new ChartData
        {
            Title = "Сумма элементов",
            EmpiricalPoints = empiricalTimes,
            TheoreticalPoints = theoreticalTimes,
        };
    }
}