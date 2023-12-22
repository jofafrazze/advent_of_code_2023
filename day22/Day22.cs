using AdventOfCode;
using System.Reflection;
using Pos2 = AdventOfCode.GenericPosition2D<int>;
using Pos = AdventOfCode.GenericPosition3D<int>;

namespace aoc
{
    public class Day22
    {
        // Today: 
        static Dictionary<Pos2, List<(int h, int id)>> StackCuboids(List<(Pos p1, Pos p2, int id)> cuboids)
        {
            Dictionary<Pos2, List<(int h, int id)>> stack = new();
            var allPos = cuboids.Select(w => new List<Pos>() { w.p1, w.p2 }).SelectMany(w => w).ToList();
            int xmin = allPos.Select(w => w.x).Min();
            int xmax = allPos.Select(w => w.x).Max();
            int ymin = allPos.Select(w => w.y).Min();
            int ymax = allPos.Select(w => w.y).Max();
            for (int y = ymin; y <= ymax; y++)
                for (int x = xmin; x <= xmax; x++)
                    stack[new Pos2(x, y)] = new() { (0, -1) };
            foreach ((Pos p1, Pos p2, int id) in cuboids)
            {
                int x1 = Math.Min(p1.x, p2.x);
                int x2 = Math.Max(p1.x, p2.x);
                int y1 = Math.Min(p1.y, p2.y);
                int y2 = Math.Max(p1.y, p2.y);
                int hBase = int.MinValue;
                for (int y = y1; y <= y2; y++)
                    for (int x = x1; x <= x2; x++)
                        hBase = Math.Max(hBase, stack[new Pos2(x, y)].Last().h);
                int z1 = Math.Min(p1.z, p2.z);
                int z2 = Math.Max(p1.z, p2.z);
                for (int z = z1; z <= z2; z++)
                    for (int y = y1; y <= y2; y++)
                        for (int x = x1; x <= x2; x++)
                        {
                            int h = hBase + 1 + (z - z1);
                            stack[new Pos2(x, y)].Add((h, id));
                        }
            }
            return stack;
        }
        static bool StacksEqual(Dictionary<Pos2, List<(int h, int id)>> sFull, Dictionary<Pos2, List<(int h, int id)>> sCmp, int ignoreId)
        {
            foreach (var (pos, pile) in sFull)
            {
                var pile1 = pile.Where(w => w.id != ignoreId).ToList();
                if (!pile1.SequenceEqual(sCmp[pos]))
                    return false;
            }
            return true;
        }
        static void PrintStackX(Dictionary<Pos2, List<(int h, int id)>> stack)
        {
            List<string> strs = new();
            int hMax = stack.Values.Select(w => w.Last().h).Max();
            int amin = stack.Keys.Select(w => w.x).Min();
            int amax = stack.Keys.Select(w => w.x).Max();
            for (int h = hMax; h >= 0; h--)
            {
                string s = "";
                for (int a = amin; a <= amax; a++)
                {
                    var piles = stack.Where(w => w.Key.x == a);
                    var hPiles = piles.Where(w => w.Value.Select(v => v.h).Contains(h));
                    var ids = hPiles.Select(w => w.Value.Where(v => v.h == h).First()).Select(w => w.id).Distinct().ToList();
                    char c = ' ';
                    if (ids.Any())
                    {
                        int v1 = ids.First();
                        c = ids.Count == 1 ? (v1 == -1 ? '-' : (char)('A' + v1)) : '?';
                    }
                    s += c;
                }
                s += $" {h}";
                Console.WriteLine(s);
            }
            Console.WriteLine();
        }
        static void PrintStackY(Dictionary<Pos2, List<(int h, int id)>> stack)
        {
            List<string> strs = new();
            int hMax = stack.Values.Select(w => w.Last().h).Max();
            int amin = stack.Keys.Select(w => w.y).Min();
            int amax = stack.Keys.Select(w => w.y).Max();
            for (int h = hMax; h >= 0; h--)
            {
                string s = "";
                for (int a = amin; a <= amax; a++)
                {
                    var piles = stack.Where(w => w.Key.y == a);
                    var hPiles = piles.Where(w => w.Value.Select(v => v.h).Contains(h));
                    var ids = hPiles.Select(w => w.Value.Where(v => v.h == h).First()).Select(w => w.id).Distinct().ToList();
                    char c = ' ';
                    if (ids.Any())
                    {
                        int v1 = ids.First();
                        c = ids.Count == 1 ? (v1 == -1 ? '-' : (char)('A' + v1 % 26)) : '?';
                    }
                    s += c;
                }
                s += $" {h}";
                Console.WriteLine(s);
            }
            Console.WriteLine();
        }
        public static Object PartA(string file)
        {
            var input = ReadInput.Strings(Day, file);
            List<(Pos p1, Pos p2, int id)> cuboids = new();
            foreach (var (s, idx) in input.Select((s, i) => (s, i)))
            {
                var i = Extract.Ints(s);
                cuboids.Add((new Pos(i[0], i[1], i[2]), new Pos(i[3], i[4], i[5]), idx));
            }
            cuboids = cuboids.OrderBy(w => w.p1.z).ToList();
            //Console.WriteLine($"Read {cuboids.Count} cuboids.");
            var fullStack = StackCuboids(cuboids);
            //PrintStackX(fullStack);
            //PrintStackY(fullStack);
            int a = 0;
            foreach (var c in cuboids)
            {
                var stack = StackCuboids(cuboids.Where(w => w.id != c.id).ToList());
                //Console.WriteLine();
                //Console.WriteLine($"Id: {c.id}");
                //Console.WriteLine();
                //PrintStackX(fullStack);
                //PrintStackX(stack);
                //Console.WriteLine();
                if (StacksEqual(fullStack, stack, c.id))
                    a++;
            }
            return a;
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
