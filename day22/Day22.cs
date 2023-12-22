using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;
using Pos3 = AdventOfCode.GenericPosition3D<int>;

namespace aoc
{
    public class Day22
    {
        // Today: 
        static Dictionary<Pos, List<(int h, int id)>> StackCuboids(List<(Pos3 p1, Pos3 p2, int id)> cuboids, int skipId = -1)
        {
            Dictionary<Pos, List<(int h, int id)>> stack = new();
            var allPos = cuboids.Select(w => new List<Pos3>() { w.p1, w.p2 }).SelectMany(w => w).ToList();
            (int x1, int x2) = allPos.Select(w => w.x).MinMax();
            (int y1, int y2) = allPos.Select(w => w.y).MinMax();
            for (int y = y1; y <= y2; y++)
                for (int x = x1; x <= x2; x++)
                    stack[new Pos(x, y)] = new() { (0, -1) };
            foreach ((Pos3 p1, Pos3 p2, int id) in cuboids)
            {
                if (id == skipId)
                    continue;
                int hBase = int.MinValue;
                for (int y = p1.y; y <= p2.y; y++)
                    for (int x = p1.x; x <= p2.x; x++)
                        hBase = Math.Max(hBase, stack[new Pos(x, y)].Last().h);
                for (int z = p1.z; z <= p2.z; z++)
                    for (int y = p1.y; y <= p2.y; y++)
                        for (int x = p1.x; x <= p2.x; x++)
                            stack[new Pos(x, y)].Add((hBase + 1 + (z - p1.z), id));
            }
            return stack;
        }
        static int MovedCuboidsCount(Dictionary<Pos, List<(int h, int id)>> sFull, Dictionary<Pos, List<(int h, int id)>> sCmp, int ignoreId)
        {
            HashSet<int> idsMoved = new();
            foreach (var (pos, pile) in sFull)
            {
                var pileCmp = sCmp[pos].ToDictionary(w => w.h, w => w.id);
                foreach (var (h, id) in pile)
                    if ((id != ignoreId) && (!pileCmp.ContainsKey(h) || pileCmp[h] != id))
                        idsMoved.Add(id);
            }
            return idsMoved.Count;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            List<(Pos3 p1, Pos3 p2, int id)> cuboids = new();
            foreach (var (s, idx) in input.Select((s, i) => (s, i)))
            {
                var i = Extract.Ints(s);
                cuboids.Add((new Pos3(i[0], i[1], i[2]), new Pos3(i[3], i[4], i[5]), idx));
            }
            cuboids = cuboids.OrderBy(w => w.p1.z).ToList();
            var fullStack = StackCuboids(cuboids);
            int a = 0, b = 0;
            foreach (var c in cuboids)
            {
                int n = MovedCuboidsCount(fullStack, StackCuboids(cuboids, c.id), c.id);
                b += n;
                if (n == 0)
                    a++;
            }
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
