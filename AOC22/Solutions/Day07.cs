class INode
{
    private int FileSize;
    private int CachedDirSize = -1;
    public string Name { get; }
    public bool IsDir { get; }
    public Dictionary<string, INode> Children { get; }
    public INode? Parent { get; }
    
    public int Size()
    {
        if (IsDir)
        {
            CachedDirSize = (CachedDirSize != -1)
                ? CachedDirSize
                : Children.Values.Select(node => node.Size()).Sum(); 
            return CachedDirSize;
        }
        return FileSize;
    }

    public override string ToString()
    {
        return base.ToString() + " " + Name;
    }

    public INode(string name, bool isDir, INode? parent, int fileSize = 0)
    {
        this.Name = name;
        this.IsDir = isDir;
        this.Children = new Dictionary<string, INode>(); 
        this.Parent = parent;
        this.FileSize = fileSize;
    }
}

class Day07 : Solver
{
    protected override string InputFile => "Data/Day07.txt";

    protected override (dynamic part1, dynamic part2) Solve()
    {
        /* INPUT */
        List<string[]> src = this.Input.Select(s => s.Split(" ")).ToList();

        /* PART 1 */
        // Build the directory tree
        List<INode> nodes = new List<INode>();
        INode root = new INode("/", true, null);
        nodes.Add(root);
        INode? parent = null;
        INode? current = null;
        foreach (string[] cmd in src)
        {
            // "$ cd dir" -> Navigate to dir
            // "$ ls" -> Doesn't change the tree's state. We can ignore it
            if (cmd[0] == "$")
            {
                if (cmd[1] == "cd")
                {
                    if (cmd[2] == "/")
                    {
                        current = root;
                        parent = null;
                    }
                    else if (cmd[2] == "..")
                    {
                        current = parent;
                        parent = current!.Parent;
                    }
                    else
                    {
                        parent = current;
                        current = current!.Children[cmd[2]];
                    }
                }
            }
            // "dir xyz" -> Create a new dir INode
            else if (cmd[0] == "dir")
            {
                INode node = new INode(cmd[1], true, current);
                current!.Children.Add(cmd[1], node);
                nodes.Add(node);
            }
            // Create a new file INode
            else
            {
                INode node = new INode(cmd[1], false, current, Int32.Parse(cmd[0]));
                current!.Children.Add(cmd[1], node);
                nodes.Add(node);
            }
        }

        // Calculate total size of directories with size <= 100,000
        IEnumerable<int> dirSizes = nodes.Where(node => node.IsDir).Select(node => node.Size());
        int part1 = dirSizes.Where(size => size < 100_000).Sum();

        // Find the smallest directory such that free space once deleted >= 30_000_000
        int freeSpace = 70_000_000 - root.Size();
        int part2 = dirSizes.Where(size => 30_000_000 <= freeSpace + size).Min();

        return (part1, part2);
    }
}