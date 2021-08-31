using System;

namespace MarsRobots.Core
{
    public class Grid
    {
        private bool[,] _grid;
        public Grid(int maxWidth, int maxHeight)
        {
            if (maxWidth > 50)
                throw new ArgumentException($"{nameof(maxWidth)} must not be greater than 50");

            if (maxHeight > 50)
                throw new ArgumentException($"{nameof(maxHeight)} must not be greater than 50");

            _grid = new bool[maxWidth+1, maxHeight+1];
        }

        public bool GridCoordinateHasScent(int x, int y)
        {
            return _grid[x, y];
        }

        public void AddScentToCoordinate(int x, int y)
        {
            _grid[x, y] = true;
        }

        public bool IsCoordinateOutOfBounds(int x, int y)
        {
            return x < 0 || (x > _grid.GetLength(0) -1) || y < 0 || (y > _grid.GetLength(1) - 1);
        }
    }
}
