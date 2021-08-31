using MarsRobots.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarsRobots.Tests
{
    public class RobotTests
    {
        [Fact]
        public void InvalidRobotPositionThrowsException()
        {
            var grid = new Grid(Guid.NewGuid(), 3, 3);
            var robot = new Robot(Guid.NewGuid(), 5, 1, Orientation.N, "F");
            Assert.Throws<ArgumentException>(() => robot.Move(grid)) ;
        }

        [Theory]
        [InlineData(Orientation.N, Orientation.W)]
        [InlineData(Orientation.W, Orientation.S)]
        [InlineData(Orientation.S, Orientation.E)]
        [InlineData(Orientation.E, Orientation.N)]
        public void CanRotateLeft(Orientation startOrientation, Orientation expectedOrientation)
        {
            var grid = new Grid(Guid.NewGuid(), 3, 3);
            var robot = new Robot(Guid.NewGuid(), 0, 0, startOrientation, "L");
            robot.Move(grid);

            Assert.Equal(expectedOrientation, robot.CurrentPosition.Orientation);
        }

        [Theory]
        [InlineData(Orientation.N, Orientation.E)]
        [InlineData(Orientation.W, Orientation.N)]
        [InlineData(Orientation.S, Orientation.W)]
        [InlineData(Orientation.E, Orientation.S)]
        public void CanRotateRight(Orientation startOrientation, Orientation expectedOrientation)
        {
            var grid = new Grid(Guid.NewGuid(), 3, 3);
            var robot = new Robot(Guid.NewGuid(), 0, 0, startOrientation, "R");
            robot.Move(grid);

            Assert.Equal(expectedOrientation, robot.CurrentPosition.Orientation);
        }

        [Fact]
        public void RobotCanMoveNorth()
        {
            var grid = new Grid(Guid.NewGuid(), 3, 3);
            var robot = new Robot(Guid.NewGuid(), 1, 1, Orientation.N, "F");
            robot.Move(grid);

            Assert.Equal(2, robot.CurrentPosition.Y);
        }

        [Fact]
        public void RobotCanMoveSouth()
        {
            var grid = new Grid(Guid.NewGuid(), 3, 3);
            var robot = new Robot(Guid.NewGuid(), 1, 3, Orientation.S, "F");
            robot.Move(grid);

            Assert.Equal(2, robot.CurrentPosition.Y);
        }

        [Fact]
        public void RobotCanMoveEast()
        {
            var grid = new Grid(Guid.NewGuid(), 3, 3);
            var robot = new Robot(Guid.NewGuid(), 2, 1, Orientation.E, "F");
            robot.Move(grid);

            Assert.Equal(3, robot.CurrentPosition.X);
        }

        [Fact]
        public void RobotCanMoveWest()
        {
            var grid = new Grid(Guid.NewGuid(), 3, 3);
            var robot = new Robot(Guid.NewGuid(), 2, 1, Orientation.W, "F");
            robot.Move(grid);

            Assert.Equal(1, robot.CurrentPosition.X);
        }

        [Fact]
        public void RobotMarkedAsLost()
        {
            var grid = new Grid(Guid.NewGuid(), 3, 3);
            var robot = new Robot(Guid.NewGuid(), 0, 0, Orientation.W, "F");
            robot.Move(grid);

            Assert.True(robot.IsLost);
            Assert.True(grid.GridCoordinateHasScent(0, 0));
        }

        [Fact]
        public void RobotMarkedAsLost2()
        {
            var grid = new Grid(Guid.NewGuid(), 3, 3);
            var robot = new Robot(Guid.NewGuid(), 3, 3, Orientation.E, "F");
            robot.Move(grid);

            Assert.True(robot.IsLost);
            Assert.True(grid.GridCoordinateHasScent(3, 3));
        }

        [Fact]
        public void ScentedSquareStopsRobotBeingLost()
        {
            var grid = new Grid(Guid.NewGuid(), 3, 3);
            grid.AddScentToCoordinate(0, 0);
            var robot = new Robot(Guid.NewGuid(), 0, 0, Orientation.W, "F");
            robot.Move(grid);

            Assert.False(robot.IsLost);
        }

        [Fact]
        public void PositionMarkedCorrectly()
        {
            var grid = new Grid(Guid.NewGuid(), 3, 3);
            var robot = new Robot(Guid.NewGuid(), 0, 0, Orientation.E, "F");
            robot.Move(grid);

            Assert.Equal("1 0 E", robot.ToString());
        }

        [Fact]
        public void LostPositionOutputCorrectly()
        {
            var grid = new Grid(Guid.NewGuid(), 3, 3);
            var robot = new Robot(Guid.NewGuid(), 0, 0, Orientation.W, "F");
            robot.Move(grid);

            Assert.Equal("0 0 W LOST", robot.ToString());
        }
    }
}
