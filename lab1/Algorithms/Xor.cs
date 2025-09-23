using lab1.Benchmarking;

namespace lab1.Algorithms;

public class Xor(int[] data) : ITask
{
    public void Run()
    {
        var result = 0;

        // XOR по всем значениям от 0 до n (где n = длина массива)
        for (var i = 0; i <= data.Length; i++)
            result ^= i;

        // XOR по всем элементам массива
        foreach (var v in data)
            result ^= v;

        Blackhole.Consume(result);
    }
}