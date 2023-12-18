namespace Common.Extensions
{
    public static class TupleExtension
    {
        public static (long x, long y) Add(this (long x, long y) a, long x, long y) => (a.x + x, a.y + y);
        public static (long x, long y) Add(this (long x, long y) a, (long x, long y) b) => (a.x + b.x, a.y + b.y);
        public static (int x, int y) Add(this (int x, int y) a, int x, int y) => (a.x + x, a.y + y);
        public static (int x, int y) Add(this (int x, int y) a, (int x, int y) b) => (a.x + b.x, a.y + b.y);
        public static (long x, long y) Subtract(this (long x, long y) a, (long x, long y) b) => (a.x - b.x, a.y - b.y);
        public static (long x, long y) Subtract(this (long x, long y) a, long x, long y) => (a.x - x, a.y - y);
		public static (int x, int y) Subtract(this (int x, int y) a, (int x, int y) b) => (a.x - b.x, a.y - b.y);
        public static (int x, int y) Subtract(this (int x, int y)a, int x, int y) => (a.x - x, a.y - y);
		public static (int x, int y, int z) Add(this (int x, int y, int z) a, int x, int y, int z) => (a.x + x, a.y + y, a.z + z);
        public static (long x, long y) ToLong(this (int x, int y) a) => (a.x, a.y);
        public static int MultiplyComponents(this (int x, int y) a) => a.x * a.y;

    }
}
