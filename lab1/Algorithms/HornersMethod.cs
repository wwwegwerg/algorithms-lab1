using lab1.Benchmarking;

namespace lab1.Algorithms;

public class HornersMethod(int[] data, double x) : ITask
{
    public void Run()
    {
        var result = 0d;
        for (var i = data.Length - 1; i >= 0; i--)
        {
            result = result * x + data[i];
        }

        Blackhole.Consume(result);
    }
}