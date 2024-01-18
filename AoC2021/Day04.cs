namespace AoC2021;

public class Day04 : Solution
{
	private readonly List<int> numbers;
	private List<(int number, bool marked)[][]> Boards { get; set; }
	public const int BoardDim = 5;
	public Day04(string file) : base(file)
	{
		numbers = Input.First().Split(',').Select(int.Parse).ToList();
	}

	private void InitializeBoards()
	{
		Boards = new();
		var digit = new Regex("\\d+");

		for (var idx = 1; idx < Input.Count - 2; idx += BoardDim)
		{
			Boards.Add(Input.Skip(idx).Take(BoardDim)
				.Select(i => digit.Matches(i).Select(m => (int.Parse(m.Value), false)).ToArray()).ToArray());
		}
	}

	public override async Task<string> SolvePart1() => PlayBingo().First().ToString();

	public override async Task<string> SolvePart2() => PlayBingo().Last().ToString();

	private IEnumerable<long> PlayBingo()
	{
		InitializeBoards();
		var winSums = new List<long>();

		numbers.ForEach(n =>
		{
			Boards.ForEach(b => b.MarkNumber(n));
			var winners = Boards.Where(b => b.HasBingo()).ToList();

			if (!winners.Any()) return;

			winSums.AddRange(winners.Select(b => b.GetSum(n)));
			Boards.RemoveAll(winners);
		});

		return winSums;
	}
}

public static class BoardExtension
{
	public static long GetSum(this (int number, bool marked)[][] board, int n) =>
		board.SelectMany(c => c).Where(c => !c.marked).Sum(c => c.number) * n;

	public static void MarkNumber(this (int number, bool marked)[][] board, int n)
	{
		for (var r = 0; r < Day04.BoardDim; r++)
		{
			for (var c = 0; c < Day04.BoardDim; c++)
			{
				if (board[r][c].number == n)
					board[r][c].marked = true;
			}
		}
	}

	public static bool HasBingo(this (int number, bool marked)[][] board)
	{
		for (var i = 0; i < Day04.BoardDim; i++)
		{
			var col = board[i].All(c => c.marked);
			var row = board.Select(b => b[i]).All(c => c.marked);
			if (col || row) return true;
		}
		return false;
	}
}