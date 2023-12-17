using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day17
    {
        // Clumsy Crucible: Walk 2D map, find minimum sum to reach target position
        record struct Step(Pos pos, Pos dir, int len);
        static int WalkMap(Map m, List<Step> start, bool part1)
        {
            HashSet<Step> visited = new();
            var toVisit = new PriorityQueue<Step, int>(start.Select(w => (w, 0)));
            Pos endPos = new(m.width - 1, m.height - 1);
            int maxLen = part1 ? 3 : 10;
            while (toVisit.TryDequeue(out Step cur, out int curScore))
            {
                if (cur.pos == endPos && (part1 || cur.len >= 3))
                    return curScore;
                if (visited.Contains(cur))
                    continue;
                visited.Add(cur);
                foreach (Pos dir in CoordsRC.directions4)
                {
                    Step next = new(cur.pos + cur.dir, dir, (dir == cur.dir) ? cur.len + 1 : 0);
                    if (dir != -cur.dir && m.HasPosition(next.pos) && next.len < maxLen)
                        if (part1 || (dir == cur.dir || cur.len >= 3))
                            toVisit.Enqueue(next, curScore + m[next.pos] - '0');
                }
            }
            throw new Exception("Not found!");
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            Map m = Map.Build(ReadInput.Strings(Day, file));
            List<Step> steps = new() { new Step(new Pos(), CoordsRC.right, 0), new Step(new Pos(), CoordsRC.down, 0) };
            return (WalkMap(m, steps, true), WalkMap(m, steps, false));
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
