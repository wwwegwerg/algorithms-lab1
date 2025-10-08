using lab1.Benchmarking;

namespace lab1.Algorithms;

public class ClassicQuickPow(int data, double x) : ITaskWithSteps
{
    public int Steps { get; private set; }

    public void Run()
    {
        var result = Pow(x, data);
        Blackhole.Consume(result);
    }

    private double Pow(double @base, int exponent)
    {
        var c = @base;
        var result = 1d;
        var k = exponent;
        Steps += 3; // присваивание
        for (;;)
        {
            Steps++; // сравнение
            if (k == 0)
            {
                break;
            }

            Steps++; // сравнение
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

            Steps += 2; // присваивание
        }

        return result;
    }
}