using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day16
    {
        // Today: 
        record struct Beam(Pos p, Pos dir);
        static int CountTiles(Map m, Beam start)
        {
            HashSet<Beam> visited = new();
            List<Beam> toVisit = new() { start };
            while (toVisit.Any())
            {
                var cur = toVisit.First();
                toVisit.RemoveAt(0);
                List<Beam> nextList = new();
                visited.Add(cur);
                char c = m[cur.p];
                Beam nextBeam = new(cur.p, cur.dir);
                if (c == '.')
                    nextList.Add(nextBeam);
                else if (c == '/')
                {
                    if (cur.dir == CoordsRC.right)
                        nextBeam.dir = CoordsRC.up;
                    else if (cur.dir == CoordsRC.left)
                        nextBeam.dir = CoordsRC.down;
                    else if (cur.dir == CoordsRC.up)
                        nextBeam.dir = CoordsRC.right;
                    else if (cur.dir == CoordsRC.down)
                        nextBeam.dir = CoordsRC.left;
                    nextList.Add(nextBeam);
                }
                else if (c == '\\')
                {
                    if (cur.dir == CoordsRC.right)
                        nextBeam.dir = CoordsRC.down;
                    else if (cur.dir == CoordsRC.left)
                        nextBeam.dir = CoordsRC.up;
                    else if (cur.dir == CoordsRC.up)
                        nextBeam.dir = CoordsRC.left;
                    else if (cur.dir == CoordsRC.down)
                        nextBeam.dir = CoordsRC.right;
                    nextList.Add(nextBeam);
                }
                else if (c == '-')
                {
                    if (cur.dir == CoordsRC.up || cur.dir == CoordsRC.down)
                    {
                        nextBeam.dir = CoordsRC.left;
                        nextList.Add(nextBeam);
                        nextBeam.dir = CoordsRC.right;
                    }
                    nextList.Add(nextBeam);
                }
                else if (c == '|')
                {
                    if (cur.dir == CoordsRC.left || cur.dir == CoordsRC.right)
                    {
                        nextBeam.dir = CoordsRC.up;
                        nextList.Add(nextBeam);
                        nextBeam.dir = CoordsRC.down;
                    }
                    nextList.Add(nextBeam);
                }
                foreach (Beam b in nextList)
                {
                    Beam beam = new(b.p + b.dir, b.dir);
                    if (!visited.Contains(beam) && m.HasPosition(beam.p))
                        toVisit.Add(beam);
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
