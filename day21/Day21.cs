using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day21
    {
        // Today: 
        static int PlotsReachable(Map m, Pos start, int maxSteps)
        {
            Dictionary<Pos, int> steps = new() { [start] = 0 };
            Queue<Pos> toVisit;
            for (int i = 0; i < maxSteps; i++)
            {
                toVisit = new(steps.Keys.ToList());
                Dictionary<Pos, int> nextSteps = new();
                while (toVisit.Any())
                {
                    Pos cur = toVisit.Dequeue();
                    foreach (Pos p in CoordsRC.directions4.Select(w => cur + w))
                        if (m.HasPosition(p) && m[p] != '#' && !nextSteps.ContainsKey(p))
                            nextSteps[p] = i + 1;
                }
                steps = nextSteps;
                //Map m2 = new(m);
                //foreach (Pos p in steps.Keys)
                //    m2[p] = 'O';
                //m2.Print();
            }
            return steps.Count;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            Map m = Map.Build(input);
            Pos start = m.Positions().Where(w => m[w] == 'S').First();
            int a = PlotsReachable(m, start, 64);
            return (a, 0);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
