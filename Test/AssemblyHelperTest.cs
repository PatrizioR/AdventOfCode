using AdventOfCode.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Test
{
    public class AssemblyHelperTest
    {
        [Theory]
        [InlineData(@"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6", 5)]
        public void CountAccBeforeRepeatTest(string input, int expected)
        {
            Assert.Equal(expected, AssemblyHelper.CountAccBeforeRepeat(input));
        }

        [Theory]
        [InlineData(@"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6", 8)]
        public void CountAccAfterFixTest(string input, int expected)
        {
            Assert.Equal(expected, AssemblyHelper.CountAccAfterFix(input));
        }
    }
}
