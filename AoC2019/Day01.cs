using System.Linq;
using Common;

namespace AoC2019
{
    public class Day01 : Solution
    {
        public Day01(string file) : base(file) { }

        public override string SolvePart1() => Input.Select(int.Parse).Select(GetFuelPerModule).Sum().ToString();

        public override string SolvePart2() => Input.Select(int.Parse).Select(mass =>
            {
                var fuelMass = GetFuelPerModule(mass);
                var total = fuelMass;
                while (fuelMass > 0)
                {
                    fuelMass = GetFuelPerModule(fuelMass);
                    if (fuelMass > 0) total += fuelMass;
                }
                return total;
            }).Sum().ToString();

        public static int GetFuelPerModule(int mass) => mass / 3 - 2;

    }
}