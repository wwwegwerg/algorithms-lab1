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
        var result = 1d;
        var k = 0;
        Steps += 2; // присваивание
        for (;;)
        {
            Steps++; // сравнение
            if (k < exponent)
            {
                result *= @base;
                k++;
                Steps += 2; // умножение и прибавление
            }
            else
            {
                break;
            }
        }

        return result;
    }
}