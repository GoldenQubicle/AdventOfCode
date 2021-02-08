using System.Collections.Generic;

namespace Common
{
    public record CellularAutomatonOptions
    {
        public int Dimensions { get; init; }
        public bool IsInfinite { get; init; }
        public bool DoesWrap { get; init; }
    }
}