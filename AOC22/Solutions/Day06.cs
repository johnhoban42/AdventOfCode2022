class Day06 : Solver
{
    protected override string InputFile => "Data/Day06.txt";

    private int GetCharactersRead(string data, int pktLength)
    {
        int pktStart = 0;
        while (data.Substring(pktStart, pktLength).Distinct().Count() < pktLength)
        {
            pktStart++;
        }
        return pktStart + pktLength;
    }

    protected override (dynamic part1, dynamic part2) Solve()
    {
        string src = this.Input.ElementAt(0);
        int part1 = GetCharactersRead(src, 4);
        int part2 = GetCharactersRead(src, 14);
        return (part1, part2);
    }
}