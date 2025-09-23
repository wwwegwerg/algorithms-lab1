using System;
using lab1.Benchmarking;

namespace lab1.Algorithms;

public class NaivePolynomial(int[] data, double x) : ITask
{
    public void Run()
    {
        var result = 0d;
        for (var i = 0; i < data.Length; i++)
        {
            result += data[i] * Math.Pow(x, i);
        }

        Blackhole.Consume(result);
    }
}