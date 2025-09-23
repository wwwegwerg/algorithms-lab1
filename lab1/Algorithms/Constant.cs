using lab1.Benchmarking;

namespace lab1.Algorithms;

public class Constant(int[] data) : ITask
{
    public void Run()
    {
        var result = 1;
        Blackhole.Consume(result);
    }
}