using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Console
{
    internal class RecursiveBacktracker
    {
        public static Grid On(Grid grid)
        {
            Stack<Cell> stack = new Stack<Cell>();
            Cell startAt = grid.RandomCell();
            stack.Push(startAt);

            while (stack.Count > 0) 
            {
                Cell current = stack.Peek();
                //Create a list of unvisited neighbors of the current cell
                List<Cell> neighbors = current.Neighbors().Where(c => c.links.Count == 0).ToList(); //I learned some new syntax since last time HAHAHAHAH.

                if (neighbors.Count == 0) stack.Pop();
                else
                {
                    Cell neighbor = neighbors[new Random().Next(neighbors.Count)];
                    current.Link(neighbor);
                    stack.Push(neighbor);
                }
            }

            return grid;
        }
    }
}
