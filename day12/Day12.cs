using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day12
    {
        // Hot Springs: Evil impossible pattern matching (brute force won't cut it)
        static Dictionary<string, long> cache = new();
        static long GetArrangements(string pattern, List<int> groups)
        {
            string key = pattern + string.Join(',', groups);
            if (!cache.ContainsKey(key))
                cache[key] = ComputeArrangements(pattern, groups);
            return cache[key];
        }
        static long ComputeArrangements(string pattern, List<int> groups)
        {
            while (true)
            {
                if (groups.Count == 0)
                    return pattern.Contains('#') ? 0 : 1;
                if (string.IsNullOrEmpty(pattern))
                    return 0;
                if (pattern.StartsWith('?'))
                    return GetArrangements("." + pattern[1..], groups) + GetArrangements("#" + pattern[1..], groups);
                if (pattern.StartsWith('.'))
                {
                    pattern = pattern.TrimStart('.');
                    continue;
                }
                if (pattern.StartsWith('#'))
                {
                    if (groups.Count == 0 || pattern.Length < groups[0] || pattern[..groups[0]].Contains('.'))
                        return 0;
                    if (groups.Count > 1)
                    {
                        if (pattern.Length < groups[0] + 1 || pattern[groups[0]] == '#')
                            return 0;
                        pattern = pattern[(groups[0] + 1)..];
                        groups = groups.Skip(1).ToList();
                        continue;
                    }
                    pattern = pattern[groups[0]..];
                    groups = groups.Skip(1).ToList();
                    continue;
                }
                throw new Exception("Bad pattern");
            }
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.StringLists(Day, file);
            long a = 0, b = 0;
            foreach (var sl in input)
            {
                var pattern = sl[0];
                var groups = sl[1].Split(',').Select(int.Parse).ToList();
                a += GetArrangements(pattern, groups);
                pattern = string.Join('?', Enumerable.Repeat(pattern, 5));
                var groups5Str = string.Join(',', Enumerable.Repeat(sl[1], 5));
                groups = groups5Str.Split(',').Select(int.Parse).ToList();
                b += GetArrangements(pattern, groups);
            }
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
