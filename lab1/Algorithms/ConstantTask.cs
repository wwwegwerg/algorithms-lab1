using System.Runtime.CompilerServices;
using lab1.Benchmarking;

namespace lab1.Algorithms;

public class ConstantTask(int[] data) : ITask
{
    private readonly int[] data = data;

    [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
    public void Run()
    {
    }
}