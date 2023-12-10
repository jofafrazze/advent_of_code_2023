using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day10
    {
        // Today: 
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            var m = Map.Build(input);
            var mDebug = Map.Build(input);
            Pos p0 = m.Positions().Where(x => m[x] == 'S').First();
            Dictionary<Pos, int> dists = new();
            dists[p0] = 0;
            List<Pos> toTry = new() { p0 };
            var toTile = new Dictionary<char, List<Pos>>() {
                ['|'] = new List<Pos> { new Pos(0, -1), new Pos(0, 1) },
                ['-'] = new List<Pos> { new Pos(-1, 0), new Pos(1, 0) },
                ['L'] = new List<Pos> { new Pos(0, -1), new Pos(1, 0) },
                ['J'] = new List<Pos> { new Pos(-1, 0), new Pos(0, -1) },
                ['7'] = new List<Pos> { new Pos(-1, 0), new Pos(0, 1) },
                ['F'] = new List<Pos> { new Pos(1, 0), new Pos(0, 1) },
                ['.'] = new List<Pos>(),
                ['S'] = new List<Pos>(),
            };
            // Replace S with the correct pipe
            foreach (var (k, v) in toTile)
            {
                int nDirs = 0;
                foreach (Pos delta in v)
                {
                    Pos pTry = p0 + delta;
                    if (m.HasPosition(pTry))
                    {
                        foreach (Pos pTryCanGoDelta in toTile[m[pTry]])
                        {
                            if (pTry + pTryCanGoDelta == p0)
                                nDirs++;
                        }
                    }
                }
                if (nDirs == 2)
                    m[p0] = k;
            }
            m.Print();
            while (toTry.Count > 0)
            {
                var pNow = toTry[0];
                toTry = toTry.Skip(1).ToList();
                foreach (Pos p in CoordsXY.directions4.Select(w => pNow + w))
                {
                    if (m.HasPosition(p))
                    {
                        bool canGoThere = toTile[m[pNow]].Select(w => pNow + w).Contains(p);
                        bool canGoBack = toTile[m[p]].Select(w => p + w).Contains(pNow);
                        if (canGoThere && canGoBack && !dists.ContainsKey(p))
                        {
                            dists[p] = dists[pNow] + 1;
                            toTry.Add(p);
                            //mDebug[p] = 'X';
                            //Console.Clear();
                            //mDebug.Print();
                            //int hej = 1;
                        }
                    }
                }
            }
            //m.Print();
            int a = dists.Values.Max();
            Map m2 = new Map(m.width * 2 + 1, m.height * 2 + 1, '.');
            for (int y = 0; y < m.height; y++)
            {
                for (int x = 0; x < m.width; x++)
                {
                    m2.data[x * 2 + 1, y * 2 + 1] = m.data[x, y];
                    Pos p = new(x, y);
                    if (x < m.width - 1)
                    {
                        Pos p2 = new(x + 1, y);
                        if (dists.ContainsKey(p) && dists.ContainsKey(p2))
                        {
                            var q0 = toTile[m[p]].Select(w => new Pos(p.x * 2 + 1 + w.x, p.y * 2 + 1 + w.y));
                            var q1 = toTile[m[p2]].Select(w => new Pos(p2.x * 2 + 1 + w.x, p2.y * 2 + 1 + w.y));
                            var q = q0.Intersect(q1).ToList();
                            if (q.Count > 0)
                                m2[q[0]] = '-';
                        }
                    }
                    if (y < m.height - 1)
                    {
                        Pos p2 = new(x, y + 1);
                        if (dists.ContainsKey(p) && dists.ContainsKey(p2))
                        {
                            var q0 = toTile[m[p]].Select(w => new Pos(p.x * 2 + 1 + w.x, p.y * 2 + 1 + w.y));
                            var q1 = toTile[m[p2]].Select(w => new Pos(p2.x * 2 + 1 + w.x, p2.y * 2 + 1 + w.y));
                            var q = q0.Intersect(q1).ToList();
                            if (q.Count > 0)
                                m2[q[0]] = '|';
                        }
                    }
                }
            }
            //Console.WriteLine();
            //m2.Print();

            HashSet<Pos> posOutside = new() { new Pos() };
            List<Pos> toGo = new() { new Pos() };
            while (toGo.Count > 0)
            {
                var pNow = toGo[0];
                toGo = toGo.Skip(1).ToList();
                foreach (Pos p in CoordsRC.directions8.Select(w => pNow + w))
                {
                    if (m2.HasPosition(p) && !posOutside.Contains(p))
                    {
                        Pos pSmallMap = new((p.x - 1) / 2, (p.y - 1) / 2);
                        if (m2[p] == '.' || !dists.ContainsKey(pSmallMap))
                        {
                            toGo.Add(p);
                            posOutside.Add(p);
                        }
                    }
                }
            }
            foreach (Pos p in posOutside)
                m2[p] = 'O';
            Console.WriteLine();
            m2.Print();

            var inOrOutPos = m.Positions().Where(p => !dists.ContainsKey(p)).ToList();
            var inPos = inOrOutPos.Where(p => m2.data[p.x * 2 + 1, p.y * 2 + 1] != 'O').ToList();
            foreach (var p in dists.Keys)
                m[p] = '+';
            foreach (var p in inOrOutPos)
                m[p] = 'O';
            foreach (var p in inPos)
                m[p] = '\'';
            Console.WriteLine();
            m.Print();
            int b = inPos.Count();
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
