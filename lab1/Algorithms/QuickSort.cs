namespace lab1.Algorithms;

public static class QuickSort
{
    public static string Name => "Быстрая сортировка (QuickSort)";

    public static void Run(double[] v)
    {
        if (v == null || v.Length < 2) return;
        Run(v, 0, v.Length - 1);
    }

    private static void Run(double[] a, int lo, int hi)
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

    private static int PartitionHoare(double[] a, int lo, int hi)
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

    private static void InsertionSortRange(double[] a, int lo, int hi)
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

    private static void Swap(double[] a, int i, int j)
    {
        if (i == j) return;
        double t = a[i];
        a[i] = a[j];
        a[j] = t;
    }
}