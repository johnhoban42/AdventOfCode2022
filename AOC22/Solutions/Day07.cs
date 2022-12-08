class INode
{
    private string NodeName;
    private int FileSize;
    private int CachedDirSize = -1;
    //public string Name { get; }
    public bool IsDir { get; }
    public List<INode> Children { get; }
    public INode? Parent { get; }

    public string Name()
    {
        if (Parent == null)
        {
            return NodeName;
        }
        return Parent.Name() + "/" + NodeName;
    }
    
    public int Size()
    {
        if (IsDir)
        {
            if (CachedDirSize == -1)
            {
                CachedDirSize = Children.Select(node => node.Size()).Sum(); 
            }
            return CachedDirSize;
        }
        return FileSize;
    }

    public override string ToString()
    {
        return base.ToString() + " " + Name();
    }

    public INode(string nodeName, bool isDir, INode? parent, int fileSize = 0)
    {
        this.NodeName = nodeName;
        this.IsDir = isDir;
        this.Children = new List<INode>(); 
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
        // Dictionary of INodes with each INode retrievable by its unique identifier
        Dictionary<string, INode> nodes = new Dictionary<string, INode>();

        // Traverse the input
        nodes.Add("/", new INode("/", true, null));
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
                    if (cmd[2] == "..")
                    {
                        current = parent;
                        parent = current!.Parent;
                    }
                    else
                    {
                        parent = current;
                        if (current == null)
                        {
                            current = nodes[cmd[2]];
                        }
                        else
                        {
                            current = nodes[current.Name() + "/" + cmd[2]];
                        }
                    }
                }
            }
            // "dir xyz" -> Create a new dir INode
            else if (cmd[0] == "dir")
            {
                INode node = new INode(cmd[1], true, current);
                current!.Children.Add(node);
                nodes.Add(node.Name(), node);
            }
            // Create a new file INode
            else
            {
                INode node = new INode(cmd[1], false, current, Int32.Parse(cmd[0]));
                current!.Children.Add(node);
                nodes.Add(node.Name(), node);
            }
        }

        // Calculate total size of directories with size <= 100,000
        int part1 = nodes.Values
            .Where(node => node.IsDir)
            .Select(node => node.Size())
            .Where(size => size < 100_000)
            .Sum();

        // Find the smallest directory such that free space once deleted >= 30_000_000
        int part2 = nodes.Values
            .Where(node => node.IsDir)
            .Select(node => node.Size())
            .Where(size => 30_000_000 <= 70_000_000 - nodes["/"].Size() + size)
            .Order()
            .First();

        return (part1, part2);
    }
}