using System;

namespace lab1.Algorithms;

public static class NaivePolynomial
{
    public static string Name => "Наивное вычисление многочлена";

    public static double Run(double[] coefficients, double x)
    {
        var result = 0d;
        for (var i = 0; i < coefficients.Length; i++)
        {
            result += coefficients[i] * Math.Pow(x, i);
        }

        return result;
    }
}