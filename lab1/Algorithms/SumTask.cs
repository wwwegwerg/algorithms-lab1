using System;
using lab1.Benchmarking;

namespace lab1.Algorithms;

public class SumTask(int[] data) : ITask
{
    public void Run()
    {
        var result = 0;
        foreach (var i in data)
        {
            result += i;
        }

        GC.KeepAlive(result);
    }
}