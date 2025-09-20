using System;
using lab1.Benchmarking;

namespace lab1.Algorithms;

public class QuickPowTask(int b, int e) : ITask, ILogicalSteps
{
    public void Run()
    {
        var c = b;
        var k = e;
        var result = k % 2 == 1 ? c : 1;
        while (k != 0)
        {
            k /= 2;
            c *= c;
            if (k % 2 == 1)
            {
                result *= c;
            }
        }

        GC.KeepAlive(result);
    }

    public int Steps { get; }

    public void ResetSteps()
    {
        throw new System.NotImplementedException();
    }
}