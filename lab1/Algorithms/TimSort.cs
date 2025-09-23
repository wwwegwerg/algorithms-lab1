using System;
using System.Collections.Generic;
using lab1.Benchmarking;

namespace lab1.Algorithms;

public class TimSort(int[] data) : ITask
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

        var n = _data.Length;
        var minRun = MinRunLength(n);
        var runBase = new List<int>();
        var runLen = new List<int>();

        var i = 0;
        while (i < n)
        {
            var runStart = i;

            // Найти естественный пробег (в т.ч. понижающийся — развернём)
            if (i == n - 1)
            {
                i++;
            }
            else
            {
                if (_data[i] <= _data[i + 1])
                {
                    while (i + 1 < n && _data[i] <= _data[i + 1]) i++;
                }
                else
                {
                    while (i + 1 < n && _data[i] > _data[i + 1]) i++;
                    Reverse(_data, runStart, i);
                }

                i++;
            }

            var len = i - runStart;

            // Дополним до minRun бинарной вставкой
            var force = Math.Min(minRun, n - runStart);
            if (len < force)
            {
                BinaryInsertionSort(_data, runStart, runStart + force - 1, runStart + len);
                i = runStart + force;
                len = force;
            }

            runBase.Add(runStart);
            runLen.Add(len);

            MergeCollapse(_data, runBase, runLen);
        }

        MergeForceCollapse(_data, runBase, runLen);
    }

    // ---- TimSort helpers ----

    private const int MIN_MERGE = 32;

    private static int MinRunLength(int n)
    {
        var r = 0;
        while (n >= MIN_MERGE)
        {
            r |= (n & 1);
            n >>= 1;
        }

        return n + r;
    }

    private static void BinaryInsertionSort(int[] a, int lo, int hi, int start)
    {
        if (start <= lo) start = lo + 1;
        for (var i = start; i <= hi; i++)
        {
            var x = a[i];

            // бинарный поиск позиции
            int left = lo, right = i;
            while (left < right)
            {
                var mid = left + (right - left) / 2;
                if (x < a[mid]) right = mid;
                else left = mid + 1;
            }

            // сдвиг и вставка
            for (var j = i; j > left; j--) a[j] = a[j - 1];
            a[left] = x;
        }
    }

    private static void MergeCollapse(int[] a, List<int> runBase, List<int> runLen)
    {
        while (runLen.Count > 1)
        {
            var n = runLen.Count;

            // Инварианты стека TimSort (упрощённо)
            if ((n >= 3 && runLen[n - 3] <= runLen[n - 2] + runLen[n - 1]) ||
                (n >= 4 && runLen[n - 4] <= runLen[n - 3] + runLen[n - 2]))
            {
                if (n >= 3 && runLen[n - 3] < runLen[n - 1])
                    MergeAt(a, runBase, runLen, n - 3);
                else
                    MergeAt(a, runBase, runLen, n - 2);
            }
            else if (runLen[n - 2] <= runLen[n - 1])
            {
                MergeAt(a, runBase, runLen, n - 2);
            }
            else
            {
                break;
            }
        }
    }

    private static void MergeForceCollapse(int[] a, List<int> runBase, List<int> runLen)
    {
        while (runLen.Count > 1)
        {
            var n = runLen.Count;
            var i = (n >= 3 && runLen[n - 3] < runLen[n - 1]) ? n - 3 : n - 2;
            MergeAt(a, runBase, runLen, i);
        }
    }

    private static void MergeAt(int[] a, List<int> runBase, List<int> runLen, int i)
    {
        var base1 = runBase[i];
        var len1 = runLen[i];
        var base2 = runBase[i + 1];
        var len2 = runLen[i + 1];

        // Объединение двух соседних пробегов: [base1..base1+len1-1] и [base2..base2+len2-1]
        // Копируем меньший пробег во временный буфер (стабильность сохраняется)
        if (len1 <= len2)
        {
            var tmp = new int[len1];
            Array.Copy(a, base1, tmp, 0, len1);

            var i1 = 0;
            var i2 = base2;
            var dest = base1;
            var end2 = base2 + len2;

            while (i1 < len1 && i2 < end2)
            {
                if (tmp[i1] <= a[i2]) a[dest++] = tmp[i1++]; // <= — сохраняем стабильность
                else a[dest++] = a[i2++];
            }

            while (i1 < len1) a[dest++] = tmp[i1++];
            // хвост второго пробега уже на месте
        }
        else
        {
            var tmp = new int[len2];
            Array.Copy(a, base2, tmp, 0, len2);

            var i1 = base1 + len1 - 1;
            var i2 = len2 - 1;
            var dest = base2 + len2 - 1;

            while (i1 >= base1 && i2 >= 0)
            {
                if (a[i1] > tmp[i2]) a[dest--] = a[i1--];
                else a[dest--] = tmp[i2--]; // при равенстве берём справа — стабильность
            }

            while (i2 >= 0) a[dest--] = tmp[i2--];
        }

        // Обновить стек пробегов
        runBase[i] = base1;
        runLen[i] = len1 + len2;
        runBase.RemoveAt(i + 1);
        runLen.RemoveAt(i + 1);
    }

    private static void Reverse(int[] a, int lo, int hi)
    {
        while (lo < hi) Swap(a, lo++, hi--);
    }

    private static void Swap(int[] a, int i, int j)
    {
        if (i == j) return;
        int t = a[i];
        a[i] = a[j];
        a[j] = t;
    }
}