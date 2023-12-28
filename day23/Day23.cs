using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;
using Node = AdventOfCode.Graph.Node<int>;

namespace aoc
{
    public class Day23
    {
        // A Long Walk: Find longest path in 2D map, reduce map to graph for speedup
        static readonly Dictionary<char, Pos> slopes = new()
        {
            ['v'] = CoordsRC.down,
            ['^'] = CoordsRC.up,
            ['<'] = CoordsRC.left,
            ['>'] = CoordsRC.right,
        };
        record struct PosId(Pos p, int steps, HashSet<Pos> visited);
        static int WalkMap(Map m, Pos start, Pos end, bool canClimbSlopes)
        {
            Queue<PosId> toVisit = new(new[] { new PosId(start, 0, new HashSet<Pos>()) });
            int maxPositions = 0;
            while (toVisit.Any())
            {
                PosId cur = toVisit.Dequeue();
                if (cur.visited.Contains(cur.p))
                    continue;
                cur.visited.Add(cur.p);
                if (cur.p == end)
                {
                    if (cur.visited.Count > maxPositions)
                    {
                        maxPositions = cur.visited.Count;
                        Console.WriteLine($"New max positions: {maxPositions}");
                    }
                    continue;
                }
                var nextDirs = m[cur.p] == '.' || canClimbSlopes ? CoordsRC.directions4 : new List<Pos>() { slopes[m[cur.p]] };
                foreach (Pos p in nextDirs.Select(w => cur.p + w))
                    if (m.HasPosition(p) && m[p] != '#')
                        toVisit.Enqueue(new PosId(p, 0, cur.visited.ToHashSet()));
            }
            return maxPositions - 1;
        }
        static Dictionary<Pos, Dictionary<Pos, int>> BuildSteps(Map m, Pos start)
        {
            Dictionary<Pos, Dictionary<Pos, int>> steps = new();
            HashSet<Pos> visited = new();
            Queue<Pos> toVisit = new(new[] { start });
            while (toVisit.Any())
            {
                Pos cur = toVisit.Dequeue();
                if (visited.Contains(cur))
                    continue;
                visited.Add(cur);
                if (!steps.ContainsKey(cur))
                    steps[cur] = new();
                foreach (Pos p in CoordsRC.directions4.Select(w => cur + w))
                    if (m.HasPosition(p) && m[p] != '#')
                    {
                        toVisit.Enqueue(p);
                        if (!steps.ContainsKey(p))
                            steps[p] = new();
                        steps[p][cur] = 1;
                        steps[cur][p] = 1;
                    }
            }
            return steps;
        }
        static void PruneSteps(Dictionary<Pos, Dictionary<Pos, int>> steps)
        {
            bool done;
            do
            {
                var toPrune = steps.Where(w => w.Value.Count == 2);
                done = !toPrune.Any();
                if (!done)
                {
                    var (from, tos) = toPrune.First();
                    steps.Remove(from);
                    foreach (var (to, _) in tos)
                        steps[to].Remove(from);
                    var toList = tos.ToList();
                    int cost = toList[0].Value + toList[1].Value;
                    steps[toList[0].Key][toList[1].Key] = cost;
                    steps[toList[1].Key][toList[0].Key] = cost;
                }
            }
            while (!done);
        }
        static int WalkSteps(Dictionary<Pos, Dictionary<Pos, int>> steps, Pos start, Pos end)
        {
            Queue<PosId> toVisit = new(new[] { new PosId(start, 0, new HashSet<Pos>()) });
            int maxSteps = 0;
            while (toVisit.Any())
            {
                PosId cur = toVisit.Dequeue();
                if (cur.visited.Contains(cur.p))
                    continue;
                cur.visited.Add(cur.p);
                if (cur.p == end)
                {
                    if (cur.steps > maxSteps)
                    {
                        maxSteps = cur.steps;
                        Console.WriteLine($"New max steps: {maxSteps}");
                    }
                    continue;
                }
                foreach (var (p, cost) in steps[cur.p])
                    toVisit.Enqueue(new PosId(p, cur.steps + cost, cur.visited.ToHashSet()));
            }
            return maxSteps;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            Map m = Map.Build(input);
            Pos start = new(1, 0);
            Pos end = new(m.width - 2, m.height - 1);
            int a = WalkMap(m, start, end, false);
            var steps = BuildSteps(m, new Pos(1, 0));
            PruneSteps(steps);
            int b = WalkSteps(steps, start, end);
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle, true);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
