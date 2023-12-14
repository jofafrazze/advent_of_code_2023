using AdventOfCode;
using System.Reflection;
using System.Text;

namespace aoc
{
    public class Day13
    {
        // Today: 
        static int ReflectionAboveRow(Map m, int previousRow = 0)
        {
            for (int y = 0; y < m.height - 1; y++)
            {
                bool allMatch = true;
                for (int offs = 0; y - offs >= 0 && y + 1 + offs < m.height && allMatch; offs++)
                {
                    int y1 = y - offs;
                    int y2 = y + 1 + offs;
                    for (int x = 0; x < m.width; x++)
                        if (m.data[x, y1] != m.data[x, y2])
                            allMatch = false;
                }
                if (allMatch && y + 1 != previousRow)
                    return y + 1;
            }
            return 0;
        }
        static int ReflectionAboveRowPermute(Map m, int previousRow)
        {
            for (int y = 0; y < m.height; y++)
            {
                for (int x = 0; x < m.width; x++)
                {
                    Map m2 = new(m);
                    m2.data[x, y] = m2.data[x, y] == '.' ? '#' : '.';
                    int row = ReflectionAboveRow(m2, previousRow);
                    if (row > 0)
                        return row;
                }
            }
            return 0;
        }
        public static (Object a, Object b) DoPuzzle(string file)
        {
            var input = ReadInput.StringGroups(Day, file);
            List<int> verticalSymmetry = new();
            List<int> horizontalSymmetry = new();
            int asum = 0;
            foreach (var sl in input)
            {
                Map m = Map.Build(sl);
                int a = ReflectionAboveRow(m);
                asum += a * 100;
                verticalSymmetry.Add(a);
            }
            foreach (var sl in input)
            {
                Map m = Map.Build(sl);
                Map trans = Map.Transpose(m);
                int a = ReflectionAboveRow(trans);
                asum += a;
                horizontalSymmetry.Add(a);
            }


            int bsum = 0;
            for (int i = 0; i < input.Count; i++)
            {
                Map m = Map.Build(input[i]);
                int line = ReflectionAboveRowPermute(m, verticalSymmetry[i]);
                bsum += line * 100;
            }
            for (int i = 0; i < input.Count; i++)
            {
                Map m = Map.Build(input[i]);
                Map trans = Map.Transpose(m);
                trans.Print();
                int line = ReflectionAboveRowPermute(trans, horizontalSymmetry[i]);
                bsum += line;
            }
            return (asum, bsum);
        }
        static void Main() => Aoc.Execute(Day, DoPuzzle, true);
        static string Day => Aoc.Day(MethodBase.GetCurrentMethod()!);
    }
}
