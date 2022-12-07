namespace AoC2022
{
    public class Day07 : Solution
    {
        internal class Directory
        {
            public string Name { get; }
            private Directory Parent { get; }
            private Dictionary<string, Directory> Children { get; } = new();
            private Dictionary<string, int> Files { get; } = new();

            public Directory(Directory parent, string name)
            {
                Parent = parent;
                Name = name;
            }

            public Directory MoveTo(string name) => name.Equals("..") ? Parent : Children[name];

            public int GetSize() => Files.Values.Sum() + Children.Values.Select(c => c.GetSize()).Sum();

            public void AddChild(Directory dir) => Children.Add(dir.Name, dir);

            public void AddFile(string name, int size) => Files.Add(name, size);

        }

        private readonly List<Directory> directories = new();

        public Day07(string file) : base(file)
        {
            var current = new Directory(null, "/");
            directories.Add(current);

            foreach (var line in Input.Skip(1))
            {
                if (line.StartsWith("$ cd"))
                {
                    current = current.MoveTo(line.Split(' ')[2]);
                    continue;
                }

                if (line.StartsWith("$ ls"))
                    continue;

                if (line.StartsWith("dir "))
                {
                    var dir = new Directory(current, line.Split(" ")[1]);
                    directories.Add(dir);
                    current.AddChild(dir);
                }
                else
                {
                    var parts = line.Split(" ");
                    current.AddFile(parts[1], int.Parse(parts[0]));
                }
            }
        }

        public override string SolvePart1() => directories
            .Where(d => d.GetSize() <= 100000)
            .Select(d => d.GetSize())
            .Sum().ToString();

        public override string SolvePart2()
        {
            var hdSize = 70000000;
            var unused = hdSize - directories.First(d => d.Name.Equals("/")).GetSize();
            var toFree = 30000000 - unused;

            return directories
                .Where(d => d.GetSize() > toFree)
                .Select(d => d.GetSize())
                .Min().ToString();
        }
    }
}