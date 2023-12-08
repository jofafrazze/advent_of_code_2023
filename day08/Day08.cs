using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day08
    {
        // Today: 
        public static Object PartA(string file)
        {
            var input = ReadInput.StringGroups(Day, file);
            string leftRight = input[0][0];
            int leftRightPos = 0;
            var map = new Dictionary<string, List<string>>();
            foreach (var item in input[1])
            {
                var s = item.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var l = new List<string>();
                l.Add(s[2][1..^1]);
                l.Add(s[3][0..^1]);
                map[s[0]] = l;
            }
            int steps = 0;
            string pos = "AAA";
            while (true)
            {
                steps++;
                bool goLeft = leftRight[leftRightPos++ % leftRight.Length] == 'L';
                pos = map[pos][goLeft ? 0 : 1];
                if (pos == "ZZZ")
                    return steps;
            }
        }
        public static Object PartB(string file)
        {
            var input = ReadInput.StringGroups(Day, file);
            string leftRight = input[0][0];
            int leftRightPos = 0;
            var map = new Dictionary<string, List<string>>();
            foreach (var item in input[1])
            {
                var s = item.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var l = new List<string>();
                l.Add(s[2][1..^1]);
                l.Add(s[3][0..^1]);
                map[s[0]] = l;
            }
            List<int> cycleLenghts = new List<int>();
            List<string> posList = new();
            foreach (var item in map.Keys)
                if (item[2] == 'A')
                    posList.Add(item);
            foreach (string pos0 in posList)
            {
                string pos = pos0;
                int steps = 0;
                leftRightPos = 0;
                bool done = false;
                while (!done)
                {
                    steps++;
                    bool goLeft = leftRight[leftRightPos++ % leftRight.Length] == 'L';
                    pos = map[pos][goLeft ? 0 : 1];
                    done = pos[2] == 'Z';
                    if (done)
                        cycleLenghts.Add(steps);
                }
            }
            long big = 1;
            foreach (int i in cycleLenghts)
                big = big * i / Utils.GCF(big, i);
            return big;
        }
        static void Main() => Aoc.Execute(Day, PartA, PartB);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
