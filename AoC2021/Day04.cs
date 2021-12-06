using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common;

namespace AoC2021
{
    public class Day04 : Solution
    {
        private readonly List<int> numbers;
        private List<(int number, bool marked)[][]> Boards { get; set; }

        public Day04(string file) : base(file)
        {
            numbers = Input.First().Split(',').Select(int.Parse).ToList();
        }

        private void InitializeBoards()
        {
            Boards = new();
            var digit = new Regex("\\d+");

            for (var idx = 1; idx < Input.Count() - 2; idx += 5)
            {
                Boards.Add(Input.Skip(idx).Take(5)
                    .Select(i => digit.Matches(i).Select(m => (int.Parse(m.Value), false)).ToArray()).ToArray());
            }
        }

        public override string SolvePart1() => PlayBingo().First().ToString();

        public override string SolvePart2() => PlayBingo().Last().ToString();

        private IEnumerable<long> PlayBingo()
        {
            InitializeBoards();
            var winSums = new List<long>();

            numbers.ForEach(n =>
            {
                Boards.ForEach(b => b.MarkNumber(n));
                var winners = Boards.Where(b => b.HasBingo()).ToList();

                if (winners.Any())
                {
                    winners.ForEach(winner =>
                    {
                        winSums.Add(winner.SelectMany(c => c).Where(c => !c.marked).Sum(c => c.number) * n);
                        Boards.Remove(winner);
                    });
                }
            });
            return winSums;
        }
    }

    public static class BoardExtension
    {
        private const int dim = 5;
        public static void MarkNumber(this (int number, bool marked)[][] board, int n)
        {
            for (var r = 0; r < dim; r++)
            {
                for (var c = 0; c < dim; c++)
                {
                    if (board[r][c].number == n)
                        board[r][c].marked = true;
                }
            }
        }

        public static bool HasBingo(this (int number, bool marked)[][] board)
        {
            for (var i = 0; i < dim; i++)
            {
                var col = board[i].All(c => c.marked);
                var row = board.Select(b => b[i]).All(c => c.marked);
                if (col || row) return true;
            }
            return false;
        }
    }
}