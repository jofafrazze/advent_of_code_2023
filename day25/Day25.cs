using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day25
    {
        // Today: 
        public static Object PartA(string file)
        {
            var input = ReadInput.Ints(Day, file);
            return 0;
        }
        public static Object PartB(string file)
        {
            var v = ReadInput.Strings(Day, file);
            return 0;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            return (PartA(file), PartB(file));
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
