using AdventOfCode;
using System.Reflection;
using Pos = AdventOfCode.GenericPosition2D<int>;

namespace aoc
{
    public class Day03
    {
        // Today: 
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var m = Map.Build(ReadInput.Strings(Day, file));
            int asum = 0;
            var validPos = new Dictionary<Pos, bool>();
            foreach (Pos p in m.Positions())
                if (char.IsDigit(m[p]))
                {
                    validPos[p] = false;
                    foreach (Pos p2 in CoordsRC.Neighbours8(p))
                    {
                        if (m.HasPosition(p2) && m[p2] != '.' && !char.IsDigit(m[p2]))
                            validPos[p] = true;
                    }
                }
            var numStartPos = new Dictionary<Pos, Pos>();
            var posNumber = new Dictionary<Pos, int>();
            for (int row = 0; row < m.height; row++)
            {
                bool lastDigit, thisDigit = false, validNum = false;
                Pos startPos = new();
                string numStr = "";
                for (int col = 0; col < m.width; col++)
                {
                    Pos p = new(col, row);
                    lastDigit = thisDigit;
                    thisDigit = char.IsDigit(m[p]);
                    if (thisDigit)
                    {
                        if (!lastDigit)
                            startPos = p;
                        numStartPos[p] = startPos;
                        numStr += m[p];
                        validNum = validNum || validPos[p];
                    }
                    if ((!thisDigit && lastDigit) || col == m.width - 1)
                    {
                        if (validNum)
                        {
                            int num = int.Parse(numStr);
                            asum += num;
                            posNumber[startPos] = num;
                        }
                        numStr = "";
                        validNum = false;
                    }
                }
            }
            int bsum = 0;
            foreach(var p in m.Positions().Where(w => m[w] == '*'))
            {
                var neighbourNums = new List<Pos>();
                foreach (Pos p2 in CoordsRC.Neighbours8(p))
                {
                    if (m.HasPosition(p2) && char.IsDigit(m[p2]))
                        neighbourNums.Add(numStartPos[p2]);
                }
                neighbourNums = neighbourNums.ToHashSet().ToList();
                if (neighbourNums.Count == 2)
                    bsum += posNumber[neighbourNums[0]] * posNumber[neighbourNums[1]];
            }
            return (asum, bsum);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
