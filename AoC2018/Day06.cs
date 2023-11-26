using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using Common.Extensions;

namespace AoC2018
{
    public class Day06 : Solution
    {
	    private readonly List<(int x, int y)> coords;

	    public Day06(string file) : base(file) => coords = Input.Select(s =>
	    {
		    var parts = s.Split(',');
		    return (parts[0].AsInteger(), parts[1].AsInteger());
	    }).ToList();


	    public override string SolvePart1()
	    {
		    var minx = coords.MinBy(c => c.x);
		    var maxx = coords.MaxBy(c => c.x);
		    var miny = coords.MinBy(c => c.y);
		    var maxy = coords.MaxBy(c => c.y);

		    var grid = new Grid2d();

		    return string.Empty;
	    }

        public override string SolvePart2( ) => null;
    }
}