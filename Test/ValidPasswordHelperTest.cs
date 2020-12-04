using AdventOfCode.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AdventOfCode.Test
{
    public class ValidPasswordHelperTest
    {
        [Theory]
        [InlineData("1-3 a: abcde\n1-3 b: cdefg\n2-9 c: ccccccccc", 2)]
        [InlineData("2-3 a: aacde\n1-3 c: cdefg\n2-6 c: cccccc", 3)]
        [InlineData("2-3 a: acade\n2-3 c: cdcefg\n2-6 c: cacacacacaca", 3)]
        public void CountValidPasswords(string input, int expected)
        {
            Assert.Equal(expected, ValidPasswordHelper.CountValidPasswords(input));
        }

        [Theory]
        [InlineData("1-3 a: abcde\n1-3 b: cdefg\n2-9 c: ccccccccc", 1)]
        [InlineData("2-3 a: aacde\n1-3 c: cdefg\n2-6 c: cccccc", 2)]
        [InlineData("2-3 a: baadaaae\n2-3 c: cccecfg\n2-6 c: cacacacacaca", 0)]
        public void CountValidPasswordsSecond(string input, int expected)
        {
            Assert.Equal(expected, ValidPasswordHelper.CountValidPasswordsSecond(input));
        }
    }
}
