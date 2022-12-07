namespace AoC2022
{
    public class Day07 : Solution
    {
        internal class Directory
        {
            public string Name { get; }
            public Directory Parent { get; }
            public List<Directory> Children { get; } = new();
            public List<(string name, int size)> Files { get; } = new();

            public Directory(Directory parent, string name)
            {
                Parent = parent;
                Name = name;
            }

            public Directory Up() => Parent;

            public Directory Down(string name) => name.Equals("/")
                ? this
                : Children.First(d => d.Name.Equals(name));

            public int GetSize() => Children.Any()
                ? Children.Select(c => c.GetSize()).Sum() + Files.Select(f => f.size).Sum()
                : Files.Select(f => f.size).Sum();

        }

        private readonly List<Directory> directories = new();

        public Day07(string file) : base(file)
        {
            var current = new Directory(null, "/");
            directories.Add(current);

            foreach (var line in Input)
            {
                if (line.StartsWith("$ cd"))
                {
                    var part = line.Split(' ')[2];
                    current = part.Equals("..") ? current.Up() : current.Down(part);
                    continue;
                }

                if (line.StartsWith("$ ls"))
                    continue;

                if (line.StartsWith("dir "))
                {
                    var dir = new Directory(current, line.Split(" ")[1]);
                    directories.Add(dir);
                    current.Children.Add(dir);
                }
                else
                {
                    var parts = line.Split(" ");
                    current.Files.Add((parts[1], int.Parse(parts[0])));
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
                .Select(d => d.GetSize())
                .Where(d => d > toFree)
                .Min().ToString();
        }
    }
}