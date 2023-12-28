using AdventOfCode;
using System.Numerics;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition3D<long>;

namespace aoc
{
    public class Day24
    {
        // Never Tell Me The Odds: Find where lines are crossing in 2D
        record struct LineGF(BigInteger a, BigInteger b, BigInteger c);
        record struct Hail(Pos p, Pos v, LineGF lgf);
        // Returns a line of general form ax + by + c = 0
        static LineGF LineGeneralForm(Hail hail)
        {
            Pos p1 = new(hail.p);
            Pos p2 = hail.p + hail.v;
            BigInteger a = p1.y - p2.y;
            BigInteger b = p2.x - p1.x;
            BigInteger c = (BigInteger)p1.x * p2.y - (BigInteger)p2.x * p1.y;
            return new(a, b, c);
        }
        static bool WillReach(Hail h, BigInteger targetX, BigInteger targetY) =>
            ((h.p.x <= targetX && h.v.x >= 0) || (h.p.x >= targetX && h.v.x <= 0)) &&
            ((h.p.y <= targetY && h.v.y >= 0) || (h.p.y >= targetY && h.v.y <= 0));
        static Pos? GetHailRouteCrossingXY(Hail h1, Hail h2)
        {
            var (a1, b1, c1) = h1.lgf;
            var (a2, b2, c2) = h2.lgf;
            var denom = a1 * b2 - a2 * b1;
            if (denom == 0)
                return null;
            var x0 = (b1 * c2 - b2 * c1) / denom;
            var y0 = (c1 * a2 - c2 * a1) / denom;
            long lx0 = unchecked((long)(ulong)(x0 & ulong.MaxValue));
            long ly0 = unchecked((long)(ulong)(y0 & ulong.MaxValue));
            return WillReach(h1, x0, y0) && WillReach(h2, x0, y0) ? new(lx0, ly0, 0) : null;
        }
        static bool HailsCrossingInTestArea(Hail h1, Hail h2, bool example)
        {
            Pos? crossing = GetHailRouteCrossingXY(h1, h2);
            if (crossing == null)
                return false;
            long x0 = crossing.Value.x;
            long y0 = crossing.Value.y;
            var limLo = example ? 7 : 200000000000000L;
            var limHi = example ? 27 : 400000000000000L;
            bool xOk = x0 >= limLo && x0 <= limHi;
            bool yOk = y0 >= limLo && y0 <= limHi;
            return xOk && yOk;
        }
        static Pos GetHailPos3DFrom2D(Hail hail, Pos target)
        {
            long t = hail.v.x == 0 ? (target.y - hail.p.y) / hail.v.y : (target.x - hail.p.x) / hail.v.x;
            return new(target.x, target.y, hail.p.z + t * hail.v.z);
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.Strings(Day, file);
            List<Hail> lines = new();
            foreach (var s in input)
            {
                var i = Extract.Longs(s).ToList();
                var h = new Hail(new Pos(i[0], i[1], i[2]), new Pos(i[3], i[4], i[5]), new());
                h.lgf = LineGeneralForm(h);
                lines.Add(h);
            }
            var pairs = Algorithms.GetCombinations(lines, 2).Where(w => w.Count == 2).ToList();
            int a = pairs.Where(p => HailsCrossingInTestArea(p[0], p[1], file == "example.txt")).Count();
            int vMax = 300;
            Pos? collisionPos = null;
            Pos rockVelocity = new();
            for (int xRock = -vMax; xRock <= vMax && collisionPos == null; xRock++)
            {
                for (int yRock = -vMax; yRock <= vMax && collisionPos == null; yRock++)
                {
                    Pos? pAllCollide = null;
                    bool searchFailed = false;
                    for (int i = 0; i < lines.Count - 1 && !searchFailed; i++)
                    {
                        bool linesAreParallell = true;
                        Pos? pCollision = null;
                        int offs = 0;
                        while (linesAreParallell)
                        {
                            bool walkDown = offs <= i;
                            int h1idx = walkDown ? i - offs : i + 1 + (offs - i);
                            if (h1idx >= lines.Count)
                                break;
                            var h1 = lines[h1idx];
                            var h2 = lines[i + 1];
                            h1.v = new Pos(h1.v.x - xRock, h1.v.y - yRock, h1.v.z);
                            h2.v = new Pos(h2.v.x - xRock, h2.v.y - yRock, h2.v.z);
                            h1.lgf = LineGeneralForm(h1);
                            h2.lgf = LineGeneralForm(h2);
                            pCollision = GetHailRouteCrossingXY(h1, h2);
                            if (pCollision != null)
                                linesAreParallell = false;
                            offs++;
                        }
                        if (pCollision == null)
                            searchFailed = true;
                        else if (pAllCollide == null)
                            pAllCollide = pCollision;
                        else if (pCollision != pAllCollide)
                            searchFailed = true;
                    }
                    if (!searchFailed)
                    {
                        collisionPos = pAllCollide;
                        rockVelocity = new(xRock, yRock, 0);
                    }
                }
            }
            for (int zRock = -vMax; zRock <= vMax; zRock++)
            {
                Pos? pAllCollide = null;
                bool searchFailed = false;
                for (int i = 0; i < lines.Count && !searchFailed; i++)
                {
                    Pos? pos = null;
                    var hail = lines[i];
                    hail.v = new Pos(hail.v.x - rockVelocity.x, hail.v.y - rockVelocity.y, hail.v.z - zRock);
                    pos = GetHailPos3DFrom2D(hail, collisionPos!.Value);
                    if (pAllCollide == null)
                        pAllCollide = pos;
                    else if (pAllCollide != pos)
                        searchFailed = true;
                }
                if (!searchFailed)
                {
                    collisionPos = pAllCollide;
                    rockVelocity = new(rockVelocity.x, rockVelocity.y, zRock);
                    Console.WriteLine($"Rock velocity needed is {rockVelocity}");
                }
            }
            long b = collisionPos!.Value.x + collisionPos.Value.y + collisionPos.Value.z;
            return (a, b);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
