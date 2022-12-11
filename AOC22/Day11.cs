public class Monkey
{
    public long ItemsInspected { get; set; }
    private List<long> Items { get; }
    public string[] Operation { get; }
    public int TestDiv { get; }
    public int TrueMonkey { get; }
    public int FalseMonkey { get; }

    public bool HasItems() => this.Items.Count > 0;

    // Inspect the first item and return which monkey to throw it to
    public int Inspect(bool worried, int mod)
    {
        // Construct and execute the operation
        long op1;
        long op2;
        if (String.Equals(Operation[0], "old"))
        {
            op1 = Items[0];
        }
        else
        {
            op1 = Int32.Parse(Operation[0]);
        }
        if (String.Equals(Operation[2], "old"))
        {
            op2 = Items[0];
        }
        else
        {
            op2 = Int32.Parse(Operation[2]);
        }
        if ((String.Equals(Operation[1], "+")))
        {
            Items[0] = op1 + op2;
        }
        else
        {
            Items[0] = op1 * op2;
        }
        if (!worried)
        {
            Items[0] /= 3;
        }
        Items[0] %= mod;
        this.ItemsInspected++;
        // Choose a monkey to throw to
        return Items[0] % TestDiv == 0 ? TrueMonkey : FalseMonkey;
    }
    
    // Throw the first item to another monkey
    public void ThrowTo(Monkey other)
    {
        long item = Items[0];
        Items.RemoveAt(0);
        other.Items.Add(item);
    } 

    // Parse the input description
    // A little convoluted but trust me it works
    public Monkey(string notes)
    {
        ItemsInspected = 0;
        string[] arr = notes.Split("\n").ToArray();
        Items = arr[1].Split(": ").ElementAt(1).Split(", ").Select(Int64.Parse).ToList();
        Operation = arr[2].Split(" = ").ElementAt(1).Split(" ").ToArray();
        TestDiv = Int32.Parse(arr[3].Split(" ").Last());
        TrueMonkey = Int32.Parse(arr[4].Split(" ").Last());
        FalseMonkey = Int32.Parse(arr[5].Split(" ").Last());
    }
}

class Day11 : Solver
{
    protected override string InputFile => "Data/Day11.txt";

    protected override List<string> ReadInput() => File.ReadAllText(InputFile).Split("\n\n").ToList();

    // Helper method to simulate monkey business
    protected long MonkeyBusiness(int rounds, bool worried)
    {
        // The test divisors for all monkeys are coprime
        // Applying the product of all test divisors as a modulus for worry levels so we don't overflow
        List<Monkey> monkeys = this.Input.Select(s => new Monkey(s)).ToList();
        int mod = monkeys.Select(m => m.TestDiv).Aggregate((x, y) => x * y);
        for (int i = 0; i < rounds; i++)
        {
            foreach (Monkey monkey in monkeys)
            {
                while (monkey.HasItems())
                {
                    int m = monkey.Inspect(worried, mod);
                    monkey.ThrowTo(monkeys[m]);
                }
            }
        }
        long[] inspections = monkeys.Select(m => m.ItemsInspected).Order().Reverse().ToArray();
        return inspections[0] * inspections[1];
    }

    protected override (dynamic part1, dynamic part2) Solve()
    {
        long part1 = MonkeyBusiness(20, false);
        long part2 = MonkeyBusiness(10_000, true);
        return (part1, part2);
    }
}