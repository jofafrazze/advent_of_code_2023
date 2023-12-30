using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day21
    {
        // Step Counter: Find positions reachable, in part two in infinite grid
        static List<int> PlotsReachable(Map m, Pos start, List<int> maxSteps)
        {
            Dictionary<Pos, int> steps = new() { [start] = 0 };
            Queue<Pos> toVisit;
            var setSteps = maxSteps.ToHashSet();
            List<int> reachableCount = new();
            for (int i = 0; i < maxSteps.Last(); i++)
            {
                toVisit = new(steps.Keys.ToList());
                Dictionary<Pos, int> nextSteps = new();
                while (toVisit.Any())
                {
                    Pos cur = toVisit.Dequeue();
                    foreach (Pos p in CoordsRC.directions4.Select(w => cur + w))
                    {
                        Pos pMod = new(Utils.Modulo(p.x, m.width), Utils.Modulo(p.y, m.width));
                        if (m[pMod] != '#' && !nextSteps.ContainsKey(p))
                            nextSteps[p] = i + 1;
                    }
                }
                steps = nextSteps;
                if (setSteps.Contains(i + 1))
                    reachableCount.Add(steps.Count);
                //Map m2 = new(m);
                //foreach (Pos p in steps.Keys)
                //    m2[p] = 'O';
                //m2.Print();
            }
            return reachableCount;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            Map m = Map.Build(input);
            Pos start = m.Positions().Where(w => m[w] == 'S').First();
            int bsteps = 26501365;
            // Input is 131 x 131 characters
            int n = bsteps / m.width;
            int rem = bsteps % m.width;
            List<int> steps = new() { 64, rem, m.width + rem, m.width * 2 + rem };
            List<int> k = PlotsReachable(m, start, steps);
            long c = k[1];
            long a_b = k[2] - c;
            long a4_b2 = k[3] - c;
            long a2 = a4_b2 - 2 * a_b;
            long a = a2 / 2;
            long b = a_b - a;
            long bres = a * n * n + b * n + c;
            return (k[0], bres);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
