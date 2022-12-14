class Day12 : Solver
{
    protected override string InputFile => "Data/Day12.txt";

    // A* heuristic function: Manhattan distance to end
    private int h((int, int) pos, (int, int) end)
    {
        return Math.Abs(end.Item1 - pos.Item1) + Math.Abs(end.Item2 - pos.Item2);
    }

    // Initialize a 2D array with all values := infinity
    private int[][] InitializeInfinityMatrix(int rows, int cols)
    {
        return Enumerable.Range(0, rows).Select(
            i => Enumerable.Range(0, cols).Select(i => Int32.MaxValue).ToArray()
        ).ToArray();
    }

    private int FindShortestPath(char[][] map, (int, int) start, (int, int) end)
    {
        // Map dimensions
        int H = map.Length;
        int W = map[0].Length;
        // Run A* on the map. Node v connects to neighbor v* if (v* - v) <= 1
        // See here for notation: https://en.wikipedia.org/wiki/A*_search_algorithm#Description
        int[][] g = InitializeInfinityMatrix(H, W);
        g[start.Item1][start.Item2] = 0;
        int[][] f = InitializeInfinityMatrix(H, W);
        f[start.Item1][start.Item2] = h(start, end);
        // Use a priority queue for finding the closest node and a set to determine membership in the seen group
        var seen = new PriorityQueue<(int, int), int>();
        var seenSet = new HashSet<(int, int)>();
        seen.Enqueue(start, h(start, end));
        seenSet.Add(start);
        
        while (seen.Count > 0)
        {
            // Get closest discovered node
            (int r, int c) = seen.Dequeue();
            seenSet.Remove((r, c));
            // Check which v* we can move to
            var neighbors = new List<(int, int)>();
            if (r > 0 && (map[r-1][c] - map[r][c] <= 1))
            {
                neighbors.Add((r-1, c));
            }
            if (r < H-1 && (map[r+1][c] - map[r][c] <= 1))
            {
                neighbors.Add((r+1, c));
            }
            if (c > 0 && (map[r][c-1] - map[r][c] <= 1))
            {
                neighbors.Add((r, c-1));
            }
            if (c < W-1 && (map[r][c+1] - map[r][c] <= 1))
            {
                neighbors.Add((r, c+1));
            }
            // Determine if this is the shortest possible path to each reachable v*
            foreach ((int rn, int cn) in neighbors)
            {
                if (g[r][c] + 1 < g[rn][cn])
                {
                    g[rn][cn] = g[r][c] + 1;
                    f[rn][cn] = g[rn][cn] + h((rn, cn), end);
                    if (!seenSet.Contains((rn, cn)))
                    {
                        seenSet.Add((rn, cn));
                        seen.Enqueue((rn, cn), f[rn][cn]);
                    }
                }
            }
        }
        // h(end) = 0, so f(end) = g(end) + h(end) = g(end)
        return g[end.Item1][end.Item2];
    }

    protected override (dynamic part1, dynamic part2) Solve()
    {
        /* INPUT */
        // Find start and end coordinates
        char[][] map = this.Input.Select(s => s.ToCharArray()).ToArray();
        (int, int) start = (-1, -1);
        (int, int) end = (-1, -1);
        for (int r = 0; r < this.Input.Count; r++)
        {
            int cs = this.Input[r].IndexOf('S');
            int ce = this.Input[r].IndexOf('E');
            if (cs != -1)
            {
                start = (r, cs);
            }
            if (ce != -1)
            {
                end = (r, ce);
            }
            if (start != (-1, -1) && end != (-1, -1))
            {
                break;
            }
        }
        // Replace start and end with their real heights
        map[start.Item1][start.Item2] = 'a';
        map[end.Item1][end.Item2] = 'z';

        /* PART 1 */
        int part1 = FindShortestPath(map, start, end);

        /* PART 2 */
        // Get all 'a' starting points
        var starts = new List<(int, int)>();
        for (int r = 0; r < this.Input.Count; r++)
        {
            for (int c = 0; c < this.Input[r].Count(); c++)
            {
                if (map[r][c] == 'a')
                {
                    starts.Add((r, c));
                }
            }
        }
        // Run A* for all 'a'
        int part2 = starts.Select(pos => FindShortestPath(map, pos, end)).Min();

        return (part1, part2);
    }
}