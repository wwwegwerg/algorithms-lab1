namespace lab1.Algorithms;

public static class ClassicQuickPow
{
    public static string Name => "Классическое быстрое возведение в степень";

    public static double Pow(double @base, int exponent)
    {
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