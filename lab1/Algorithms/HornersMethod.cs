namespace lab1.Algorithms;

public static class HornersMethod
{
    public static string Name => "Метод Горнера";

    public static double Run(double[] coefficients, double x)
    {
        var result = 0d;
        for (var i = coefficients.Length - 1; i >= 0; i--)
        {
            result = result * x + coefficients[i];
        }

        return result;
    }
}