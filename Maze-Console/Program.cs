using SixLabors.ImageSharp;
using System.Diagnostics;

namespace Maze_Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ////BinaryTree demo
            //Grid grid = new Grid(20, 20);
            //BinaryTree.On(grid);
            //Console.WriteLine(grid.ToString());
            //var img1 = grid.ToImg(30);
            //img1.SaveAsPng("C:\\Users\\netinova\\Desktop\\Maze\\Maze-Console\\generated-mazes\\maze1.png");

            ////Sidewinder demo
            //Grid grid2 = new Grid(10, 10);
            //Sidewinder.On(grid2);
            //Console.WriteLine(grid2.ToString());
            //var img = grid2.ToImg(40, 60);
            //img.SaveAsPng("C:\\Users\\netinova\\Desktop\\Maze\\Maze-Console\\generated-mazes\\maze2.png");

            //Process.Start(new ProcessStartInfo("C:\\Users\\netinova\\Desktop\\Maze\\Maze-Console\\generated-mazes\\maze1.png") { UseShellExecute = true });
            //Process.Start(new ProcessStartInfo("C:\\Users\\netinova\\Desktop\\Maze\\Maze-Console\\generated-mazes\\maze2.png") { UseShellExecute = true });



            //DistanceGrid distanceGrid = new DistanceGrid(7, 7);
            //Sidewinder.On(distanceGrid);

            //Cell start = distanceGrid[6 , 0];
            //Distances distancesMap = start.Distances();
            //distanceGrid.distanceMap = distancesMap.ToDictionary();

            ColoredGrid coloredGrid = new ColoredGrid(40, 40);
            Wilsons.On(coloredGrid);
            Cell start = coloredGrid[coloredGrid.rows/2 , coloredGrid.columns/2];
            Distances distanceMap = start.Distances();
            coloredGrid.distanceMap = distanceMap;
            //coloredGrid.distanceMap = start.Distances();
            var img = coloredGrid.ToImg(30,60);
            
            img.SaveAsPng("C:\\Users\\netinova\\Desktop\\Maze\\Maze-Console\\generated-mazes\\maze3.png");
            Process.Start(new ProcessStartInfo("C:\\Users\\netinova\\Desktop\\Maze\\Maze-Console\\generated-mazes\\maze3.png") { UseShellExecute = true });

            //test
            //distancesMap = start.Distances();
            //var (newStart, maxDistance) = distancesMap.Max();
            //Distances newDistancesMap = newStart.Distances();

            //distanceGrid.distanceMap = newDistancesMap.ToDictionary();
            //Console.WriteLine(distanceGrid.ToString());

            //var (goal, distance) = newDistancesMap.Max();

            //distanceGrid.distanceMap = newDistancesMap.PathTo(goal).ToDictionary();
            //Console.WriteLine(distanceGrid.ToString());

        }
    }
}