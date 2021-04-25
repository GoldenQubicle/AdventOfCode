namespace Common.Extensions
{
    public static class TupleExtension
    {
        public static (int, int) Add(this (int x, int y) a, (int x, int y) b) => (a.x + b.x, a.y + b.y);
    }
}
