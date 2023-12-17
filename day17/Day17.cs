using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day17
    {
        // Today: 
        record struct Step(Pos p, Pos dir, int nStraight);
        static int WalkMap(Map m, List<Step> start)
        {
            HashSet<Step> visited = new();
            var toVisit = new PriorityQueue<Step, int>();
            foreach (var s in start)
                toVisit.Enqueue(s, 0);
            Pos endPos = new(m.width - 1, m.height - 1);
            while (toVisit.TryDequeue(out Step cur, out int curScore))
            {
                if (cur.p == endPos)
                    return curScore;
                if (visited.Contains(cur))
                    continue;
                visited.Add(cur);
                foreach (Pos dir in CoordsRC.directions4)
                {
                    Step next = new(cur.p + cur.dir, dir, (dir == cur.dir) ? cur.nStraight + 1 : 0);
                    if (dir != -cur.dir && m.HasPosition(next.p) && next.nStraight < 3)
                        toVisit.Enqueue(next, curScore + m[next.p] - '0');
                }
            }
            throw new Exception("Not found!");
        }
        static int WalkMap2(Map m, List<Step> start)
        {
            Dictionary<Step, int> visited = new();
            var toVisit = new PriorityQueue<Step, int>();
            foreach (var s in start)
            {
                visited[s] = 0;
                toVisit.Enqueue(s, 0);
            }
            int bestSum = int.MaxValue;
            Pos bottomRight = new Pos(m.width - 1, m.height - 1);
            while (toVisit.TryDequeue(out Step cur, out int score))
            {
                foreach (Pos dir in CoordsRC.directions4)
                {
                    if (dir != -cur.dir)
                    {
                        Step next = new(cur.p + cur.dir, dir, (dir == cur.dir) ? cur.nStraight + 1 : 0);
                        if ((dir == cur.dir) || cur.nStraight >= 3)
                        {
                            if (m.HasPosition(next.p) && next.nStraight < 10)
                            {
                                int nextSum = visited[cur] + (m[next.p] - '0');
                                if (nextSum < bestSum)
                                {
                                    if (!visited.ContainsKey(next) || visited[next] > nextSum)
                                    {
                                        visited[next] = nextSum;
                                        toVisit.Enqueue(next, nextSum);
                                        if (next.p == bottomRight && next.nStraight >= 4)
                                            bestSum = nextSum;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return bestSum;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            Map m = Map.Build(input);
            List<Step> steps = new() { new Step(new Pos(), CoordsRC.right, 0), new Step(new Pos(), CoordsRC.down, 0) };
            int a = WalkMap(m, steps);
            int b = WalkMap2(m, steps);
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
