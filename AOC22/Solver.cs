public abstract class Solver
{
    protected List<String> Input { get; }
    protected abstract string InputFile { get; }
    protected abstract (dynamic part1, dynamic part2) Solve();

    protected virtual List<String> ReadInput() => File.ReadAllLines(InputFile).ToList();

    public (dynamic part1, dynamic part2) SolveTimed()
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        (var part1, var part2) = Solve();
        watch.Stop();
        string time = watch.Elapsed.TotalMilliseconds.ToString("0.0000");
        Console.WriteLine($"TIME: {time} ms");
        return (part1, part2);
    }

    public Solver()
    {
        Input = ReadInput();
    }
}
