class Day05 : Solver
{
    protected override string InputFile => "Data/Day05.txt";

    protected override (dynamic part1, dynamic part2) Solve()
    {
        /* INPUT */
        // Get dimensions
        int blankRow = this.Input.IndexOf("");
        int nCols = 1 + this.Input.ElementAt(blankRow - 1).Length / 4;
        // Build each crate stack by parsing every fourth char of each row
        List<string> crates = new List<string>();
        for (int col = 0; col < nCols; col++)
        {
            crates.Add(
                String.Concat(
                    this.Input.GetRange(0, blankRow - 1).Select(s => s.ElementAt(1 + 4*col))
                    .Reverse()
                ).Trim()
            );
        }
        // Parse moves
        List<int[]> moves = this.Input.GetRange(blankRow + 1, this.Input.Count - (blankRow + 1))
            .Select(s => s.Split(" "))
            .Select(arr => new int[] { Int32.Parse(arr[1]), Int32.Parse(arr[3]), Int32.Parse(arr[5]) })
            .ToList();

        /* PART 1 */
        List<string> crates1 = new List<string>(crates);
        foreach (int[] move in moves)
        {
            (int count, int from, int to) = (move[0], move[1] - 1, move[2] - 1);
            int removeIndex = crates1[from].Length - count;
            crates1[to] += String.Concat(crates1[from][removeIndex..].Reverse());
            crates1[from] = crates1[from].Remove(removeIndex);
        }
        string part1 = String.Concat(crates1.Select(s => s.Last()));

        /* PART 2 */
        List<string> crates2 = new List<string>(crates);
        foreach (int[] move in moves)
        {
            (int count, int from, int to) = (move[0], move[1] - 1, move[2] - 1);
            int removeIndex = crates2[from].Length - count;
            crates2[to] += String.Concat(crates2[from][removeIndex..]);
            crates2[from] = crates2[from].Remove(removeIndex);
        }
        string part2 = String.Concat(crates2.Select(s => s.Last()));

        return (part1, part2);
    }
}