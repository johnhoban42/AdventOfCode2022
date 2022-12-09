class Day09 : Solver
{
    protected override string InputFile => "Data/Day09.txt";

    // Return the number of positions the rope's tail visits
    private int TrackTail(IEnumerable<(char, int)> moves, int knots)
    {
        HashSet<(int, int)> tailVisits = new HashSet<(int, int)>();
        int[,] rope = new int[knots, 2]; // (x, y)

        // Always move the first knot along direction "dir"
        // Only move the nth knot if its x or y coordinate is more than 1 unit away from the (n-1)th knot
        // When moving the nth knot, move its x/y coordinate 1 unit towards the (n-1)th x/y coordinate if not equal
        foreach ((char dir, int n) in moves)
        {
            for (int i = 0; i < n; i++)
            {
                switch (dir)
                {
                    case 'L':
                        rope[0, 0]--;
                        break;

                    case 'R':
                        rope[0, 0]++;
                        break;

                    case 'U':
                        rope[0, 1]++;
                        break;

                    case 'D':
                        rope[0, 1]--;
                        break;
                }
                for (int k = 1; k < knots; k++)
                {
                    if ((Math.Abs(rope[k-1, 0] - rope[k, 0]) > 1) || (Math.Abs(rope[k-1, 1] - rope[k, 1]) > 1))
                    {
                        rope[k, 0] += Math.Sign(rope[k-1, 0] - rope[k, 0]);
                        rope[k, 1] += Math.Sign(rope[k-1, 1] - rope[k, 1]);
                    }
                }
                tailVisits.Add((rope[knots-1, 0], rope[knots-1, 1]));
            }
        }
        return tailVisits.Count();
    }

    protected override (dynamic part1, dynamic part2) Solve()
    {
        IEnumerable<(char, int)> moves = this.Input.Select(s => (s[0], Int32.Parse(s.Substring(2))));
        int part1 = TrackTail(moves, 2);
        int part2 = TrackTail(moves, 10);
        return (part1, part2);
    }
}