namespace lab1.Algorithms;

public static class NaivePow
{
    public static string Name => "Простое (наивное) возведение в степень";

    public static double Pow(double @base, int exponent)
    {
        var result = 1d;
        for (var i = 0; i < exponent; i++)
        {
            result *= @base;
        }

        return result;
    }
}