using System;
using System.Diagnostics;

namespace lab1.Benchmarking;

public class Benchmark : IBenchmark
{
    public double MeasureDurationInMs(ITask task, int repetitionCount)
    {
        // "прогрев" JIT
        task.Run();
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
}