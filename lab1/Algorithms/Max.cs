using lab1.Benchmarking;

namespace lab1.Algorithms;

public class Max(int[] data) : ITask
{
    public void Run()
    {
        var result = data[0];
        foreach (var el in data)
        {
            if (el > result)
            {
                result = el;
            }
        }

        Blackhole.Consume(result);
    }
}