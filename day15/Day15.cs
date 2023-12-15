using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day15
    {
        // Today: 
        static byte GetBox(string s)
        {
            byte a = 0;
            foreach (byte c in s)
            {
                a += c;
                a *= 17;
            }
            return a;
        }
        public static Object PartA(string file)
        {
            var input = ReadInput.StringLists(Day, file, ",");
            int sum = 0;
            foreach (var s in input[0])
                sum += GetBox(s);
            return sum;
        }
        public static Object PartB(string file)
        {
            var input = ReadInput.StringLists(Day, file, ",");
            int sum = 0;
            var boxes = new Dictionary<int, List<(string label, int focalLength)>>();
            for (int i = 0; i < 256; i++)
                boxes[i] = new List<(string label, int focalLength)>();
            foreach (var s in input[0])
            {
                var sl = s.Split("=-".ToCharArray()).ToList();
                string label = sl[0];
                byte box = GetBox(label);
                int labelIdx = -1;
                for (int i = 0; i < boxes[box].Count; i++)
                    if (boxes[box][i].label == label)
                        labelIdx = i;
                if (s[label.Length] == '=')
                {
                    string snum = s[label.Length + 1].ToString();
                    int num = int.Parse(snum);
                    if (labelIdx >= 0)
                        boxes[box][labelIdx] = (label, num);
                    else
                        boxes[box].Add((label, num));
                }
                else
                {
                    if (labelIdx >= 0)
                        boxes[box].RemoveAt(labelIdx);
                }
            }
            for (int i = 0; i < 256; i++)
            {
                var b = boxes[i];
                for (int n = 0; n < b.Count; n++)
                {
                    sum += (i + 1) * (n + 1) * b[n].focalLength;
                }
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
