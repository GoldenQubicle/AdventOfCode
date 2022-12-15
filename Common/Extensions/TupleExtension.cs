namespace Common.Extensions
{
    public static class TupleExtension
    {
        public static (long, long) Add(this (long x, long y) a, long x, long y) => (a.x + x, a.y + y);
        public static (long, long) Add(this (long x, long y) a, (long x, long y) b) => (a.x + b.x, a.y + b.y);
        public static (int, int) Add(this (int x, int y) a, int x, int y) => (a.x + x, a.y + y);
        public static (int, int, int) Add(this (int x, int y, int z) a, int x, int y, int z) => (a.x + x, a.y + y, a.z + z);
        public static (int, int) Add(this (int x, int y) a, (int x, int y) b) => (a.x + b.x, a.y + b.y);
        public static int MultiplyComponents(this (int x, int y) a) => a.x * a.y;
    }
}
