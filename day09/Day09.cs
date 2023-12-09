using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day09
    {
        // Mirage Maintenance: Calculate number deltas
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.StringLists(Day, file);
            int a = 0, b = 0;
            foreach (var sl in input)
            {
                List<List<int>> rows = new();
                rows.Add(sl.Select(int.Parse).ToList());
                while (rows.Last().Any(x => x != 0))
                    rows.Add(rows.Last().Zip(rows.Last().Skip(1), (x, y) => y - x).ToList());
                int alast = 0, blast = 0;
                for (int i = rows.Count - 1; i >= 0; i--)
                {
                    alast += rows[i].Last();
                    blast = rows[i].First() - blast;
                }
                a += alast;
                b += blast;
            }
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
