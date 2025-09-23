using lab1.Benchmarking;

namespace lab1.Algorithms;

public class Sum(int[] data) : ITask
{
    public void Run()
    {
        var result = 0;
        foreach (var i in data)
        {
            result += i;
        }

        Blackhole.Consume(result);
    }
}