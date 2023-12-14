using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day12
    {
        // Hot Springs: Evil impossible pattern matching (brute force won't cut it)
        public static Object PartA(string file)
        {
            var input = ReadInput.StringLists(Day, file);
            int sum = 0;
            foreach (var item in input)
            {
                int possibleMatches = 0;
                var broken = item[1].Split(",").Select(int.Parse);
                uint maxVal = (uint)Math.Pow(2, item[0].Length) - 1;
                uint onePattern = 0;
                uint mustOnePattern = 0;
                string s = String.Concat(item[0].Reverse());
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] != '.')
                        onePattern |= 1u << i;
                    if (s[i] == '#')
                        mustOnePattern |= 1u << i;
                }
                uint zeroPattern = ~onePattern;
                //Console.WriteLine(Convert.ToString(onePattern, 2).PadLeft(32, '_'));
                //Console.WriteLine(Convert.ToString(zeroPattern, 2).PadLeft(32, '_'));
                //Console.WriteLine();
                var brokenReversed = broken.Reverse().ToList();
                for (uint i = 0; i <= maxVal; i++)
                {
                    if ((i & zeroPattern) == 0 && (i & mustOnePattern) == mustOnePattern)
                    {
                        var nums = new List<int>();
                        int nOnes = 0;
                        int nWhenLastGroupAdded = 0;
                        uint testPattern = i;
                        for (int n = 0; n < 32; n++)
                        {
                            if (((1 << n) & testPattern) != 0)
                            {
                                nOnes++;
                            }
                            else if (nOnes > 0)
                            {
                                nums.Add(nOnes);
                                nOnes = 0;
                                nWhenLastGroupAdded = n;
                            }
                        }
                        if (nums.SequenceEqual(brokenReversed))
                        {
                            //Console.WriteLine(testPattern.ToString("X"));
                            string b0 = Convert.ToString(testPattern, 2);
                            string b = b0.PadLeft(item[0].Length, '_');
                            //Console.WriteLine(item[0]);
                            //Console.WriteLine(b);
                            //Console.WriteLine();
                            possibleMatches++;
                        }
                    }
                }
                //Console.WriteLine(possibleMatches);
                //Console.WriteLine();
                sum += possibleMatches;
            }
            return sum;
        }
        public static Object PartB(string file)
        {
            var v = ReadInput.Strings(Day, file);
            return -1;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            return (PartA(file), PartB(file));
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
