using AdventOfCode;
using System.Reflection;
using Node = AdventOfCode.Graph.Node<AdventOfCode.Graph.Module>;

namespace aoc
{
    public class Day20
    {
        // Pulse Propagation: Propagate signals in graph with different modules
        record struct Pulse(string from, string dest, bool isHigh);
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            Dictionary<string, Node> nodes = new();
            List<(string name, List<string> connections)> modules = new();
            string toStrip = "%&->,";
            foreach (var line in input)
            {
                bool isFlipFlop = line[0] == '%';
                var sl = string.Concat(line.ToCharArray().Where(c => !toStrip.Contains(c))).Split(' ').ToList();
                modules.Add((sl[0], sl.Skip(2).ToList()));
                nodes[sl[0]] = new Node(new Graph.Module(sl[0], isFlipFlop, false, new Dictionary<string, bool>()));
            }
            foreach (var to in modules.Select(w => w.connections).SelectMany(w => w))
                if (!nodes.ContainsKey(to))
                    nodes[to] = new Node(new Graph.Module(to, true, false, new Dictionary<string, bool>()));
            foreach (var (name, connections) in modules)
                foreach (var to in connections)
                {
                    nodes[to].t.inputsFrom[name] = false;
                    nodes[name].edges.Add(nodes[to]);
                }
            Queue<Pulse> pulses = new();
            long nLow = 0, nHigh = 0, a = 0;
            bool findRx = nodes.ContainsKey("rx");
            string beforeRx = findRx ? nodes["rx"].t.inputsFrom.Keys.First() : "";
            Dictionary<string, long> cyclesToLow = findRx ? 
                nodes[beforeRx].t.inputsFrom.Keys.ToDictionary(w => w, w => 0L) : new();
            for (int presses = 1; a == 0 || (findRx && cyclesToLow.Values.Where(w => w == 0).Any()); presses++)
            {
                pulses.Enqueue(new Pulse("button", "broadcaster", false));
                while (pulses.TryDequeue(out Pulse p))
                {
                    if (p.dest == beforeRx && p.isHigh && cyclesToLow[p.from] == 0)
                        cyclesToLow[p.from] = presses;
                    if (p.isHigh)
                        nHigh++;
                    else
                        nLow++;
                    if (p.dest == "broadcaster")
                    {
                        foreach (var n in nodes[p.dest].edges)
                            pulses.Enqueue(new Pulse(p.dest, n.t.name, p.isHigh));
                    }
                    else if (nodes[p.dest].t.isFlipFlop && !p.isHigh)
                    {
                        nodes[p.dest].t.state = !nodes[p.dest].t.state;
                        foreach (var n in nodes[p.dest].edges)
                            pulses.Enqueue(new Pulse(p.dest, n.t.name, nodes[p.dest].t.state));
                    }
                    else if (!nodes[p.dest].t.isFlipFlop) // Conjunction module
                    {
                        nodes[p.dest].t.inputsFrom[p.from] = p.isHigh;
                        bool allHigh = nodes[p.dest].t.inputsFrom.Values.All(w => w);
                        foreach (var n in nodes[p.dest].edges)
                            pulses.Enqueue(new Pulse(p.dest, n.t.name, !allHigh));
                    }
                }
                if (presses == 1000)
                    a = nLow * nHigh;
            }
            long b = cyclesToLow.Values.Aggregate(1L, (u, v) => Utils.LCM(u, v));
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
