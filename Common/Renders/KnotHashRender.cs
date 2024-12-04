namespace Common.Renders
{
	public class KnotHashRender : IRenderState
	{
		public int Cycle { get; init; }
		public int Operation { get; init; }
		public int Jump { get; init; }
		public List<int> Range { get; init; }
	}
}
