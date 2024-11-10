namespace AoC2016
{
    public class Day04 : Solution
    {
        private readonly List<(string name, int sectorId, string checkSum)> rooms;

        public Day04(string file) : base(file, "\n")
        {
            var regex = new Regex(@"([a-z-]+)(\d+)\[(\w+)\]");
            rooms = Input.Select(line =>
            {
                var matches = regex.Match(line);
                return (name: matches.Groups[1].Value,
                    sectorId: int.Parse(matches.Groups[2].Value),
                    checkSum: matches.Groups[3].Value);
            }).ToList();
        }

        public override async Task<string> SolvePart1( ) => rooms.Where(IsRealRoom).Sum(r => r.sectorId).ToString();

        public override async Task<string> SolvePart2( ) => rooms.Where(IsRealRoom)
                .Select(r => (name: DecryptRoomName(r), r.sectorId))
                .First(r => r.name.Contains("northpole")).sectorId.ToString();

        public string DecryptRoomName((string name, int sectorId, string checkSum) room)
        {
            var shift = room.sectorId % 26;
            //a:97 - z:122
            return new string(room.name.Select(c =>
                c == '-' ? ' ' :
                    c + shift > 122 ?
                    (char) (c + shift - 26) :
                    (char) (c + shift))
                .ToArray());
        }

        private bool IsRealRoom((string name, int sectorId, string checkSum) room)
        {
            var checksum = new string(room.name
                .Where(c => c != '-')
                .GroupBy(c => c, (c, chars) => (letter: c, count: chars.Count()))
                .OrderByDescending(g => g.count)
                .GroupBy(g => g.count, (_, letters) => letters.OrderBy(l => l.letter))
                .SelectMany(g => g.Select(c => c.letter))
                .Take(5).ToArray());

            return room.checkSum.Equals(checksum);
        }
    }
}