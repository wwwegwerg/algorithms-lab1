using System;
using lab1.Benchmarking;

namespace lab1.Algorithms;

public class MatrixMultiplication(Matrix m1, Matrix m2) : ITask
{
    public void Run()
    {
        var a = m1.Content;
        var b = m2.Content;

        var aRows = m1.Rows;
        var aCols = m1.Columns; // == bRows
        var bCols = m2.Columns;

        var resArray = new int[aRows, bCols];

        for (var i = 0; i < aRows; i++)
        {
            for (var j = 0; j < bCols; j++)
            {
                var sum = 0;
                for (var k = 0; k < aCols; k++)
                {
                    sum += a[i, k] * b[k, j];
                }

                resArray[i, j] = sum;
            }
        }

        var result = new Matrix(resArray);
        Blackhole.Consume(result);
    }
}

public class Matrix
{
    public readonly int[,] Content;
    public readonly int Rows;
    public readonly int Columns;

    public Matrix(int[,] matrix)
    {
        Content = matrix;
        Rows = matrix.GetLength(0);
        Columns = matrix.GetLength(1);
    }
}