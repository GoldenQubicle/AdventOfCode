using System.Collections.Generic;
using Common.Interfaces;

namespace Common.Renders
{
	public class KnotHashRender : IRenderState
	{
		public int Jump { get; init; }
		public List<int> Range { get; init; }
	}
}
