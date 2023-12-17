using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day17
    {
        // Clumsy Crucible: Walk 2D map, find minimum sum to reach target position
        record struct Step(Pos p, Pos dir, int nStraight);
        static int WalkMap(Map m, List<Step> start, bool part1)
        {
            HashSet<Step> visited = new();
            var toVisit = new PriorityQueue<Step, int>(start.Select(w => (w, 0)));
            Pos endPos = new(m.width - 1, m.height - 1);
            int maxStraight = part1 ? 3 : 10;
            while (toVisit.TryDequeue(out Step cur, out int curScore))
            {
                if (cur.p == endPos && (part1 || cur.nStraight >= 3))
                    return curScore;
                if (visited.Contains(cur))
                    continue;
                visited.Add(cur);
                foreach (Pos dir in CoordsRC.directions4)
                {
                    Step next = new(cur.p + cur.dir, dir, (dir == cur.dir) ? cur.nStraight + 1 : 0);
                    if (dir != -cur.dir && m.HasPosition(next.p) && next.nStraight < maxStraight)
                        if (part1 || (dir == cur.dir || cur.nStraight >= 3))
                            toVisit.Enqueue(next, curScore + m[next.p] - '0');
                }
            }
            throw new Exception("Not found!");
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            Map m = Map.Build(input);
            List<Step> steps = new() { new Step(new Pos(), CoordsRC.right, 0), new Step(new Pos(), CoordsRC.down, 0) };
            int a = WalkMap(m, steps, true);
            int b = WalkMap(m, steps, false);
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
