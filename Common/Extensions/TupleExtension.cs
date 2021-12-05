namespace Common.Extensions
{
    public static class TupleExtension
    {
        public static (int, int) Add(this (int x, int y) a, int x, int y ) => (a.x + x, a.y + y);
        public static (int, int) Add(this (int x, int y) a, (int x, int y) b) => (a.x + b.x, a.y + b.y);
        public static int MultiplyComponents(this (int x, int y) a) => a.x * a.y;
    }
}
