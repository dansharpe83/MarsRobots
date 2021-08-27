# MarsRobots
Current application is a console application that takes in the grid and robot configuration, then will process the instructions for each robot and output the final position of each robot.

## Console Application
The console application will take a filename as a command line argument, it will then load and process the file, before instructiong the robots to move and outpoutting final positions.
### Input Format
The input file is a multi line input file, the first line is the size of the grid in width and height seperated by a space, then for each robot there are 2 lines, the first is the co ordinates and initial orientation of the robot, the 2nd line is the instructions for the robot.
Valid orientations are N,S,E,W
Valid Instructions are L to roate the robot left, R to rotate the robot right, F to move the robot forward
File example:
```
5 3   
1 1 E      
RFRFRFRF   
3 2 N  
FRRFLLFFRRFLL  
0 3 W  
LLFFFLFLFL  
```

### Output Format
THe output will be the final position of every robot, if the robot moved outside the bounds of the grid, it will be reported as lost
```
1 1 E   
3 3 N LOST   
2 3 S   
```
