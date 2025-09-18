using System;
using System.Diagnostics;

namespace lab1.Benchmarking;

public class Benchmark : IBenchmark
{
    public double MeasureDurationInMs(ITask task, int repetitionCount)
    {
        // "прогрев" JIT'а 
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

// public class StringBuilderTask : ITask
// {
//     public void Run()
//     {
//         var sb = new StringBuilder();
//         for (var i = 0; i < 10000; i++)
//             sb.Append('a');
//         _ = sb.ToString();
//     }
// }
//     
// public class StringConstructorTask : ITask
// {
//     public void Run()
//     {
//         _ = new string('a', 10000);
//     }
// }
//
// [TestFixture]
// public class RealBenchmarkUsageSample
// {
//     [Test]
//     public void StringConstructorFasterThanStringBuilder()
//     {
//         IBenchmark benchmark = new Benchmark();
//
//         ITask stringBuilderTask = new StringBuilderTask();
//         ITask stringConstructorTask = new StringConstructorTask();
//
//         var repetitionCount = 30000;
//
//         var stringBuilderTime = benchmark.MeasureDurationInMs(stringBuilderTask, repetitionCount);
//         var stringConstructorTime = benchmark.MeasureDurationInMs(stringConstructorTask, repetitionCount);
//
//         Assert.Less(stringConstructorTime, stringBuilderTime);
//     }
// }