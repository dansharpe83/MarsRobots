using MarsRobots.Core;
using System;
using Xunit;

namespace MarsRobots.Tests
{
    public class GridTests
    {
        [Fact]
        public void GridSizeCorrect()
        {
            var grid = new Grid(Guid.NewGuid(), 3, 3);

            Assert.True(grid.IsCoordinateOutOfBounds(3, 4));
        }

        [Fact]
        public void XTooBigRaisesException()
        {
            Assert.Throws<ArgumentException>(() => new Grid(Guid.NewGuid(), 60, 10));
        }

        [Fact]
        public void YTooBigRaisesException()
        {
            Assert.Throws<ArgumentException>(() => new Grid(Guid.NewGuid(), 10, 60));
        }

        [Theory]
        [InlineData(-1,0)]
        [InlineData(0,-1)]
        [InlineData(3,5)]
        [InlineData(5,3)]
        public void OutOfBoundsCheckWorks(int x, int y)
        {
            var grid = new Grid(Guid.NewGuid(), 3, 3);
            Assert.True(grid.IsCoordinateOutOfBounds(x, y));
        }

        [Fact]
        public void SetScentWorks()
        {
            var grid = new Grid(Guid.NewGuid(), 3, 3);

            grid.AddScentToCoordinate(1, 1);

            Assert.True(grid.GridCoordinateHasScent(1, 1));
        }

        [Fact]
        public void SetScentReturnsFalse()
        {
            var grid = new Grid(Guid.NewGuid(), 3, 3);
            Assert.False(grid.GridCoordinateHasScent(1, 1));
        }
    }
}
