using lab1.Benchmarking;

namespace lab1.Algorithms;

public class ClassicQuickPow(int[] data, double x) : ITaskWithSteps
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
        var result = 1d;
        var k = exponent;
        while (k != 0)
        {
            if (k % 2 == 0)
            {
                c *= c;
                k /= 2;
            }
            else
            {
                result *= c;
                k--;
            }
        }

        return result;
    }
}