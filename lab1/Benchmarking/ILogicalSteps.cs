namespace lab1.Benchmarking;

public interface ILogicalSteps : ITask
{
    int Steps { get; }
    void ResetSteps();
}