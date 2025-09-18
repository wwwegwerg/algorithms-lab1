namespace lab1.Algorithms;

public static class QuickPow
{
    public static string Name => "Быстрое возведение в степень";
    
    public static double Pow(double @base, int exponent)
    {
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