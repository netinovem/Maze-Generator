﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Console
{
    internal class HuntAndKill
    {
        public static Grid On(Grid grid)
        {
            Cell current = grid.RandomCell();

            while (current != null)
            {
                List<Cell> unvisitedNeighbors = new List<Cell>();

                foreach (Cell neighbor in current.Neighbors())
                {
                    if (neighbor.links.Count == 0) unvisitedNeighbors.Add(neighbor);
                }

                if (unvisitedNeighbors.Count != 0) //Kill mode
                {
                    Cell neighbor = unvisitedNeighbors[new Random().Next(unvisitedNeighbors.Count)]; //pick a random unvisited neighbor
                    current.Link(neighbor);
                    current = neighbor;
                }
                else //Hunt mode
                {
                    current = null;
                    foreach (Cell cell in grid.EachCell())
                    {
                        List<Cell> visitedNeighbors = new List<Cell>();
                        foreach (Cell neighbor in cell.Neighbors())
                        {
                            if (neighbor.links.Count > 0) visitedNeighbors.Add(neighbor);
                        }
                        if (cell.links.Count == 0 && visitedNeighbors.Count > 0)
                        {
                            current = cell;
                            Cell neighbor = visitedNeighbors[new Random().Next(visitedNeighbors.Count)];
                            current.Link(neighbor);
                        }
                    }
                }
            }

            return grid;
        }
    }
}
