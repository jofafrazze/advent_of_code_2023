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
            string toStrip = "%&->,";
            foreach (var s0 in input)
            {
                bool isFlipFlop = s0[0] == '%';
                string s = new string(s0.ToCharArray().Where(c => !toStrip.Contains(c)).ToArray());
                var sl = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string name = sl[0];
                Node node = nodes.ContainsKey(name) ? nodes[name] : 
                    new Node(new Graph.Module(name, isFlipFlop, false, new Dictionary<string, bool>()));
                nodes[name] = node;
            }
            foreach (var s0 in input)
            {
                string s = new string(s0.ToCharArray().Where(c => !toStrip.Contains(c)).ToArray());
                var sl = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (var to in sl.Skip(1))
                {
                    if (!nodes.ContainsKey(to))
                    {
                        nodes[to] = new Node(new Graph.Module(to, true, false, new Dictionary<string, bool>()));
                    }
                }
            }
            foreach (var s0 in input)
            {
                string s = new string(s0.ToCharArray().Where(c => !toStrip.Contains(c)).ToArray());
                var sl = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string name = sl[0];
                foreach (var to in sl.Skip(1))
                {
                    nodes[to].t.inputsFrom[name] = false;
                    nodes[name].edges.Add(nodes[to]);
                }
            }
            Queue<Pulse> pulses = new();
            long nLow = 0, nHigh = 0, a = 0;
            bool findRx = nodes.ContainsKey("rx");
            string beforeRx = "";
            Dictionary<string, long> cyclesToLow = new();
            if (findRx)
            {
                beforeRx = nodes["rx"].t.inputsFrom.Keys.First();
                cyclesToLow = nodes[beforeRx].t.inputsFrom.Keys.ToDictionary(w => w, w => 0L);
            }
            for (int i = 0; i < 1000 || (findRx && cyclesToLow.Values.Where(w => w == 0).Any()); i++)
            {
                pulses.Enqueue(new Pulse("button", "broadcaster", false));
                while (pulses.TryDequeue(out Pulse p))
                {
                    if (p.dest == beforeRx && p.isHigh && cyclesToLow[p.from] == 0)
                    {
                        Console.WriteLine($"{p.from} send low after {i + 1} button presses");
                        cyclesToLow[p.from] = i + 1;
                    }
                    //string lh = p.isHigh ? "high" : "low";
                    //Console.WriteLine($"{p.from} -{lh}-> {p.dest}");
                    if (p.isHigh)
                        nHigh++;
                    else
                        nLow++;
                    if (p.dest == "broadcaster")
                    {
                        foreach (var n in nodes[p.dest].edges)
                            pulses.Enqueue(new Pulse(p.dest, n.t.name, p.isHigh));
                    }
                    else if (nodes[p.dest].t.isFlipFlop)
                    {
                        if (!p.isHigh)
                        {
                            nodes[p.dest].t.state = !nodes[p.dest].t.state;
                            foreach (var n in nodes[p.dest].edges)
                                pulses.Enqueue(new Pulse(p.dest, n.t.name, nodes[p.dest].t.state));
                        }
                    }
                    else // Conjunction module, can have several inputs
                    {
                        nodes[p.dest].t.inputsFrom[p.from] = p.isHigh;
                        bool allHigh = nodes[p.dest].t.inputsFrom.Values.All(w => w);
                        foreach (var n in nodes[p.dest].edges)
                            pulses.Enqueue(new Pulse(p.dest, n.t.name, !allHigh));
                    }
                }
                if (i == 999)
                    a = nLow * nHigh;
            }
            long b = cyclesToLow.Values.Aggregate(1L, (u, v) => Utils.LCM(u, v));
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
