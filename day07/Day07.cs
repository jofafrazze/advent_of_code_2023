using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day07
    {
        // Today: 
        static int HandToSortingValue(string hand)
        {
            string cardValue = "23456789TJQKA";
            int cardBase = 14;
            int baseValue = 0;
            int nCards = 5;
            for (int i = 0; i < hand.Length; i++)
            {
                char card = hand[i];
                int ci = cardValue.IndexOf(card) + 1;
                baseValue += ci * (int)Math.Pow(cardBase, nCards - 1 - i);
            }
            var freq = new Dictionary<char, int>();
            foreach (char c in hand)
                freq.Inc(c, 1);
            var sortedFreq = string.Join("", freq.Values.OrderByDescending(x => x).Select(x => x.ToString()));
            var hands = new Dictionary<string, int>() {
                { "11111", 1 },
                { "2111", 2 },
                { "221", 3 },
                { "311",  4 },
                { "32",  5 },
                { "41",  6 },
                { "5",  7 },
            };
            return baseValue + hands[sortedFreq] * (int)Math.Pow(cardBase, nCards);
        }
        public static Object PartA(string file)
        {
            var input = ReadInput.StringLists(Day, file);
            int sum = 0;
            var handsToSort = new Dictionary<int, int>();
            foreach(var line in input)
            {
                int x = HandToSortingValue(line[0]);
                handsToSort[x] = int.Parse(line[1]);
            }
            var sortedHands0 = handsToSort.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            var sortedHands = sortedHands0.Values.ToList();
            for (int i = 0; i < sortedHands.Count; i++)
            {
                sum += (i + 1) * sortedHands[i];
            }
            return sum;
        }
        static int HandToSortingValueB(string hand)
        {
            string cardValue = "J23456789TQKA";
            int cardBase = 14;
            int baseValue = 0;
            int nCards = 5;
            for (int i = 0; i < hand.Length; i++)
            {
                char card = hand[i];
                int ci = cardValue.IndexOf(card) + 1;
                baseValue += ci * (int)Math.Pow(cardBase, nCards - 1 - i);
            }
            var freq = new Dictionary<char, int>();
            int nj = 0;
            foreach (char c in hand)
            {
                if (c != 'J')
                    freq.Inc(c, 1);
                else
                    nj++;
            }

            var sortedFreq0 = freq.Values.OrderByDescending(x => x).ToList();
            if (sortedFreq0.Count == 0)
                sortedFreq0.Add(0);
            sortedFreq0[0] += nj;
            var sortedFreq = string.Join("", sortedFreq0.Select(x => x.ToString()));
            var hands = new Dictionary<string, int>() {
                { "11111", 1 },
                { "2111", 2 },
                { "221", 3 },
                { "311",  4 },
                { "32",  5 },
                { "41",  6 },
                { "5",  7 },
            };
            return baseValue + hands[sortedFreq] * (int)Math.Pow(cardBase, nCards);
        }
        public static Object PartB(string file)
        {
            var input = ReadInput.StringLists(Day, file);
            int sum = 0;
            var handsToSort = new Dictionary<int, int>();
            foreach (var line in input)
            {
                int x = HandToSortingValueB(line[0]);
                handsToSort[x] = int.Parse(line[1]);
            }
            var sortedHands0 = handsToSort.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            var sortedHands = sortedHands0.Values.ToList();
            for (int i = 0; i < sortedHands.Count; i++)
            {
                sum += (i + 1) * sortedHands[i];
            }
            return sum;
        }
        static void Main() => Aoc.Execute(Day, PartA, PartB);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
