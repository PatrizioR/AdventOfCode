using AdventOfCode.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Test
{
    public static class ShipMovementTest
    {
        [Theory]
        [InlineData(@"F10
N3
F7
R90
F11", "East", 25)]
        public static void Move(string input, string startDirection, int expected)
        {
            Assert.Equal(expected, ShipMovement.Move(input, startDirection));
        }

        [Fact]
        public static void RightTest()
        {
            Ship s = new Ship(Direction.East);
            s.Right();
            Assert.Equal(Direction.South, s.Direction);
        }

        [Fact]
        public static void FourRightTest()
        {
            Ship s = new Ship(Direction.East);
            s.Right();
            s.Right();
            s.Right();
            s.Right();
            Assert.Equal(Direction.East, s.Direction);
        }

        [Fact]
        public static void LeftTest()
        {
            Ship s = new Ship(Direction.East);
            s.Left();
            Assert.Equal(Direction.North, s.Direction);
        }

        [Fact]
        public static void FourLeftTest()
        {
            Ship s = new Ship(Direction.East);
            s.Left();
            s.Left();
            s.Left();
            s.Left();
            Assert.Equal(Direction.East, s.Direction);
        }

        [Fact]
        public static void ForwardTest()
        {
            Ship s = new Ship(Direction.East);
            s.Forward(10);
            Assert.Equal(10, s.PosSum);
        }

        [Fact]
        public static void RightForwardTest()
        {
            Ship s = new Ship(Direction.East);
            s.Forward(10);
            s.Right();
            s.Forward(10);
            Assert.Equal(20, s.PosSum);
        }
    }
}