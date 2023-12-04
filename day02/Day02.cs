using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day02
    {
        // Today: 
        public static Object PartA(string file)
        {
            var input = ReadInput.StringLists(Day, file);
            int sum = 0;
            var availableCubes = new Dictionary<string, int>() { ["red"] = 12, ["green"] = 13, ["blue"] = 14 };
            foreach (var sl in input)
            {
                int id = int.Parse(sl[1][..^1]);
                var s = string.Join(" ", sl.Skip(2));
                var rounds = s.Split("; ", StringSplitOptions.RemoveEmptyEntries);
                var maxCubes = new Dictionary<string, int>() { ["red"] = 0, ["green"] = 0, ["blue"] = 0 };
                foreach (var round in rounds)
                {
                    var cubes = round.Split(", ", StringSplitOptions.RemoveEmptyEntries);
                    foreach (var cube in cubes)
                    {
                        var nCubes = cube.Split(" ");
                        int n = int.Parse(nCubes[0]);
                        string color = nCubes[1];
                        if (maxCubes[color] < n)
                            maxCubes[color] = n;
                    }
                }
                if (maxCubes["red"] <= availableCubes["red"] && maxCubes["green"] <= availableCubes["green"] && maxCubes["blue"] <= availableCubes["blue"])
                    sum += id;
            }
            return sum;
        }
        public static Object PartB(string file)
        {
            var input = ReadInput.StringLists(Day, file);
            int sum = 0;
            foreach (var sl in input)
            {
                int id = int.Parse(sl[1][..^1]);
                var s = string.Join(" ", sl.Skip(2));
                var rounds = s.Split("; ", StringSplitOptions.RemoveEmptyEntries);
                var maxCubes = new Dictionary<string, int>() { ["red"] = 0, ["green"] = 0, ["blue"] = 0 };
                foreach (var round in rounds)
                {
                    var cubes = round.Split(", ", StringSplitOptions.RemoveEmptyEntries);
                    foreach (var cube in cubes)
                    {
                        var nCubes = cube.Split(" ");
                        int n = int.Parse(nCubes[0]);
                        string color = nCubes[1];
                        if (maxCubes[color] < n)
                            maxCubes[color] = n;
                    }
                }
                sum += maxCubes["red"] * maxCubes["green"] * maxCubes["blue"];
            }
            return sum;
        }
        static void Main() => Aoc.Execute(Day, PartA, PartB);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
