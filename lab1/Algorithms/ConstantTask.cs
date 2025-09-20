using System;
using lab1.Benchmarking;

namespace lab1.Algorithms;

public class ConstantTask(int[] data) : ITask
{
    public void Run()
    {
        var result = 1;
        GC.KeepAlive(result);
    }
}