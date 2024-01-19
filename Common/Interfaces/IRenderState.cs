using System.Threading.Tasks;
using System;

namespace Common.Interfaces;

public interface IRenderState
{
	T Cast<T>() => (T)this;

	static bool IsActive => Update is not null;
	static Func<IRenderState, Task> Update { get; set; }


}