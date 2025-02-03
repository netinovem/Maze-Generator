using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Console
{
    internal class Wilsons
    {
        public static Grid On(Grid grid)
        {
            //initializing a list of unvisited cells
            List<Cell> unvisited = new List<Cell>();
            foreach (var cell in grid.EachCell())
            {
                unvisited.Add(cell);
            }

            //picking a random cell to start with
            Cell start = unvisited[new Random().Next(unvisited.Count)];
            unvisited.Remove(start);

            while (unvisited.Count > 0)
            {
                //picking a random unvisited cell
                Cell cell = unvisited[new Random().Next(unvisited.Count)];
                List<Cell> path = new List<Cell> { cell };

                //continue the loop until we find a cell that's part of an existing path
                while (unvisited.Contains(cell))
                {
                    cell = cell.Neighbors(cell)[new Random().Next(cell.Neighbors(cell).Count)];

                    //check if we have already visited this cell  
                    int position = path.IndexOf(cell);

                    if (position >= 0)
                    {
                        //if we have visited the cell, erase the loop in the path  
                        path = path.GetRange(0, position + 1); //DIDN't UNDERSTAND this line tbh TvT
                    }
                    else path.Add(cell);
                }

                //linking the cells in the path
                for (int i = 0; i < path.Count - 1; i++)
                {
                    path[i].Link(path[i + 1]);
                    unvisited.Remove(path[i]); //and removing from unvisited
                }
            }

            return grid;
        }
    }
}