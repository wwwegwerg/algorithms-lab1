namespace lab1.Algorithms;

public static class RecPow
{
    public static string Name => "Рекурсивное возведение в степень";

    public static double Pow(double @base, int exponent)
    {
        if (exponent == 0)
        {
            return 1;
        }

        var result = Pow(@base, exponent / 2);
        result = exponent % 2 == 1 ? result * result * @base : result * result;
        return result;
    }
}