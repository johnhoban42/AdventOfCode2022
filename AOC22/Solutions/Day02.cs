class Day02 : Solver
{
    protected override string InputFile => "Data/Day02.txt";

    protected override (dynamic part1, dynamic part2) Solve()
    {
        // INPUT
        List<string[]> src = this.Input.Select(s => s.Split(" ")).ToList();

        // PART 1
        int part1 = 0;
        foreach(string[] round in src)
        {
            // Each expression = rock/paper/scissors value + win/lose/draw value
            part1 += round[1] switch
            {
                "X" => 1 + round[0] switch { "A" => 3, "B" => 0, "C" => 6, _ => 0 },
                "Y" => 2 + round[0] switch { "A" => 6, "B" => 3, "C" => 0, _ => 0 },
                "Z" => 3 + round[0] switch { "A" => 0, "B" => 6, "C" => 3, _ => 0 },
                _ => 0
            };
        }

        // PART 2
        int part2 = 0;
        foreach(string[] round in src)
        {
            // Each expression = win/lose/draw value + rock/paper/scissors value
            part2 += round[1] switch
            {
                "X" => 0 + round[0] switch { "A" => 3, "B" => 1, "C" => 2, _ => 0 },
                "Y" => 3 + round[0] switch { "A" => 1, "B" => 2, "C" => 3, _ => 0 },
                "Z" => 6 + round[0] switch { "A" => 2, "B" => 3, "C" => 1, _ => 0 },
                _ => 0
            };
        }

        return (part1, part2);
    }
}