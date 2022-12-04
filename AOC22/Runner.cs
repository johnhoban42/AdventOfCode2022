public class Runner
{
    public static void Main(string[] args)
    {
        int day = Int32.Parse(args[0]);
        (dynamic part1, dynamic part2) = day switch
        {
            1 => new Day01().SolveTimed(),
            2 => new Day02().SolveTimed(),
            3 => new Day03().SolveTimed(),
            4 => new Day04().SolveTimed(),
            _ => throw new NotSupportedException()
        };
        Console.WriteLine($"PART 1: {part1}");
        Console.WriteLine($"PART 2: {part2}");
    }
}