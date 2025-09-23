using lab1.Benchmarking;

namespace lab1.Algorithms;

public class QuickPow(int[] data, double x) : ITaskWithSteps
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
        var c = @base;
        var k = exponent;
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

        return result;
    }
}