namespace lab1.Algorithms;

public static class BubbleSort
{
    public static string Name => "Сортировка пузырьком (BubbleSort)";

    public static void Run(double[] data)
    {
        var n = data.Length;
        bool swapped;
        for (var i = 0; i < n - 1; i++)
        {
            swapped = false;
            for (var j = 0; j < n - 1 - i; j++)
            {
                if (!(data[j] > data[j + 1])) continue;
                Swap(data, j, j + 1);
                swapped = true;
            }

            if (!swapped) return;
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