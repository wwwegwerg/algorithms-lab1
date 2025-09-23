using System.Runtime.CompilerServices;

namespace lab1.Benchmarking;

public static class Blackhole<T>
{
    public static T? Value;
}

public static class Blackhole
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Consume<T>(T value) => Blackhole<T>.Value = value;
}