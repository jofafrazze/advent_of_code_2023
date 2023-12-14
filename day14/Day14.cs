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
        static Map TiltNorth(Map mIn) 
        {
            Map m = new(mIn);
            while (PermuteNorth(m)) 
                ; 
            return m; 
        }
        static int Score(Map m) => m.Positions().Where(p => m[p] == 'O').Select(p => m.height - p.y).Sum();
        static Map TiltWest(Map m) => Map.TurnCW(TiltNorth(Map.TurnCCW(m)));
        static Map TiltSouth(Map m) => Map.TurnCW(Map.TurnCW(TiltNorth(Map.TurnCCW(Map.TurnCCW(m)))));
        static Map TiltEast(Map m) => Map.TurnCCW(TiltNorth(Map.TurnCW(m)));
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            Map prev, cur = Map.Build(input);
            int a = Score(TiltNorth(cur));
            int i = 0;
            Dictionary<Map, int> been = new() { [cur] = i };
            while (true)
            {
                i++;
                prev = cur;
                cur = TiltEast(TiltSouth(TiltWest(TiltNorth(cur))));
                if (been.TryGetValue(cur, out int offs))
                {
                    int cycleLen = i - offs;
                    int finalIdx = offs + (1000_000_000 - offs) % cycleLen;
                    return (a, Score(been.Where(w => w.Value == finalIdx).Select(w => w.Key).First()));
                }
                been[cur] = i;
            }
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
