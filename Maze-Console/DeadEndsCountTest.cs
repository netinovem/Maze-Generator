using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Console
{
    internal class DeadEndsCountTest
    {
        public static void RunTest(int tries, int size)
        {
            List<Func<Grid, Grid>> algorithms = new List<Func<Grid, Grid>>
            {
                BinaryTree.On,
                Sidewinder.On,
                AldousBroder.On,
                Wilsons.On,
                HuntAndKill.On
            };

            Dictionary<string, double> averages = new Dictionary<string, double>();

            foreach (var algorithm in algorithms)
            {
                Console.WriteLine($"runing {algorithm.Method.DeclaringType.Name}...");
                List<int> deadEndsCounts = new List<int>();

                for (int i = 0; i < tries; i++)
                {
                    Grid testGrid = new Grid(size, size);
                    algorithm(testGrid);
                    deadEndsCounts.Add(testGrid.DeadEnds().Count);
                }

                int totalDeadEnds = deadEndsCounts.Sum();
                averages[algorithm.Method.DeclaringType.Name] = totalDeadEnds / deadEndsCounts.Count;
            }

            int totalCells = size * size;
            Console.WriteLine($"\nAverages dead-ends per {size}x{size} maze ({totalCells} cells):\n");

            var sortedAlgorithms = averages.OrderByDescending(x => x.Value);

            foreach (var algorithm in sortedAlgorithms)
            {
                double percentage = algorithm.Value * 100.0 / totalCells;
                Console.WriteLine($"{algorithm.Key,-14} : {algorithm.Value,3:F0}/{totalCells} ({percentage:F2}%)");
            }
        }
    }
}
