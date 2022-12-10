class Day10 : Solver
{
    protected override string InputFile => "Data/Day10.txt";

    // Draw a # if the current sprite position mod 40 is within 1 pixel of the cycle mod 40
    // Use (cycle - 1) because the cycles are 1-indexed but the CRT is 0-indexed
    private char DrawPixel(int x, int cycle)
    {
        return Math.Abs(x % 40 - (cycle - 1) % 40) <= 1 ? '#': '.';
    }

    protected override (dynamic part1, dynamic part2) Solve()
    {
        /* PART 1 */
        int x = 1;
        List<int> signals = new List<int>();
        int op = 0;
        int n = this.Input.Count();
        while (signals.Count() < 220)
        {
            if (this.Input[op % n] == "noop")
            {
                signals.Add(x);
            }
            else
            {
                signals.Add(x);
                signals.Add(x);
                x += Int32.Parse(this.Input[op % n].Split(" ")[1]);
            }
            op++;
        }
        int[] idx = new int[] { 20, 60, 100, 140, 180, 220 };
        int part1 = idx.Select(i => i * signals[i-1]).Sum();
        
        /* PART 2 */
        string crt = "";
        x = 1;
        op = 0;
        int cycle = 0;
        while (cycle < 240)
        {
            // Position
            if (this.Input[op % n] == "noop")
            {
                cycle++;
                crt += DrawPixel(x, cycle);
            }
            else
            {
                cycle++;
                crt += DrawPixel(x, cycle);
                cycle++;
                crt += DrawPixel(x, cycle);
                x += Int32.Parse(this.Input[op % n].Split(" ")[1]);
            }
            op++;
        }
        for (int i = 0; i < 240; i += 40)
        {
            Console.WriteLine(crt.Substring(i, 40));
        }

        // Part 2 is solved by inspection, return nothing
        return (part1, 0);
    }
}