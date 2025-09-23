using lab1.Benchmarking;

namespace lab1.Algorithms;

public class Product(int[] data) : ITask
{
    public void Run()
    {
        var result = 1;
        foreach (var i in data)
        {
            result *= i;
        }

        Blackhole.Consume(result);
    }
}