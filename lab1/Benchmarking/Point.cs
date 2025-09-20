namespace lab1.Benchmarking;

public class Point
{
    public readonly double XAxis;
    public readonly double YAxis;
    public readonly double ZAxis;

    public Point(double xAxis, double yAxis, double zAxis = 0)
    {
        XAxis = xAxis;
        YAxis = yAxis;
        ZAxis = zAxis;
    }

    public override string ToString()
    {
        return $"{XAxis} {YAxis} {ZAxis}";
    }
}