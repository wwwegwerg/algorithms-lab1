using System.Runtime.CompilerServices;
using lab1.Benchmarking;

namespace lab1.Algorithms;

public class ProductTask : ITask
{
    private readonly double[] data;

    public ProductTask(double[] data)
    {
        this.data = data;
    }

    [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
    public void Run()
    {
        var prod = 1d;
        foreach (var i in data)
        {
            prod *= i;
        }
    }
}