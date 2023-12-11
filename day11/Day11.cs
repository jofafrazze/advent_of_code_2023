using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;
using PosLong = AdventOfCode.GenericPosition2D<long>;

namespace aoc
{
    public class Day11
    {
        // Today: 
        public static Object PartA(string file)
        {
            var input = ReadInput.Strings(Day, file);
            var expRows = new List<int>();
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i].All(c => c == '.'))
                    expRows.Add(i);
            }
            var colFlags = new List<bool>();
            for (int i = 0; i < input[0].Length; i++)
                colFlags.Add(true);
            for (int j = 0; j < input.Count; j++)
            {
                for (int i = 0; i < input[0].Length; i++)
                {
                    if (input[j][i] != '.')
                        colFlags[i] = false;
                }
            }
            var expCols = new List<int>();
            for (int i = 0; i < input[0].Length; i++)
                if (colFlags[i])
                    expCols.Add(i);
            var m = Map.Build(input);
            var galaxies = m.Positions().Where(w => m[w] == '#').Select(w => new PosLong(w.x, w.y)).ToList();
            var galaxiesOriginal = galaxies.ToList();
            for (int i = expRows.Count - 1; i >= 0; i--)
            {
                for (int n = 0; n < galaxies.Count; n++)
                {
                    if (galaxiesOriginal[n].y > expRows[i])
                        galaxies[n] = new PosLong(galaxies[n].x, galaxies[n].y + 1);
                }
            }
            for (int i = expCols.Count - 1; i >= 0; i--)
            {
                for (int n = 0; n < galaxies.Count; n++)
                {
                    if (galaxiesOriginal[n].x > expCols[i])
                        galaxies[n] = new PosLong(galaxies[n].x + 1, galaxies[n].y);
                }
            }
            var pairs = Algorithms.GetCombinations(galaxies, 2).ToList();
            long sum = 0;
            foreach (var pair in pairs)
            {
                if (pair.Count == 2)
                {
                    PosLong p0 = pair[0];
                    PosLong p1 = pair[1];
                    sum += p0.ManhattanDistance(p1);
                }
            }
            return sum;
        }
        public static Object PartB(string file)
        {
            var input = ReadInput.Strings(Day, file);
            var expRows = new List<int>();
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i].All(c => c == '.'))
                    expRows.Add(i);
            }
            var colFlags = new List<bool>();
            for (int i = 0; i < input[0].Length; i++)
                colFlags.Add(true);
            for (int j = 0; j < input.Count; j++)
            {
                for (int i = 0; i < input[0].Length; i++)
                {
                    if (input[j][i] != '.')
                        colFlags[i] = false;
                }
            }
            var expCols = new List<int>();
            for (int i = 0; i < input[0].Length; i++)
                if (colFlags[i])
                    expCols.Add(i);
            var m = Map.Build(input);
            var galaxies = m.Positions().Where(w => m[w] == '#').Select(w => new PosLong(w.x, w.y)).ToList();
            var galaxiesOriginal = galaxies.ToList();
            int expSize = 1000000;
            for (int i = expRows.Count - 1; i >= 0; i--)
            {
                for (int n = 0; n < galaxies.Count; n++)
                {
                    if (galaxiesOriginal[n].y > expRows[i])
                        galaxies[n] = new PosLong(galaxies[n].x, galaxies[n].y + expSize - 1);
                }
            }
            for (int i = expCols.Count - 1; i >= 0; i--)
            {
                for (int n = 0; n < galaxies.Count; n++)
                {
                    if (galaxiesOriginal[n].x > expCols[i])
                        galaxies[n] = new PosLong(galaxies[n].x + expSize - 1, galaxies[n].y);
                }
            }
            var pairs = Algorithms.GetCombinations(galaxies, 2).ToList();
            long sum = 0;
            foreach (var pair in pairs)
            {
                if (pair.Count == 2)
                {
                    PosLong p0 = pair[0];
                    PosLong p1 = pair[1];
                    sum += p0.ManhattanDistance(p1);
                }
            }
            return sum;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            return (PartA(file), PartB(file));
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
