using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day14
    {
        // Parabolic Reflector Dish: Move chars around in 2D, find repeating cycle
        static bool PermuteNorth(Map m)
        {
            bool changed = false;
            for (int y = 0; y < m.height - 1; y++)
                for (int x = 0; x < m.width; x++)
                    if (m.data[x, y] == '.' && m.data[x, y + 1] == 'O')
                    {
                        m.data[x, y] = 'O';
                        m.data[x, y + 1] = '.';
                        changed = true;
                    }
            return changed;
        }
        static Map TiltNorth(Map m) { while (PermuteNorth(m)) ; return m; }
        static bool PermuteSouth(Map m)
        {
            bool changed = false;
            for (int y = m.height - 2; y >= 0; y--)
                for (int x = 0; x < m.width; x++)
                    if (m.data[x, y] == 'O' && m.data[x, y + 1] == '.')
                    {
                        m.data[x, y] = '.';
                        m.data[x, y + 1] = 'O';
                        changed = true;
                    }
            return changed;
        }
        static Map TiltSouth(Map m) { while (PermuteSouth(m)) ; return m; }
        static bool PermuteWest(Map m)
        {
            bool changed = false;
            for (int y = 0; y < m.height; y++)
                for (int x = 0; x < m.width - 1; x++)
                    if (m.data[x, y] == '.' && m.data[x + 1, y] == 'O')
                    {
                        m.data[x, y] = 'O';
                        m.data[x + 1, y] = '.';
                        changed = true;
                    }
            return changed;
        }
        static Map TiltWest(Map m) { while (PermuteWest(m)) ; return m; }
        static bool PermuteEast(Map m)
        {
            bool changed = false;
            for (int y = 0; y < m.height; y++)
                for (int x = m.width - 2; x >= 0; x--)
                    if (m.data[x + 1, y] == '.' && m.data[x, y] == 'O')
                    {
                        m.data[x, y] = '.';
                        m.data[x + 1, y] = 'O';
                        changed = true;
                    }
            return changed;
        }
        static Map TiltEast(Map m) { while (PermuteEast(m)) ; return m; }
        static int Score(Map m) => m.Positions().Where(p => m[p] == 'O').Select(p => m.height - p.y).Sum();
        static Map TurnCW(Map mIn)
        {
            Map m = new(mIn);
            for (int y = 0; y < m.height; y++)
                for (int x = 0; x < m.width; x++)
                    m.data[x, y] = mIn.data[m.width - 1 - y, x];
            return m;
        }
        static Map TurnCCW(Map mIn)
        {
            Map m = new(mIn);
            for (int y = 0; y < m.height; y++)
                for (int x = 0; x < m.width; x++)
                    m.data[m.width - 1 - y, x] = mIn.data[x, y];
            return m;
        }
        static Map FlipWest(Map mIn)
        {
            Map m = new(mIn);
            m = TurnCCW(m);
            TiltNorth(m);
            m = TurnCW(m);
            return m;
        }
        static Map FlipSouth(Map mIn)
        {
            Map m = new(mIn);
            m = TurnCCW(m);
            m = TurnCCW(m);
            TiltNorth(m);
            m = TurnCW(m);
            m = TurnCW(m);
            return m;
        }
        static Map FlipEast(Map mIn)
        {
            Map m = new(mIn);
            m = TurnCW(m);
            TiltNorth(m);
            m = TurnCCW(m);
            return m;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            Map mIn = Map.Build(input);
            Map ma = new(mIn);
            TiltNorth(ma);
            int a = Score(ma);
            Map cur = new(mIn);
            Map prev;
            int i = 0;
            Dictionary<Map, int> been = new() { [cur] = i };
            do
            {
                i++;
                prev = cur;
                TiltNorth(cur);
                TiltWest(cur);
                TiltSouth(cur);
                TiltEast(cur);
                Map c = new(cur);
                if (!been.ContainsKey(c))
                    been[c] = i;
                else
                {
                    int offs = been[c];
                    int cycleLen = i - offs;
                    int idx = offs + (1000_000_000 - offs) % cycleLen;
                    int b = Score(been.Where(w => w.Value == idx).Select(w => w.Key).First());
                    return (a, b);
                }
            }
            while (true);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
