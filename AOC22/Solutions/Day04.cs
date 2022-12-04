class Day04 : Solver
{
    protected override string InputFile => "Data/Day04.txt";

    protected override (dynamic part1, dynamic part2) Solve()
    {
        // INPUT
        List<string[]> raw = this.Input.Select(s => s.Split(',', '-')).ToList();
        List<int[]> src = new List<int[]>();
        foreach(string[] s in raw)
        {
            src.Add(s.Select(int.Parse).ToArray());
        }

        // PART 1
        // Check if both endpoints of one range are contained by the endpoints of the other range
        int part1 = 0;
        foreach (int[] arr in src)
        {
            if ((arr[0] <= arr[2] && arr[3] <= arr[1]) || (arr[2] <= arr[0] && arr[1] <= arr[3]))
            {
                part1++;
            }
        }

        // PART 2
        // Find overlaps by eliminating pairs where one range ends before the other starts
        int part2 = 0;
        foreach (int[] arr in src)
        {
            if (!(arr[3] < arr[0] || arr[1] < arr[2]))
            {
                part2++;
            }
        }

        return (part1, part2);
    }
}