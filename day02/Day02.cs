using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day02
    {
        // Cube Conundrum: Parse number of cubes of each color
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.StringLists(Day, file);
            int ares = 0, bres = 0;
            const string R = "red", G = "green", B = "blue";
            var haveCubes = new Dictionary<string, int>() { [R] = 12, [G] = 13, [B] = 14 };
            foreach (var sl in input)
            {
                int id = int.Parse(sl[1][..^1]);
                var rounds = string.Join(" ", sl.Skip(2)).Split("; ", StringSplitOptions.RemoveEmptyEntries);
                var maxCubes = new Dictionary<string, int>() { [R] = 0, [G] = 0, [B] = 0 };
                foreach (var round in rounds)
                    foreach (var cube in round.Split(", ", StringSplitOptions.RemoveEmptyEntries))
                    {
                        var nCubes = cube.Split(" ");
                        int n = int.Parse(nCubes[0]);
                        string color = nCubes[1];
                        if (maxCubes[color] < n)
                            maxCubes[color] = n;
                    }
                if (maxCubes[R] <= haveCubes[R] && maxCubes[G] <= haveCubes[G] && maxCubes[B] <= haveCubes[B])
                    ares += id;
                bres += maxCubes[R] * maxCubes[G] * maxCubes[B];
            }
            return (ares, bres);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
