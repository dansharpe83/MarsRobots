using System;

namespace MarsRobots.Core
{
    public class Grid
    {
        private Guid _id;
        private bool[,] _grid;
        public Grid(Guid id, int width, int height)
        {
            if (width > 50)
                throw new ArgumentException($"{nameof(width)} must not be greater than 50");

            if (height > 50)
                throw new ArgumentException($"{nameof(height)} must not be greater than 50");

            _id = id;
            _grid = new bool[width+1, height+1];
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
