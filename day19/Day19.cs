using AdventOfCode;
using System.Reflection;
using Range = AdventOfCode.IntRange;

namespace aoc
{
    public class Day19
    {
        // Today: 
        record struct Xmas(int x, int m, int a, int s);
        static string Parse(List<string> rules, Xmas xmas)
        {
            foreach (string rule in rules)
            {
                if (rule.Length > 1 && (rule[1] == '<' || rule[1] == '>'))
                {
                    char c = rule[0];
                    var colon = rule.Split(':');
                    int value = int.Parse(colon[0][2..]);
                    int xmasCmp = 0;
                    if (c == 'x')
                        xmasCmp = xmas.x;
                    else if (c == 'm')
                        xmasCmp = xmas.m;
                    else if (c == 'a')
                        xmasCmp = xmas.a;
                    else
                        xmasCmp = xmas.s;
                    if (rule[1] == '<' && xmasCmp < value)
                        return colon[1];
                    if (rule[1] == '>' && xmasCmp > value)
                        return colon[1];
                }
                else
                    return rule;
            }
            throw new Exception("Noooo!");
        }
        public static Object PartA(string file)
        {
            var input = ReadInput.StringGroups(Day, file);
            Dictionary<string, List<string>> workflows = new();
            List<Xmas> xmasIn = new();
            foreach (var line in input[1])
            {
                var n = Extract.Ints(line);
                xmasIn.Add(new Xmas(n[0], n[1], n[2], n[3]));
            }
            foreach (var line in input[0])
            {
                var s1 = line.Split('{');
                var rules = s1[1][0..^1].Split(',').ToList();
                workflows[s1[0]] = rules;
            }
            int sum = 0;
            foreach (var w in xmasIn)
            {
                string res = Parse(workflows["in"], w);
                while (res != "A" && res != "R")
                    res = Parse(workflows[res], w);
                if (res == "A")
                    sum += w.x + w.m + w.a + w.s;
            }
            return sum;
        }
        static long ParseB(Dictionary<string, List<string>> workflows, Dictionary<char, List<Range>> ranges, string key)
        {
            if (key == "A")
            {
                long res = 1;
                foreach (var (k, rl) in ranges)
                    res *= rl.Select(w => w.Size()).Sum();
                return res;
            }
            else if (key == "R")
                return 0;
            string rule = workflows[key][0];
            if (rule.Length > 1 && (rule[1] == '<' || rule[1] == '>'))
            {
                char c = rule[0];
                var colon = rule.Split(':');
                int value = int.Parse(colon[0][2..]);
                bool lt = rule[1] == '<';
                Range pass = new(lt ? 1 : value + 1, lt ? value - 1 : 4000);
                Range noPass = new(lt ? value : 1, lt ? 4000 : value);

                long passValue = 0;
                long noPassValue = 0;
                {
                    var wfPass = workflows.ToDictionary(w => w.Key, w => w.Value.ToList());
                    var rangesPass = ranges.ToDictionary(w => w.Key, w => w.Value.ToList());
                    var rPass = new List<Range>();
                    foreach (var range in rangesPass[c])
                        rPass.AddRange(pass.Intersect(range));
                    rangesPass[c] = Range.NormalizeUnion(rPass);
                    wfPass[key] = wfPass[key].Skip(1).ToList();
                    passValue = ParseB(wfPass, rangesPass, colon[1]);
                }
                {
                    var wfNoPass = workflows.ToDictionary(w => w.Key, w => w.Value.ToList());
                    var rangesNoPass = ranges.ToDictionary(w => w.Key, w => w.Value.ToList());
                    var rNoPass = new List<Range>();
                    foreach (var range in rangesNoPass[c])
                        rNoPass.AddRange(noPass.Intersect(range));
                    rangesNoPass[c] = Range.NormalizeUnion(rNoPass);
                    wfNoPass[key] = wfNoPass[key].Skip(1).ToList();
                    noPassValue = ParseB(wfNoPass, rangesNoPass, key);
                }
                return passValue + noPassValue;
            }
            else
            {
                var wfPass = workflows.ToDictionary(w => w.Key, w => w.Value.ToList());
                var rangesPass = ranges.ToDictionary(w => w.Key, w => w.Value.ToList());
                return ParseB(wfPass, rangesPass, rule);
            }
            throw new Exception("Noooo B!");
        }
        public static Object PartB(string file)
        {
            var input = ReadInput.StringGroups(Day, file)[0];
            Dictionary<string, List<string>> workflows = new();
            foreach (var line in input)
            {
                var s1 = line.Split('{');
                var rules = s1[1][0..^1].Split(',').ToList();
                workflows[s1[0]] = rules;
            }
            Dictionary<char, List<Range>> ranges = new();
            ranges['x'] = new List<Range>() { new(1, 4000) };
            ranges['m'] = new List<Range>() { new(1, 4000) };
            ranges['a'] = new List<Range>() { new(1, 4000) };
            ranges['s'] = new List<Range>() { new(1, 4000) };
            return ParseB(workflows, ranges, "in");
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            return (PartA(file), PartB(file));
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
