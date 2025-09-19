namespace lab1.Benchmarking;

public interface IBenchmark
{
    double MeasureDurationInMs(ITask task, int repetitionCount);
}