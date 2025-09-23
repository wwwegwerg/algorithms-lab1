using lab1.Benchmarking;

namespace lab1.Algorithms;

public class BubbleSort(int[] data) : ITask, ISetup
{
    private readonly int[] _original = (int[])data.Clone();
    private int[] _data = (int[])data.Clone();

    public void Setup()
    {
        _data = (int[])_original.Clone();
    }

    public void Run()
    {
        var n = _data.Length;
        for (var i = 0; i < n - 1; i++)
        {
            var swapped = false;
            for (var j = 0; j < n - 1 - i; j++)
            {
                if (!(_data[j] > _data[j + 1])) continue;
                Swap(_data, j, j + 1);
                swapped = true;
            }

            if (!swapped) return;
        }
    }

    private static void Swap(int[] a, int i, int j)
    {
        if (i == j) return;
        int t = a[i];
        a[i] = a[j];
        a[j] = t;
    }
}