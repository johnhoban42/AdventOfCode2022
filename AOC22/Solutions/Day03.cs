class Day03 : Solver
{
    protected override string InputFile => "Data/Day03.txt";

    // Perform the set intersection of the two halves of a rucksack
    private char GetDuplicate(string sack)
    {
        int mid = sack.Length / 2;
        return sack[..mid].ToHashSet()
            .Intersect(sack[mid..].ToHashSet())
            .First();
    }

    // Get char priority
    private int GetPriority(char c)
    {
        if (Char.IsUpper(c))
        {
            return c - 'A' + 27;
        }
        return c - 'a' + 1;
    }

    protected override (dynamic part1, dynamic part2) Solve()
    {
        // PART 1
        int part1 = this.Input.Select(GetDuplicate).Select(GetPriority).Sum();

        // PART 2
        // Perform the set intersection of each group of 3 rucksacks
        List<char> badges = new List<char>();
        for (int i = 0; i < this.Input.Count; i += 3)
        {
            badges.Add(
                this.Input[i].ToHashSet().Intersect(
                    this.Input[i+1].ToHashSet().Intersect(
                        this.Input[i+2].ToHashSet()
                    )
                ).First()
            );
        }
        int part2 = badges.Select(GetPriority).Sum();

        return (part1, part2);
    }
}