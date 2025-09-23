using System;
using System.Diagnostics;

namespace lab1.Benchmarking;

public static class Benchmark
{
    public static void Warmup(ITask task, int warmupCount)
    {
        for (var i = 0; i < warmupCount; i++)
        {
            if (task is ISetup s)
            {
                s.Setup();
            }

            task.Run();
        }
    }

    public static double MeasureDurationInMs(ITask task, int repetitionCount)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        var stopwatch = new Stopwatch();
        for (var i = 0; i < repetitionCount; i++)
        {
            if (task is ISetup s)
            {
                s.Setup();
            }

            stopwatch.Start();
            task.Run();
            stopwatch.Stop();
        }

        stopwatch.Stop();
        return stopwatch.Elapsed.TotalMilliseconds / repetitionCount;
    }

    public static int MeasureStepsCount(ITaskWithSteps task)
    {
        task.Run();
        return task.Steps;
    }
}