using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day18
    {
        // Lavaduct Lagoon: Calculate area of huge pool
        static readonly Dictionary<char, Pos> directionsA = new()
        {
            ['U'] = CoordsRC.up,
            ['L'] = CoordsRC.left,
            ['D'] = CoordsRC.down,
            ['R'] = CoordsRC.right,
        };
        static Pos[] directionsB = new[] { CoordsRC.right, CoordsRC.down, CoordsRC.left, CoordsRC.up };
        static List<Pos> BuildPolygon(List<(Pos dir, int len)> recipe)
        {
            Pos pos = new();
            List<Pos> polygon = new() { pos };
            foreach (var (dir, len) in recipe)
                polygon.Add(pos += dir * len);
            return polygon;
        }
        static long PolygonArea(List<Pos> polygon)
        {
            // Shoelace formula
            long sum1 = 0, sum2 = 0;
            for (int i = 0; i < polygon.Count; i++)
            {
                sum1 += polygon[i].x * (long)polygon[(i + 1) % polygon.Count].y;
                sum2 += polygon[i].y * (long)polygon[(i + 1) % polygon.Count].x;
            }
            long area = Math.Abs(sum1 - sum2) / 2;
            long circumference = 0;
            for (int i = 0; i < polygon.Count; i++)
                circumference += polygon[i].ManhattanDistance(polygon[(i + 1) % polygon.Count]);
            // Total area is area + half circumference + 4 * 1/4 corners
            return area + circumference / 2 + 1;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.StringLists(Day, file);
            List<(Pos, int)> recipeA = new();
            List<(Pos, int)> recipeB = new();
            foreach (var sl in input)
            {
                recipeA.Add((directionsA[sl[0][0]], int.Parse(sl[1])));
                recipeB.Add((directionsB[sl[2][^2] - '0'], Convert.ToInt32(sl[2][2..^2], 16)));
            }
            long a = PolygonArea(BuildPolygon(recipeA));
            long b = PolygonArea(BuildPolygon(recipeB));
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle, true);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
