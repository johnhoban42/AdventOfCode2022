class Day14 : Solver
{
    protected override string InputFile => "Data/Day14.txt";

    protected override (dynamic part1, dynamic part2) Solve()
    {
        /* INPUT */
        // Scan for all rocks
        // Sand grains and rocks are treated the same, so we can put them all in one set
        int[][][] src = this.Input.Select(
            s => s.Split(" -> ").ToArray().Select(
                t => t.Split(",").Select(Int32.Parse).ToArray()
            ).ToArray()
        ).ToArray();
        var sand = new HashSet<(int, int)>();
        foreach (int[][] path in src)
        {
            for (int i = 0; i < path.Length - 1; i++)
            {
                (int x0, int y0) = (path[i][0], path[i][1]);
                (int x1, int y1) = (path[i+1][0], path[i+1][1]);
                sand.Add((x0, y0));
                while ((x0, y0) != (x1, y1))
                {
                    x0 += Math.Sign(x1 - x0);
                    y0 += Math.Sign(y1 - y0);
                    sand.Add((x0, y0));
                }
            }
        }

        /* PART 1 */
        // Count grains until one falls below the rock with the highest y-coordinate (y*)
        /* PART 2 */
        // Sand settles at the floor, y=(y*-1), until (500, 0) enters the set of settled sand
        int abyss = sand.Select(r => r.Item2).Max();
        int units = 0;
        int part1 = 0;
        while (!sand.Contains((500, 0)))
        {
            int grainX = 500;
            int grainY = 0;
            while (true)
            {
                if (grainY == abyss + 1)
                {
                    sand.Add((grainX, grainY));
                    break;
                }
                else if (!sand.Contains((grainX, grainY + 1)))
                {
                    grainY++;
                }
                else if (!sand.Contains((grainX - 1, grainY + 1)))
                {
                    grainY++;
                    grainX--;
                }
                else if (!sand.Contains((grainX + 1, grainY + 1)))
                {
                    grainY++;
                    grainX++;
                }
                else
                {
                    sand.Add((grainX, grainY));
                    break;
                }
            }
            if (grainY >= abyss && part1 == 0)
            {
                part1 = units;
            }
            units++;
        }
        int part2 = units;

        return (part1, part2);
    }
}