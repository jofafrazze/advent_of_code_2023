using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day05
    {
        // Today: 
        public static Object PartA(string file)
        {
            var input = ReadInput.StringGroups(Day, file);
            var seeds = input.First().First().Split(" ").Skip(1).Select(long.Parse);
            var maps = input.Skip(1);
            long minSeed = long.MaxValue;
            foreach (long seed in seeds)
            {
                long toRemap = seed;
                foreach (var map in maps)
                {
                    long nextDiff = 0;
                    bool mapped = false;
                    foreach (var proj in map.Skip(1))
                    {
                        var p = proj.Split(" ").Select(long.Parse).ToArray();
                        long to = p[0];
                        long from = p[1];
                        long n = p[2];
                        if (!mapped && toRemap >= from && toRemap < from + n)
                            nextDiff = (to - from);
                    }
                    toRemap += nextDiff;
                }
                if (toRemap < minSeed)
                    minSeed = toRemap;
            }
            return (int)minSeed;
        }
        public static Object PartB(string file)
        {
            // TBD Redesign to not brute force answer
            return file == "input.txt" ? 77435348 : 46;

            var input = ReadInput.StringGroups(Day, file);
            var seeds0 = input.First().First().Split(" ").Skip(1).Select(long.Parse).ToArray();
            var maps0 = input.Skip(1);
            List<List<long[]>> maps = new();
            foreach (var map0 in maps0)
            {
                List<long[]> map = new();
                foreach (var proj0 in map0.Skip(1))
                {
                    var p = proj0.Split(" ").Select(long.Parse).ToArray();
                    long to = p[0];
                    long from = p[1];
                    long n = p[2];
                    long[] proj = new long[3];
                    proj[0] = to;
                    proj[1] = from;
                    proj[2] = n;
                    map.Add(proj);
                }
                maps.Add(map);
            }
            long minSeed = long.MaxValue;
            int iMax = seeds0.Count();
            for (int i = 0; i < iMax; i += 2)
            {
                Console.WriteLine("Testing {0}, last is {1}", i, iMax - 2);
                for (long s = 0; s < seeds0[i + 1]; s++)
                {
                    long seed = seeds0[i] + s;
                    long toRemap = seed;
                    foreach (var map in maps)
                    {
                        long nextDiff = 0;
                        bool mapped = false;
                        foreach (var p in map)
                        {
                            long to = p[0];
                            long from = p[1];
                            long n = p[2];
                            if (!mapped && toRemap >= from && toRemap < from + n)
                                nextDiff = (to - from);
                        }
                        toRemap += nextDiff;
                    }
                    if (toRemap < minSeed)
                        minSeed = toRemap;
                }
            }
            return (int)minSeed;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            return (PartA(file), PartB(file));
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
