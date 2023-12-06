using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day06
    {
        // Today: 
        public static Object PartA(string file)
        {
            var input = ReadInput.Strings(Day, file);
            var times = input[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse).ToList();
            var dists = input[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse).ToList();
            long a = 1;
            for (int n = 0; n < times.Count; n++)
            {
                long time = times[n];
                long limit = dists[n];
                int nLarger = 0;
                for (int i = 1; i < time; i++)
                {
                    long dist = i * (time - i);
                    if (dist > limit)
                        nLarger++;
                }
                a *= nLarger;
            }
            return a;
        }
        public static Object PartB(string file)
        {
            var input = ReadInput.Strings(Day, file);
            var time0 = input[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1);
            var time = long.Parse(string.Join("", time0));
            var dist0 = input[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1);
            var distLimit = long.Parse(string.Join("", dist0));
            int nLarger = 0;
            for (long i = 1; i < time; i++)
            {
                long dist = i * (time - i);
                if (dist > distLimit)
                    nLarger++;
                if (i % 1000000 == 0)
                    Console.WriteLine("Done {0}", ((double)i) / time);
            }
            return nLarger;
        }
        static void Main() => Aoc.Execute(Day, PartA, PartB);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
