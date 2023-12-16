using Xunit;

namespace test
{
    public class Input
    {
        public static readonly string actual = "input.txt";
        public static readonly string example = "example.txt";
    }

    public class TestDay01
    {
        [Fact]
        public static void Example() => Assert.Equal(0, 0); // No example input works for both parts
        [Fact]
        public static void Test() => Assert.Equal((56506, 56017), aoc.Day01.DoPuzzle(Input.actual));
    }

    public class TestDay02
    {
        [Fact]
        public static void Example() => Assert.Equal((8, 2286), aoc.Day02.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((2476, 54911), aoc.Day02.DoPuzzle(Input.actual));
    }

    public class TestDay03
    {
        [Fact]
        public static void Example() => Assert.Equal((4361, 467835), aoc.Day03.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((544664, 84495585), aoc.Day03.DoPuzzle(Input.actual));
    }
    public class TestDay04
    {
        [Fact]
        public static void Example() => Assert.Equal((13, 30), aoc.Day04.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((23941, 5571760), aoc.Day04.DoPuzzle(Input.actual));
    }
    public class TestDay05
    {
        [Fact]
        public static void Example() => Assert.Equal((35, 46), aoc.Day05.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((910845529, 77435348), aoc.Day05.DoPuzzle(Input.actual));
    }
    public class TestDay06
    {
        [Fact]
        public static void Example() => Assert.Equal((288, 71503), aoc.Day06.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((861300, 28101347), aoc.Day06.DoPuzzle(Input.actual));
    }
    public class TestDay07
    {
        [Fact]
        public static void Example() => Assert.Equal((6440, 5905), aoc.Day07.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((248453531, 248781813), aoc.Day07.DoPuzzle(Input.actual));
    }
    public class TestDay08
    {
        [Fact]
        public static void Example() => Assert.Equal(0, 0); // No example input works for both parts
        [Fact]
        public static void Test() => Assert.Equal((15517L, 14935034899483L), aoc.Day08.DoPuzzle(Input.actual));
    }
    public class TestDay09
    {
        [Fact]
        public static void Example() => Assert.Equal((114, 2), aoc.Day09.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((1806615041, 1211), aoc.Day09.DoPuzzle(Input.actual));
    }
    public class TestDay10
    {
        [Fact]
        public static void Example() => Assert.Equal((80, 10), aoc.Day10.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((6786, 495), aoc.Day10.DoPuzzle(Input.actual));
    }
    public class TestDay11
    {
        [Fact]
        public static void Example() => Assert.Equal((374L, 82000210L), aoc.Day11.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((10033566L, 560822911938L), aoc.Day11.DoPuzzle(Input.actual));
    }
    public class TestDay12
    {
        [Fact]
        public static void Example() => Assert.Equal((21, 0), aoc.Day12.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((7350, 0), aoc.Day12.DoPuzzle(Input.actual));
    }
    public class TestDay13
    {
        [Fact]
        public static void Example() => Assert.Equal((405, 400), aoc.Day13.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((33735, 38063), aoc.Day13.DoPuzzle(Input.actual));
    }
    public class TestDay14
    {
        [Fact]
        public static void Example() => Assert.Equal((136, 64), aoc.Day14.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((110565, 89845), aoc.Day14.DoPuzzle(Input.actual));
    }
    public class TestDay15
    {
        [Fact]
        public static void Example() => Assert.Equal((1320, 145), aoc.Day15.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((510801, 212763), aoc.Day15.DoPuzzle(Input.actual));
    }
    public class TestDay16
    {
        [Fact]
        public static void Example() => Assert.Equal((46, 51), aoc.Day16.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((8901, 9064), aoc.Day16.DoPuzzle(Input.actual));
    }
    public class TestDay17
    {
        [Fact]
        public static void Example() => Assert.Equal((0, 0), aoc.Day17.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((0, 0), aoc.Day17.DoPuzzle(Input.actual));
    }
    public class TestDay18
    {
        [Fact]
        public static void Example() => Assert.Equal((0, 0), aoc.Day18.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((0, 0), aoc.Day18.DoPuzzle(Input.actual));
    }
    public class TestDay19
    {
        [Fact]
        public static void Example() => Assert.Equal((0, 0), aoc.Day19.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((0, 0), aoc.Day19.DoPuzzle(Input.actual));
    }
    public class TestDay20
    {
        [Fact]
        public static void Example() => Assert.Equal((0, 0), aoc.Day20.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((0, 0), aoc.Day20.DoPuzzle(Input.actual));
    }
    public class TestDay21
    {
        [Fact]
        public static void Example() => Assert.Equal((0, 0), aoc.Day21.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((0, 0), aoc.Day21.DoPuzzle(Input.actual));
    }
    public class TestDay22
    {
        [Fact]
        public static void Example() => Assert.Equal((0, 0), aoc.Day22.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((0, 0), aoc.Day22.DoPuzzle(Input.actual));
    }
    public class TestDay23
    {
        [Fact]
        public static void Example() => Assert.Equal((0, 0), aoc.Day23.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((0, 0), aoc.Day23.DoPuzzle(Input.actual));
    }
    public class TestDay24
    {
        [Fact]
        public static void Example() => Assert.Equal((0, 0), aoc.Day24.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((0, 0), aoc.Day24.DoPuzzle(Input.actual));
    }
    public class TestDay25
    {
        [Fact]
        public static void Example() => Assert.Equal((0, 0), aoc.Day25.DoPuzzle(Input.example));
        [Fact]
        public static void Test() => Assert.Equal((0, 0), aoc.Day25.DoPuzzle(Input.actual));
    }
}
