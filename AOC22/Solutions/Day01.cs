class Day01 : Solver
{
    protected override string InputFile => "Data/Day01.txt";

    protected override List<string> ReadInput() => File.ReadAllText(InputFile).Split("\n\n").ToList();

    protected override (dynamic part1, dynamic part2) Solve()
    {
        // INPUT
        List<string[]> raw = this.Input.Select(s => s.Split("\n")).ToList();
        List<int[]> src = new List<int[]>();
        foreach(string[] s in raw)
        {
            src.Add(s.Select(int.Parse).ToArray());
        }

        // PART 1
        IEnumerable<int> calories = src.Select(arr => arr.Sum()).Order().Reverse();
        int part1 = calories.ElementAt(0);

        // PART 2
        int part2 = part1 + calories.ElementAt(1) + calories.ElementAt(2);

        return (part1, part2);
    }
}