using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day16
    {
        // The Floor Will Be Lava: Let beam move around in 2D, reflect and split
        record struct Beam(Pos p, Pos dir);
        static readonly Dictionary<Pos, Pos> mirrorSlash = new()
        {
            [CoordsRC.right] = CoordsRC.up,
            [CoordsRC.left] = CoordsRC.down,
            [CoordsRC.up] = CoordsRC.right,
            [CoordsRC.down] = CoordsRC.left,
        };
        static readonly Dictionary<Pos, Pos> mirrorBackslash = new()
        {
            [CoordsRC.right] = CoordsRC.down,
            [CoordsRC.left] = CoordsRC.up,
            [CoordsRC.up] = CoordsRC.left,
            [CoordsRC.down] = CoordsRC.right,
        };
        static int CountTiles(Map m, Beam start)
        {
            HashSet<Beam> visited = new();
            Queue<Beam> toVisit = new(new[] { start });
            while (toVisit.Any())
            {
                Beam cur = toVisit.Dequeue();
                visited.Add(cur);
                List<Beam> nextList = new();
                char c = m[cur.p];
                Beam nextBeam = new(cur.p, cur.dir);
                if (c == '/')
                    nextBeam.dir = mirrorSlash[cur.dir];
                else if (c == '\\')
                    nextBeam.dir = mirrorBackslash[cur.dir];
                else if (c == '-' && (cur.dir == CoordsRC.up || cur.dir == CoordsRC.down))
                {
                    nextBeam.dir = CoordsRC.left;
                    nextList.Add(nextBeam);
                    nextBeam.dir = CoordsRC.right;
                }
                else if (c == '|' && (cur.dir == CoordsRC.left || cur.dir == CoordsRC.right))
                {
                    nextBeam.dir = CoordsRC.up;
                    nextList.Add(nextBeam);
                    nextBeam.dir = CoordsRC.down;
                }
                nextList.Add(nextBeam);
                foreach (Beam b in nextList)
                {
                    Beam beam = new(b.p + b.dir, b.dir);
                    if (!visited.Contains(beam) && m.HasPosition(beam.p))
                        toVisit.Enqueue(beam);
                }
            }
            return visited.Select(b => b.p).ToHashSet().Count;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            Map m = Map.Build(input);
            int a = CountTiles(m, new Beam(new Pos(), CoordsRC.right));
            int b = 0;
            for (int i = 0; i < m.width; i++)
            {
                b = Math.Max(b, CountTiles(m, new Beam(new Pos(i, 0), CoordsRC.down)));
                b = Math.Max(b, CountTiles(m, new Beam(new Pos(i, m.height - 1), CoordsRC.up)));
            }
            for (int i = 0; i < m.height; i++)
            {
                b = Math.Max(b, CountTiles(m, new Beam(new Pos(0, i), CoordsRC.right)));
                b = Math.Max(b, CountTiles(m, new Beam(new Pos(m.width - 1, i), CoordsRC.left)));
            }
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
