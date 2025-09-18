using System.Collections.Generic;
using System.Runtime.CompilerServices;
using lab1.Benchmarking;

namespace lab1.Algorithms;

public class SumTask : ITask
{
    private readonly double[] data;

    public SumTask(double[] data)
    {
        this.data = data;
    }

    [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
    public void Run()
    {
        var result = 0d;
        foreach (var i in data)
        {
            result += i;
        }
    }
}