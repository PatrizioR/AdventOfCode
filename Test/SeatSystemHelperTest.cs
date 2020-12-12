using AdventOfCode.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Test
{
    public class SeatSystemHelperTest
    {
        [Theory]
        [InlineData(@"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL", true, 4, 37)]
        [InlineData(@"..........", true, 4, 0)]
        [InlineData(@"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL", false, 5, 26)]
        public void CountSeatsOnNoMoreChangesTest(string input, bool limit, int minCount, int expected)
        {
            Assert.Equal(expected, SeatSystemHelper.CountSeatsOnNoMoreChanges(input, limit, minCount));
        }

        [Theory]
        [InlineData(@"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL", 0, 0, true, 0)]
        [InlineData(@".......#.
...#.....
.#.......
.........
..#L....#
....#....
.........
#........
...#.....", 4, 3, false, 8)]
        public void GetSeatsTest(string input, int line, int row, bool limit, int expected)
        {
            Assert.Equal(expected, SeatSystemHelper.GetAdjacents(SeatSystemHelper.GetSeats(input), line, row, limit).Count());
        }
    }
}