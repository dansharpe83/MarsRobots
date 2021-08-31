using MarsRobots.Core;
using System;
using System.IO;

namespace marsrobots
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var config = GetConfiguration(args[0]);

                //now we can process them
                var grid = new Grid(Guid.NewGuid(), config.GridWidth, config.GridHeight);
                foreach (var rc in config.RobotConfigurations)
                {
                    var robot = new Robot(Guid.NewGuid(), rc.X, rc.Y, rc.Orientation, rc.Instructions);

                    robot.Move(grid);

                    Console.WriteLine(robot.ToString());
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static InputConfiguration GetConfiguration(string filepath)
        {
            bool _isFirstLine = true;
            string line = null;
            var config = new InputConfiguration();
            //load the file
            using (var stream = File.OpenRead(filepath))
            using (var reader = new StreamReader(stream))
            {
                string robotCoords = null;
                while ((line = reader.ReadLine()) != null)
                {
                    if (_isFirstLine)
                    {
                        var parts = line.Split(" ");
                        if (!int.TryParse(parts[0], out var x))
                        {
                            throw new Exception($"could not parse {parts[0]} as a number for grid width");
                        }
                        config.GridWidth = x;
                        if (!int.TryParse(parts[1], out var y))
                        {
                            throw new Exception($"could not parse {parts[1]} as a number for grid height");
                        }

                        config.GridHeight = y;
                        _isFirstLine = false;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(robotCoords))
                            robotCoords = line;
                        else
                        {
                            //we can make a new robot config
                            var coordParts = robotCoords.Split(" ");
                            if (!int.TryParse(coordParts[0], out var x))
                                throw new Exception($"could not parse {coordParts[0]} as a number for robot X coordinate");
                            if (!int.TryParse(coordParts[1], out var y))
                                throw new Exception($"could not parse {coordParts[1]} as a number for robot Y Coordinate");
                            if (!Enum.TryParse<Orientation>(coordParts[2], out var orientation))
                                throw new Exception($"could not parse {coordParts[2]} as the orientation of the robot");
                            if (string.IsNullOrEmpty(line))
                                throw new Exception($"no instructions provided for the robot");

                            var robotConfig = new RobotConfiguration();
                            robotConfig.X = x;
                            robotConfig.Y = y;
                            robotConfig.Orientation = orientation;
                            robotConfig.Instructions = line;

                            config.RobotConfigurations.Add(robotConfig);

                            robotCoords = null;
                        }
                    }
                }
            }
            return config;
        }
    }
}
