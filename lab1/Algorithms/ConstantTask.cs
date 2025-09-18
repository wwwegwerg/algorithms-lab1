using System.Runtime.CompilerServices;
using lab1.Benchmarking;

namespace lab1.Algorithms;

public class ConstantTask : ITask
{
    private readonly double[] data;

    public ConstantTask(double[] data)
    {
        this.data = data;
    }

    [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
    public void Run()
    {
    }
}