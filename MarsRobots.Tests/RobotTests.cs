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
        public void InvalidRobotThrowsException()
        {
            var grid = new Grid(3, 3);
            var position = new Position(5, 1, Orientation.N);
            Assert.Throws<ArgumentException>(() => new Robot(position, grid));
        }

        [Theory]
        [InlineData(Orientation.N, Orientation.W)]
        [InlineData(Orientation.W, Orientation.S)]
        [InlineData(Orientation.S, Orientation.E)]
        [InlineData(Orientation.E, Orientation.N)]
        public void CanRotateLeft(Orientation startOrientation, Orientation expectedOrientation)
        {
            var grid = new Grid(3, 3);
            var position = new Position(1, 1, startOrientation);
            var robot = new Robot(position, grid);
            robot.Instruct("L");

            Assert.Equal(expectedOrientation, robot.CurrentPosition.Orientation);
        }

        [Theory]
        [InlineData(Orientation.N, Orientation.E)]
        [InlineData(Orientation.W, Orientation.N)]
        [InlineData(Orientation.S, Orientation.W)]
        [InlineData(Orientation.E, Orientation.S)]
        public void CanRotateRight(Orientation startOrientation, Orientation expectedOrientation)
        {
            var grid = new Grid(3, 3);
            var position = new Position(1, 1, startOrientation);
            var robot = new Robot(position, grid);
            robot.Instruct("R");

            Assert.Equal(expectedOrientation, robot.CurrentPosition.Orientation);
        }

        [Fact]
        public void RobotCanMoveNorth()
        {
            var grid = new Grid(3, 3);
            var position = new Position(1, 1, Orientation.N);
            var robot = new Robot(position, grid);
            robot.Instruct("F");

            Assert.Equal(2, robot.CurrentPosition.Y);
        }

        [Fact]
        public void RobotCanMoveSouth()
        {
            var grid = new Grid(3, 3);
            var position = new Position(1, 3, Orientation.S);
            var robot = new Robot(position, grid);
            robot.Instruct("F");

            Assert.Equal(2, robot.CurrentPosition.Y);
        }

        [Fact]
        public void RobotCanMoveEast()
        {
            var grid = new Grid(3, 3);
            var position = new Position(2, 1, Orientation.E);
            var robot = new Robot(position, grid);
            robot.Instruct("F");

            Assert.Equal(3, robot.CurrentPosition.X);
        }

        [Fact]
        public void RobotCanMoveWest()
        {
            var grid = new Grid(3, 3);
            var position = new Position(2, 1, Orientation.W);
            var robot = new Robot(position, grid);
            robot.Instruct("F");

            Assert.Equal(1, robot.CurrentPosition.X);
        }

        [Fact]
        public void RobotMarkedAsLost()
        {
            var grid = new Grid(3, 3);
            var position = new Position(0, 0, Orientation.W);
            var robot = new Robot(position, grid);
            robot.Instruct("F");

            Assert.True(robot.IsLost);
            Assert.True(grid.GridCoordinateHasScent(0, 0));
        }

        [Fact]
        public void RobotMarkedAsLost2()
        {
            var grid = new Grid(3, 3);
            var position = new Position(3, 3, Orientation.E);
            var robot = new Robot(position, grid);
            robot.Instruct("F");

            Assert.True(robot.IsLost);
            Assert.True(grid.GridCoordinateHasScent(3, 3));
        }

        [Fact]
        public void ScentedSquareStopsRobotBeingLost()
        {
            var grid = new Grid(3, 3);
            grid.AddScentToCoordinate(0, 0);
            var position = new Position(0, 0, Orientation.W);
            var robot = new Robot(position, grid);
            robot.Instruct("F");

            Assert.False(robot.IsLost);
        }

        [Fact]
        public void PositionMarkedCorrectly()
        {
            var grid = new Grid(3, 3);
            var position = new Position(0, 0, Orientation.E);
            var robot = new Robot(position, grid);
            robot.Instruct("F");

            Assert.Equal("1 0 E", robot.ToString());
        }

        [Fact]
        public void LostPositionOutputCorrectly()
        {
            var grid = new Grid(3, 3);
            var position = new Position(0, 0, Orientation.W);
            var robot = new Robot(position, grid);
            robot.Instruct("F");

            Assert.Equal("0 0 W LOST", robot.ToString());
        }
    }
}
