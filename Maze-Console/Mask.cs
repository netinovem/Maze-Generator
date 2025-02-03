using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Console
{
    internal class Mask
    {
        public int rows { get; private set; }
        public int columns { get; private set; }

        public bool[,] bits;

        public Mask(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            bits = new bool[rows, columns];

            //initialize all bits to true
            foreach (var row in Enumerable.Range(0, rows))
            {
                foreach (var column in Enumerable.Range(0, columns))
                {
                    bits[row, column] = true;
                }
            }
        }


    }
}
