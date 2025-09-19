using System.Runtime.CompilerServices;
using lab1.Benchmarking;

namespace lab1.Algorithms;

public class SumTask(int[] data) : ITask
{
    [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
    public void Run()
    {
        var result = 0;
        foreach (var i in data)
        {
            result += i;
        }
    }
}