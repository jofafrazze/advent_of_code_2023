using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day18
    {
        // Today: 
        static readonly Dictionary<char, Pos> directions = new()
        {
            ['U'] = CoordsRC.up,
            ['L'] = CoordsRC.left,
            ['D'] = CoordsRC.down,
            ['R'] = CoordsRC.right,
        };
        public static Object PartA(string file)
        {
            var input = ReadInput.StringLists(Day, file);
            Pos cur = new Pos();
            HashSet<Pos> been = new() { cur };
            foreach (var sl in input)
            {
                Pos dir = directions[sl[0][0]];
                for (int i = 0; i < int.Parse(sl[1]); i++)
                {
                    cur += dir;
                    been.Add(cur);
                }
            }
            int xmin = been.Select(w => w.x).Min();
            int xmax = been.Select(w => w.x).Max();
            int ymin = been.Select(w => w.y).Min();
            int ymax = been.Select(w => w.y).Max();
            int w = xmax - xmin + 1;
            int h = ymax - ymin + 1;
            Map m = new(w + 2, h + 2, '.');
            foreach(var p in been)
                m.data[p.x - xmin + 1, p.y - ymin + 1] = '#';
            m.Print();
            Console.WriteLine($"Map is w = {w}, h = {h}, thus area = {w * h}.");
            Queue<Pos> toFill = new();
            HashSet<Pos> visited = new();
            toFill.Enqueue(new Pos());
            int it = 0;
            while (toFill.Any())
            {
                it++;
                Pos p = toFill.Dequeue();
                if (visited.Contains(p))
                    continue;
                visited.Add(p);
                m[p] = '*';
                foreach (var d in CoordsRC.directions4)
                {
                    Pos p2 = p + d;
                    if (m.HasPosition(p2) && m[p2] == '.' && !visited.Contains(p2))
                        toFill.Enqueue(p2);
                }
                if (it % 100 == 0)
                {
                    Console.WriteLine($"Queue has {toFill.Count} positions left to try.");
                    //m.Print();
                }
            }
            m.Print();
            return m.Positions().Where(w => m[w] != '*').Count();
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
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
