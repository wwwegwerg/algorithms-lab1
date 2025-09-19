using System;
using System.Diagnostics;

namespace lab1.Benchmarking;

public static class Benchmark
{
    public static double MeasureDurationInMs(ITask task, int warmupCount, int repetitionCount)
    {
        // "прогрев" JIT
        for (var i = 0; i < warmupCount; i++)
        {
            task.Run();
        }

        // выключение сборщика мусора на момент рана
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        var stopwatch = Stopwatch.StartNew();
        for (var i = 0; i < repetitionCount; i++)
            task.Run();
        stopwatch.Stop();
        return stopwatch.Elapsed.TotalMilliseconds / repetitionCount;
    }
    
    public static int MeasureStepsCount<T>(T task) where T : ITask, ILogicalSteps
    {
        task.Run();
        return task.Steps;
    }
}