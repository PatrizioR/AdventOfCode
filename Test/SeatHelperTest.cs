using AdventOfCode.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Test
{
    public class SeatHelperTest
    {
        [Theory]
        [InlineData("BFFFBBFRRR", 567)]
        [InlineData("FFFBBBFRRR", 119)]
        [InlineData(@"BFFFBBFRRR
FFFBBBFRRR
BBFFBBFRLL", 820)]
        public void GetMaxSeatIdTest(string input, int expected)
        {
            Assert.Equal(expected, SeatHelper.GetMaxSeatId(input));
        }
    }
}