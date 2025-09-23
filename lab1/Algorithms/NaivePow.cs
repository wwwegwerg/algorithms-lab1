using lab1.Benchmarking;

namespace lab1.Algorithms;

public class NaivePow(int[] data, double x) : ITaskWithSteps
{
    public int Steps { get; private set; }

    public void Run()
    {
        var result = x;
        foreach (var e in data)
        {
            result = Pow(result, e);
        }

        Blackhole.Consume(result);
    }

    private double Pow(double @base, int exponent)
    {
        Steps++;
        var result = 1d;
        for (var i = 0; i < exponent; i++)
        {
            result *= @base;
        }

        return result;
    }
}