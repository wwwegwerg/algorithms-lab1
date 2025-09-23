using lab1.Benchmarking;

namespace lab1.Algorithms;

public class PrefixSums(int[] data) : ITask
{
    public void Run()
    {
        var n = data.Length;
        var result = new int[n + 1];
        for (var i = 0; i < n; i++)
        {
            result[i + 1] = result[i] + data[i];
        }

        Blackhole.Consume(result);
    }
}