using AdventOfCode;
using System.Reflection;

namespace aoc
{
    public class Day20
    {
        // Pulse Propagation: Propagate signals in graph with different modules
        record class Node(string name, bool isFlipFlop, Dictionary<string, bool> inputs, HashSet<string> edges);
        record struct Pulse(string from, string dest, bool isHigh);
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            List<(string name, List<string> connections)> modules = new();
            Dictionary<string, Node> nodes = new();
            string toStrip = "%&->,";
            foreach (var line in input)
            {
                bool isFlipFlop = line[0] == '%';
                var sl = string.Concat(line.ToCharArray().Where(c => !toStrip.Contains(c))).Split(' ').ToList();
                modules.Add((sl[0], sl.Skip(2).ToList()));
                nodes[sl[0]] = new Node(sl[0], isFlipFlop, new(), new());
            }
            foreach (var to in modules.Select(w => w.connections).SelectMany(w => w))
                if (!nodes.ContainsKey(to))
                    nodes[to] = new Node(to, true, new(), new());
            foreach (var (name, connections) in modules)
                foreach (var to in connections)
                {
                    nodes[to].inputs[name] = false;
                    nodes[name].edges.Add(to);
                }
            var states = nodes.ToDictionary(w => w.Key, w => false);
            Queue<Pulse> pulses = new();
            long nLow = 0, nHigh = 0, a = 0;
            bool findRx = nodes.ContainsKey("rx");
            string beforeRx = findRx ? nodes["rx"].inputs.Keys.First() : "";
            Dictionary<string, long> cyclesToLow = findRx ? 
                nodes[beforeRx].inputs.Keys.ToDictionary(w => w, w => 0L) : new();
            for (int presses = 1; a == 0 || (findRx && cyclesToLow.Values.Where(w => w == 0).Any()); presses++)
            {
                pulses.Enqueue(new Pulse("button", "broadcaster", false));
                while (pulses.TryDequeue(out Pulse pulse))
                {
                    if (pulse.dest == beforeRx && pulse.isHigh && cyclesToLow[pulse.from] == 0)
                        cyclesToLow[pulse.from] = presses;
                    if (pulse.isHigh) nHigh++; else nLow++;
                    if (pulse.dest == "broadcaster")
                    {
                        foreach (var edge in nodes[pulse.dest].edges)
                            pulses.Enqueue(new Pulse(pulse.dest, edge, pulse.isHigh));
                    }
                    else if (nodes[pulse.dest].isFlipFlop && !pulse.isHigh)
                    {
                        states[pulse.dest] = !states[pulse.dest];
                        foreach (var edge in nodes[pulse.dest].edges)
                            pulses.Enqueue(new Pulse(pulse.dest, edge, states[pulse.dest]));
                    }
                    else if (!nodes[pulse.dest].isFlipFlop) // Conjunction module
                    {
                        nodes[pulse.dest].inputs[pulse.from] = pulse.isHigh;
                        foreach (var edge in nodes[pulse.dest].edges)
                            pulses.Enqueue(new Pulse(pulse.dest, edge, !nodes[pulse.dest].inputs.Values.All(w => w)));
                    }
                }
                if (presses == 1000)
                    a = nLow * nHigh;
            }
            return (a, cyclesToLow.Values.Aggregate(1L, (u, v) => Utils.LCM(u, v)));
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
