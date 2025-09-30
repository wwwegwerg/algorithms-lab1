using System.Threading;
using lab1.Benchmarking;

namespace lab1.Algorithms;

public class Constant(int[] data) : ITask
{
    public void Run()
    {
        Thread.Sleep(1);
        var result = 1;
        Blackhole.Consume(result);
    }
}