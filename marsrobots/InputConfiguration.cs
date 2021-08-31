using MarsRobots.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marsrobots
{
    class InputConfiguration
    {
        public int GridWidth { get; set; }
        public int GridHeight { get; set; }

        public List<RobotConfiguration> RobotConfigurations { get; set; } = new List<RobotConfiguration>();

    }

    class RobotConfiguration
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Orientation Orientation { get; set; }
        public string Instructions { get; set; }
    }
}
