using System.Text;

namespace lab1.Algorithms;

public static class MatrixMultiplication
{
    public static string Name => "Перемножение матриц";

    public static Matrix Run(Matrix m1, Matrix m2)
    {
        var result = new Matrix(new int[m1.Rows, m2.Columns]);
        for (var i = 0; i < result.Rows; i++)
        {
            for (var j = 0; j < result.Columns; j++)
            {
                for (var k = 0; k < m2.Rows; k++)
                {
                    result.Content[i, j] += m1.Content[i, k] * m2.Content[k, j];
                }
            }
        }

        return result;
    }
}

public class Matrix(int[,] matrix)
{
    public readonly int[,] Content = matrix;
    public int Rows => Content.GetUpperBound(0) + 1;
    public int Columns => Content.Length / Rows;

    public override string ToString()
    {
        var result = new StringBuilder();
        for (var i = 0; i < Rows; i++)
        {
            for (var j = 0; j < Columns; j++)
            {
                result.Append($"{Content[i, j]} ");
            }

            result.AppendLine();
        }

        return result.ToString().TrimEnd();
    }
}