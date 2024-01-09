namespace Common.Interfaces
{
	public interface IRenderState
	{
		T Cast<T>() => (T)this;
	}
}
