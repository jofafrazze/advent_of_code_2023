using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day14
    {
        // Today: 
        static Map Permute(Map mIn)
        {
            Map m = new(mIn);
            for (int y = 0; y < m.height - 1; y++)
            {
                for (int x = 0; x < m.width; x++)
                {
                    if (m.data[x,y] == '.' && m.data[x, y + 1] == 'O')
                    {
                        m.data[x, y] = 'O';
                        m.data[x, y + 1] = '.';
                    }
                }
            }
            return m;
        }
        static Map FlipNorth(Map mIn)
        {
            Map cur = new Map(mIn);
            Map prev;
            do
            {
                prev = cur;
                cur = Permute(prev);
            }
            while (cur != prev);
            return cur;
        }
        static int Score(Map m)
        {
            int sum = 0;
            for (int y = 0; y < m.height; y++)
                for (int x = 0; x < m.width; x++)
                    if (m.data[x, y] == 'O')
                        sum += m.height - y;
            return sum;
        }
        public static Object PartA(string file)
        {
            var input = ReadInput.Strings(Day, file);
            Map mIn = Map.Build(input);
            Map m = FlipNorth(mIn);
            return Score(m);
        }
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
            m = FlipNorth(m);
            m = TurnCW(m);
            return m;
        }
        static Map FlipSouth(Map mIn)
        {
            Map m = new(mIn);
            m = TurnCCW(m);
            m = TurnCCW(m);
            m = FlipNorth(m);
            m = TurnCW(m);
            m = TurnCW(m);
            return m;
        }
        static Map FlipEast(Map mIn)
        {
            Map m = new(mIn);
            m = TurnCW(m);
            m = FlipNorth(m);
            m = TurnCCW(m);
            return m;
        }
        public static Object PartB(string file)
        {
            var input = ReadInput.Strings(Day, file);
            Map mIn = Map.Build(input);
            Map cur = new(mIn);
            Map prev;
            int i = 0;
            Dictionary<Map, int> been = new() { [cur] = i };
            do
            {
                i++;
                prev = cur;
                cur = FlipNorth(cur);
                cur = FlipWest(cur);
                cur = FlipSouth(cur);
                cur = FlipEast(cur);
                Map c = new(cur);
                if (!been.ContainsKey(c))
                    been[c] = i;
                else
                {
                    int offs = been[c];
                    int cycleLen = i - offs;
                    int idx = offs + (1000_000_000 - offs) % cycleLen;
                    return Score(been.Where(w => w.Value == idx).Select(w => w.Key).First());
                }
            }
            while (true);
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            return (PartA(file), PartB(file));
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
