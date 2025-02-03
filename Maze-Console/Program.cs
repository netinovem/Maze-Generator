using SixLabors.ImageSharp;
using System.Diagnostics;

namespace Maze_Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ColoredGrid coloredGrid = new ColoredGrid(40, 40);
            //coloredGrid[0, 0].east.west = null;
            //coloredGrid[0, 0].south.north = null;
            RecursiveBacktracker.On(coloredGrid);
            Cell start = coloredGrid[coloredGrid.rows / 2, coloredGrid.columns / 2];
            Distances distanceMap = start.Distances();
            coloredGrid.distanceMap = distanceMap;
            var img = coloredGrid.ToImg(60, 60);

            img.SaveAsPng("C:\\Users\\netinova\\Desktop\\Maze\\Maze-Console\\generated-mazes\\maze3.png");
            Process.Start(new ProcessStartInfo("C:\\Users\\netinova\\Desktop\\Maze\\Maze-Console\\generated-mazes\\maze3.png") { UseShellExecute = true });

            //DeadEndsCountTest.RunTest(100, 20); 

        }
    }
}