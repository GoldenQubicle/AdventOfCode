using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Common.Interfaces;


public interface INode
{
    int X { get; }
    int Y { get; }
    long Value { get; }
    char Character { get; set; }

	T Cast<T>() => (T)this;
}
