using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRobots.Core
{
    public class Robot
    {
        private readonly Grid _grid;

        private List<Position> _history = new List<Position>();
        private Position _currentPosition;
        private bool _isLost;

        public Robot(Position initialPosition, Grid grid)
        {
            if (grid is null)
            {
                throw new ArgumentNullException(nameof(grid));
            }

            //check that the initial position is in bounds of our grid
            if (grid.IsCoordinateOutOfBounds(initialPosition.X, initialPosition.Y))
                throw new ArgumentException("Initial position is not with in the grid");
            

            _currentPosition = initialPosition ?? throw new ArgumentNullException(nameof(initialPosition));
            _history.Add(initialPosition);
            _grid = grid;
        }

        public Position CurrentPosition => _currentPosition;
        public bool IsLost => _isLost;

        public void Instruct(string instructions)
        {
            if (instructions.Length > 100)
                throw new ArgumentException($"{nameof(instructions)} must not be greater than 100 characters");

            foreach(var instruction in instructions)
            {
                //do we break or skip if its lost????
                switch(instruction)
                {
                    case 'L': RotateLeft(); break;
                    case 'R': RotateRight(); break;
                    case 'F': MoveForward(); break;
                }
            }
        }

        private Position RotateLeft()
        {
            var newOrientation = _currentPosition.Orientation switch
            {
                Orientation.N => Orientation.W,
                Orientation.W => Orientation.S,
                Orientation.S => Orientation.E,
                Orientation.E => Orientation.N
            };

            var newPosition = new Position(_currentPosition.X, _currentPosition.Y, newOrientation);
            AddNewPosition(newPosition);
            return newPosition;
        }
        
        private Position RotateRight()
        {
            var newOrientation = _currentPosition.Orientation switch
            {
                Orientation.N => Orientation.E,
                Orientation.E => Orientation.S,
                Orientation.S => Orientation.W,
                Orientation.W => Orientation.N
            };

            var position = new Position(_currentPosition.X, _currentPosition.Y, newOrientation);
            AddNewPosition(position);
            return position;
        }

        private Position MoveForward()
        {
            //work out the next co-ordinates based on the orientation
            var x = _currentPosition.X;
            var y = _currentPosition.Y;
            switch(_currentPosition.Orientation)
            {
                case Orientation.N: y++; break;
                case Orientation.S: y--; break;
                case Orientation.E: x++; break;
                case Orientation.W: x--; break;
            }

            //check if it will take the robot out of bounds
            if(_grid.IsCoordinateOutOfBounds(x,y))
            {
                if (_grid.GridCoordinateHasScent(_currentPosition.X, _currentPosition.Y))
                    return _currentPosition;

                _grid.AddScentToCoordinate(_currentPosition.X, _currentPosition.Y);
                _isLost = true;

                return _currentPosition;
            }

            var newPosition = new Position(x, y, _currentPosition.Orientation);
            AddNewPosition(newPosition);

            return newPosition;
        }

        private void AddNewPosition(Position position)
        {
            _history.Add(position);
            _currentPosition = position;
        }

        public override string ToString()
        {
            return $"{_currentPosition.X} {_currentPosition.Y} {_currentPosition.Orientation.ToString()}{(_isLost ? " LOST" : "")}";
        }
    }

    public record Position(int X, int Y, Orientation Orientation)
    {

    }

    public enum Orientation
    {
        N,
        E,
        W,
        S
    }
}
