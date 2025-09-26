using lab1.Benchmarking;

namespace lab1.Algorithms;

public class RecPow(int[] data, double x) : ITaskWithSteps
{
    public int Steps { get; private set; }

    public void Run()
    {
        double result;
        foreach (var e in data)
        {
            result = Pow(x, e);
            Blackhole.Consume(result);
        }
    }

    private double Pow(double @base, int exponent)
    {
        Steps++; // сравнение
        if (exponent == 0)
        {
            Steps++; // f = 1
            return 1;
        }

        var result = Pow(@base, exponent / 2);
        Steps += 2; // присваивание и сравнение
        if (exponent % 2 == 1)
        {
            result *= result;
        }
        else
        {
            result *= result * @base;
        }

        Steps++; // присваивание в if'е
        return result;
    }
}