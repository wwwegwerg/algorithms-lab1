using System.Runtime.CompilerServices;
using lab1.Benchmarking;

namespace lab1.Algorithms;

public class ProductTask(int[] data) : ITask
{
    [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
    public void Run()
    {
        var prod = 1;
        foreach (var i in data)
        {
            prod *= i;
        }
    }
}