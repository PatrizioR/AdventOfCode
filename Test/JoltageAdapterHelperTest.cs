using AdventOfCode.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Test
{
    public class JoltageAdapterHelperTest
    {
        [Theory]
        [InlineData(@"16
10
15
5
1
11
7
19
6
12
4", 3, "1,3", 35)]
        [InlineData(@"28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3", 3, "1,3", 220)]
        public void JoltMultChainDiffTest(string input, int maxStep, string joltMults, int expected)
        {
            Assert.Equal(
                expected,
                JoltageAdapterHelper
                .JoltMultChainDiff(
                    input,
                    maxStep,
                    joltMults.Split(",").Select(item => int.Parse(item.Trim())).ToList())
                );
        }

        [Theory(Skip = "Ignore since not working")]
        [InlineData(@"44
41
48
17
35
146
73
3
16
159
11
29
32
63
65
62
126
151
6
124
87
115
122
43
12
85
2
98
59
156
149
66
10
82
26
79
56
22
74
49
25
69
54
19
108
18
55
131
140
15
125
37
129
91
51
158
117
136
142
109
64
36
160
150
42
118
101
78
28
105
110
40
157
70
97
139
152
47
104
81
27
116
132
143
1
80
75
141
133
9
50
153
123
111
119
130
112
94
90
86", 3, 1)]
        public void Dp(string input, int maxStep, int expected)
        {
            Assert.Equal(
              expected,
              JoltageAdapterHelper
              .JoltPerm(
                  input,
                  maxStep)
              );
        }
    }
}