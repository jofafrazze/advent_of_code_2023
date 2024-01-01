using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day25
    {
        // Snowverload: Split graph into two subgraphs by cutting 3 edges
        static int EdgesCut(HashSet<string> subset, List<(string, string)> edges) =>
            edges.Where(e => subset.Contains(e.Item1) != subset.Contains(e.Item2)).Count();
        static int SeparateNodes(HashSet<string> nodes, List<(string, string)> edges0)
        {
            List<HashSet<string>> subsets;
            var rnd = new Random();
            int iter = 0, cuts;
            do
            {
                iter++;
                subsets = nodes.Select(w => new HashSet<string>() { w }).ToList();
                var edges = edges0.ToList();
                while (subsets.Count > 2)
                {
                    int i = rnd.Next(edges.Count);
                    var subset1 = subsets.First(s => s.Contains(edges[i].Item1));
                    var subset2 = subsets.First(s => s.Contains(edges[i].Item2));
                    edges.RemoveAt(i);
                    if (subset1 == subset2) 
                        continue;
                    subsets.Remove(subset1);
                    subset2.UnionWith(subset1);
                }
                cuts = EdgesCut(subsets[0], edges0);
                //Console.WriteLine($"Iteration {iter++}, {cuts} cuts");
            } while (cuts != 3);
            Console.WriteLine($"Did {iter} iterations.");
            return subsets[0].Count * subsets[1].Count;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            var nodes = new HashSet<string>();
            var edges = new HashSet<(string, string)>();
            foreach (var line in input)
            {
                var v = line.Split(": ");
                var n0 = v[0];
                nodes.Add(n0);
                foreach (var n in v[1].Split(' '))
                {
                    nodes.Add(n);
                    if (!edges.Contains((n0, n)) && !edges.Contains((n, n0)))
                        edges.Add((n0, n));
                }
            }
            return (SeparateNodes(nodes, edges.ToList()), 0);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
