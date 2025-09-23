using lab1.Benchmarking;

namespace lab1.Algorithms;

public class QuickSort(int[] data) : ITask, ISetup
{
    private readonly int[] _original = (int[])data.Clone();
    private int[] _data = (int[])data.Clone();

    public void Setup()
    {
        _data = (int[])_original.Clone();
    }

    public void Run()
    {
        if (_data == null || _data.Length < 2) return;
        Run(_data, 0, _data.Length - 1);
    }

    private static void Run(int[] a, int lo, int hi)
    {
        const int INSERTION_SORT_THRESHOLD = 16;

        while (lo < hi)
        {
            if (hi - lo + 1 <= INSERTION_SORT_THRESHOLD)
            {
                InsertionSortRange(a, lo, hi);
                return;
            }

            var p = PartitionHoare(a, lo, hi);

            // Хвостовая рекурсия — сначала меньшая часть
            if (p - lo < hi - (p + 1))
            {
                Run(a, lo, p);
                lo = p + 1;
            }
            else
            {
                Run(a, p + 1, hi);
                hi = p;
            }
        }
    }

    private static int PartitionHoare(int[] a, int lo, int hi)
    {
        var pivot = a[(lo + hi) / 2];
        var i = lo - 1;
        var j = hi + 1;

        while (true)
        {
            do
            {
                i++;
            } while (a[i] < pivot);

            do
            {
                j--;
            } while (a[j] > pivot);

            if (i >= j) return j;
            Swap(a, i, j);
        }
    }

    private static void InsertionSortRange(int[] a, int lo, int hi)
    {
        for (var i = lo + 1; i <= hi; i++)
        {
            var x = a[i];
            var j = i - 1;
            while (j >= lo && a[j] > x)
            {
                a[j + 1] = a[j];
                j--;
            }

            a[j + 1] = x;
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