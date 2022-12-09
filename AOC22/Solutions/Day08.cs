class Day08 : Solver
{
    protected override string InputFile => "Data/Day08.txt";

    protected override (dynamic part1, dynamic part2) Solve()
    {
        /* INPUT */
        int[][] trees = this.Input.Select(s => s.ToCharArray().Select(c => c - '0').ToArray()).ToArray();
        int h = trees.Length;
        int w = trees[0].Length;

        /* PART 1 */
        // Find cumulative maximums in all directions
        bool[][] isMax = Enumerable.Range(0, h).Select(i => new bool[w]).ToArray();
        // Left to right
        for (int r = 0; r < h; r++)
        {
            int cumMax = -1;
            for (int c = 0; c < w - 1; c++)
            {
                if (cumMax < trees[r][c])
                {
                    isMax[r][c] = true;
                    cumMax = trees[r][c];
                }
            }
        }
        // Right to left
        for (int r = 0; r < h; r++)
        {
            int cumMax = -1;
            for (int c = w-1; c > 0; c--)
            {
                if (cumMax < trees[r][c])
                {
                    isMax[r][c] = true;
                    cumMax = trees[r][c];
                }
            }
        }
        // Top to bottom
        for (int c = 0; c < w-1; c++)
        {
            int cumMax = -1;
            for (int r = 0; r < h-1; r++)
            {
                if (cumMax < trees[r][c])
                {
                    isMax[r][c] = true;
                    cumMax = trees[r][c];
                }
            }
        }
        // Bottom to top
        for (int c = 0; c < w-1; c++)
        {
            int cumMax = -1;
            for (int r = h-1; r > 0; r--)
            {
                if (cumMax < trees[r][c])
                {
                    isMax[r][c] = true;
                    cumMax = trees[r][c];
                }
            }
        }

        int part1 = isMax.Select(
            row => Array.FindAll(row, col => col == true).Count()
        ).Sum();

        /* PART 2 */
        // From each tree, count trees in each direction until our view is blocked
        int[][] scenic = Enumerable.Range(0, h).Select(i => new int[w]).ToArray();
        for (int r = 1; r < h - 1; r++)
        {
            for (int c = 1; c < w - 1; c++)
            {
                int left = 1;
                while((c - left > 0) && (trees[r][c - left] < trees[r][c]))
                {
                    left++;
                }
                
                int right = 1;
                while((c + right < w - 1) && (trees[r][c + right] < trees[r][c]))
                {
                    right++;
                }
                
                int up = 1;
                while((r - up > 0) && (trees[r - up][c] < trees[r][c]))
                {
                    up++;
                }

                int down = 1;
                while((r + down < h - 1) && (trees[r + down][c] < trees[r][c]))
                {
                    down++;
                }

                scenic[r][c] = left * right * up * down;
            }
        }
        int part2 = scenic.Select(row => row.Max()).Max();

        return (part1, part2);
    }
}