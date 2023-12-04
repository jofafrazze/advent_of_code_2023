using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day01
    {
        // Today: 
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            int asum = 0, bsum = 0;
            foreach (var s in input)
            {
                int t = s.IndexOfAny("0123456789".ToCharArray());
                int u = s.LastIndexOfAny("0123456789".ToCharArray());
                string n = s[t].ToString() + s[u];
                int a = int.Parse(n);
                asum += a;
            }
            foreach (var s in input)
            {
                var nums = new List<string> { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
                int t0 = s.IndexOfAny("0123456789".ToCharArray());
                int u0 = s.LastIndexOfAny("0123456789".ToCharArray());
                int t = -1, u = -1;
                int tPos = -1, uPos = -1;
                for (int j = 0; j < s.Length && t < 0; j++)
                {
                    for (int i = 0; i < nums.Count && t < 0; i++)
                    {
                        if (s.Substring(j).StartsWith(nums[i]))
                        {
                            t = i;
                            tPos = j;
                        }
                    }
                }
                for (int j = s.Length - 1; j >= 0 && u < 0; j--)
                {
                    for (int i = 0; i < nums.Count && u < 0; i++)
                    {
                        if (s.Substring(j).StartsWith(nums[i]))
                        {
                            u = i;
                            uPos = j;
                        }
                    }
                }
                char c0 = tPos < 0 || t0 < tPos ? s[t0] : (char)('0' + t);
                char c1 = uPos < 0 || u0 > uPos ? s[u0] : (char)('0' + u);
                string n = c0.ToString() + c1.ToString();
                int b = int.Parse(n);
                bsum += b;
            }
            return (asum, bsum);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
