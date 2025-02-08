using SixLabors.ImageSharp;
using System.Diagnostics;

namespace Maze_Console;

internal class Program
{
    private static void Main(string[] args)
    {
        //ColoredGrid coloredGrid = new ColoredGrid(200, 200);
        ////coloredGrid[0, 0].east.west = null;
        ////coloredGrid[0, 0].south.north = null;
        //HuntAndKill.On(coloredGrid);
        //Cell start = coloredGrid[coloredGrid.rows/2, coloredGrid.columns/2];
        //Distances distanceMap = start.Distances();
        //coloredGrid.distanceMap = distanceMap;
        //var img = coloredGrid.ToImg(60, 60);

        //img.SaveAsPng("..\\..\\..\\generated-mazes\\maze3.png");
        //Process.Start(new ProcessStartInfo("..\\..\\..\\generated-mazes\\maze3.png") { UseShellExecute = true });

        //DeadEndsCountTest.RunTest(100, 20);

        //Masking -----------------
        MaskedGrid maskedGrid = new(Mask.FromTxt("..\\..\\..\\masks\\mask.txt"));
        RecursiveBacktracker.On(maskedGrid);
        Cell start = maskedGrid[0, 1];
        ColoredGrid coloredMaskedGrid = maskedGrid.ToColoredGrid(start);

        var img = coloredMaskedGrid.ToImg(60, 60);
        img.SaveAsPng("..\\..\\..\\generated-mazes\\maskedmaze.png");
        Process.Start(new ProcessStartInfo("..\\..\\..\\generated-mazes\\maskedmaze.png") { UseShellExecute = true });

        //pushing the limits ----------------
        //Grid maze = new(4600, 4600);
        //Task<Grid> task = Task.Run(() => RecursiveBacktracker.On(maze));
        //Console.WriteLine("running...");
        //task.ContinueWith(t => Console.WriteLine("finished"));
        //task.Wait();
    }
}