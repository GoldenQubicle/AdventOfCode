namespace Common.Interfaces;


public interface INode
{
	(int x, int y) Position { get; set; }
	int X { get; }
	int Y { get; }
	long Value { get; }
	char Character { get; set; }
	long Cost { get; set; }

	T Cast<T>() => (T)this;
}
