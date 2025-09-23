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
        var c = @base;
        var k = exponent;
        Steps += 2; // присваивание
        double result;
        if (k % 2 == 1)
        {
            result = 1;
        }
        else
        {
            result = c;
        }

        Steps++; // присваивание в if'е
        for (;;)
        {
            k /= 2;
            c *= c;
            Steps += 3; // присваивание и срвнение
            if (k % 2 == 1)
            {
                result *= c;
                Steps++;
            }

            Steps++; // сравнение
            if (k == 0)
            {
                break;
            }
        }

        return result;
    }
}