using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<long>;

namespace aoc
{
    public class Day11
    {
        // Cosmic Expansion: Expand 2D map, calculate sum of distances
        static long ExpandAndGetDists(List<Pos> galaxies, List<Pos> galaxies0, List<int> rows, List<int> cols, int add)
        {
            // Expand universe
            for (int i = rows.Count - 1; i >= 0; i--)
                for (int n = 0; n < galaxies.Count; n++)
                    if (galaxies0[n].y > rows[i])
                        galaxies[n] = new Pos(galaxies[n].x, galaxies[n].y + add);
            for (int i = cols.Count - 1; i >= 0; i--)
                for (int n = 0; n < galaxies.Count; n++)
                    if (galaxies0[n].x > cols[i])
                        galaxies[n] = new Pos(galaxies[n].x + add, galaxies[n].y);
            // Calculate sum of distances
            var pairs = Algorithms.GetCombinations(galaxies, 2).Where(w => w.Count == 2).ToList();
            long sum = 0;
            foreach (var pair in pairs)
                sum += pair[0].ManhattanDistance(pair[1]);
            return sum;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            var m = Map.Build(input);
            var galaxiesA = m.Positions().Where(w => m[w] == '#').Select(w => new Pos(w.x, w.y)).ToList();
            var galaxiesB = galaxiesA.ToList();
            var galaxiesOriginal = galaxiesA.ToList();
            var rows = Enumerable.Range(0, input.Count).ToHashSet();
            var cols = Enumerable.Range(0, input[0].Length).ToHashSet();
            foreach (Pos p in galaxiesA)
            {
                rows.Remove((int)p.y);
                cols.Remove((int)p.x);
            }
            var expRows = rows.ToList();
            var expCols = cols.ToList();
            long a = ExpandAndGetDists(galaxiesA, galaxiesOriginal, expRows, expCols, 1);
            long b = ExpandAndGetDists(galaxiesB, galaxiesOriginal, expRows, expCols, 1000000 - 1);
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
