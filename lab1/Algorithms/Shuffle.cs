using System;
using lab1.Benchmarking;

namespace lab1.Algorithms;

public class Shuffle(int[] data) : ITask, ISetup
{
    private readonly int[] _original = (int[])data.Clone();
    private int[] _data = (int[])data.Clone();
    
    public void Setup()
    {
        _data = (int[])_original.Clone();
    }

    public void Run()
    {
        var rng = Random.Shared;
        for (var i = _data.Length - 1; i > 0; i--)
        {
            var j = rng.Next(i + 1);
            (_data[i], _data[j]) = (_data[j], _data[i]);
        }
    }
}