using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day04
    {
        // Today: 
        public static Object PartA(string file)
        {
            var input = ReadInput.StringLists(Day, file);
            int sum = 0;
            foreach (var sl in input)
            {
                var s = string.Join(" ", sl.Skip(2));
                var round = s.Split("|", StringSplitOptions.RemoveEmptyEntries);
                var winning = round[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
                var your = round[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
                int n = your.Where(x => winning.Contains(x)).Count();
                sum += (int)Math.Pow(2, n - 1);
            }
            return sum;
        }
        public static Object PartB(string file)
        {
            var input = ReadInput.StringLists(Day, file);
            int sum = 0;
            var nCards = new Dictionary<int, int>();
            int curCard = 1;
            foreach (var sl in input)
            {
                var s = string.Join(" ", sl.Skip(2));
                var round = s.Split("|", StringSplitOptions.RemoveEmptyEntries);
                var winning = round[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
                var your = round[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
                nCards.Inc(curCard, 1);
                int n = your.Where(x => winning.Contains(x)).Count();
                for (int i = 0; i < n; i++)
                    nCards.Inc(curCard + 1 + i, nCards[curCard]);
                sum += nCards[curCard];
                curCard++;
            }
            return sum;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            return (PartA(file), PartB(file));
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
