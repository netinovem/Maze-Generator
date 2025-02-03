using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Console
{
    internal class AldousBroder
    {
        public static Grid On(Grid grid)
        {
            Cell cell = grid.RandomCell();
            int unvisited = grid.Size() - 1;

            while (unvisited > 0)
            {
                //picking a random neighbor
                Cell neighbor = cell.Neighbors(cell)[new Random().Next(cell.Neighbors(cell).Count)];

                if (neighbor.links.Count == 0)
                {
                    cell.Link(neighbor);
                    unvisited--;
                }

                cell = neighbor;
            }

            return grid;
        }
    }
}
