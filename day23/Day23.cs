using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day23
    {
        // Today: 
        static readonly Dictionary<char, Pos> slopes = new()
        {
            ['v'] = CoordsRC.down,
            ['^'] = CoordsRC.up,
            ['<'] = CoordsRC.left,
            ['>'] = CoordsRC.right,
        };
        static int WalkMap(Map m, Pos start, Pos end)
        {
            HashSet<Pos> visited = new();
            Queue<Pos> toVisit = new(new[] { start });
            while (toVisit.Any())
            {
                Pos cur = toVisit.Dequeue();
                //visited.Add(cur);
                //List<Pos> nextList = new();
                //char c = m[cur.p];
                //Pos nextBeam = new(cur.p, cur.dir);
                //if (c == '/')
                //    nextBeam.dir = mirrorSlash[cur.dir];
                //else if (c == '\\')
                //    nextBeam.dir = mirrorBackslash[cur.dir];
                //else if (c == '-' && (cur.dir == CoordsRC.up || cur.dir == CoordsRC.down))
                //{
                //    nextBeam.dir = CoordsRC.left;
                //    nextList.Add(nextBeam);
                //    nextBeam.dir = CoordsRC.right;
                //}
                //else if (c == '|' && (cur.dir == CoordsRC.left || cur.dir == CoordsRC.right))
                //{
                //    nextBeam.dir = CoordsRC.up;
                //    nextList.Add(nextBeam);
                //    nextBeam.dir = CoordsRC.down;
                //}
                //nextList.Add(nextBeam);
                //foreach (Pos b in nextList)
                //{
                //    Pos beam = new(b.p + b.dir, b.dir);
                //    if (!visited.Contains(beam) && m.HasPosition(beam.p))
                //        toVisit.Enqueue(beam);
                //}
            }
            return visited.Count;
        }
        public static Object PartA(string file)
        {
            var input = ReadInput.Strings(Day, file);
            Map m = Map.Build(input);
            int a = WalkMap(m, new Pos(1, 0), new Pos(m.width - 2, m.height - 1));
            return 0;
        }
        public static Object PartB(string file)
        {
            var v = ReadInput.Strings(Day, file);
            return 0;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            return (PartA(file), PartB(file));
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle, true);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
