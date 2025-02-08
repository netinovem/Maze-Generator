using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Text;

namespace Maze_Console
{
    internal class Grid
    {
        public int rows { get; private set; }
        public int columns { get; private set; }

        protected Cell[,] grid;

        public Grid(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;

            this.grid = PrepareGrid();
            ConfigureCells();
        }

        protected virtual Cell[,] PrepareGrid()
        {
            Cell[,] newGrid = new Cell[rows, columns];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    newGrid[r, c] = new Cell(r, c);
                }
            }

            return newGrid;
        }

        //Accessors method
        public Cell? this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= rows)
                {
                    return null;
                }
                if (column < 0 || column >= columns)
                {
                    return null;
                }
                return grid[row, column];
            }
        }

        protected virtual void ConfigureCells()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    var cell = this[r, c];
                    if (cell != null)
                    {
                        cell.north = this[r - 1, c];
                        cell.south = this[r + 1, c];
                        cell.west = this[r, c - 1];
                        cell.east = this[r, c + 1];
                    }
                }
            }
        }

        public virtual Cell RandomCell()
        {
            Random random = new Random();
            int row = random.Next(rows);
            int column = random.Next(columns);
            return grid[row, column];
        }

        public virtual int Size()
        {
            return rows * columns;
        }

        //iterator methods (holycrap the "yield" is awesome i dodn't know that!)
        public IEnumerable<Cell[]> EachRow()
        {
            for (int r = 0; r < rows; r++)
            {
                Cell[] row = new Cell[columns];
                for (int c = 0; c < columns; c++)
                {
                    row[c] = grid[r, c];
                }
                yield return row;
            }
        }

        public IEnumerable<Cell> EachCell()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    yield return grid[r, c];
                }
            }
        }

        protected virtual string Contentsof(Cell cell)
        {
            return " "; //default behavior is to return a space
        }

        protected virtual Color? BackgroundColorFor(Cell cell)
        {
            return null;
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();

            output.Append("+");
            for (int c = 0; c < columns; c++)
            {
                output.Append("---+");
            }
            output.AppendLine();

            foreach (var row in EachRow())
            {
                string top = "|";
                string bottom = "+";

                foreach (var cell in row)
                {
                    var currentCell = cell ?? new Cell(-1, -1);  // i don't know why this is needed

                    string body = $" {Contentsof(currentCell)} "; // Three spaces

                    // Right boundary for the cell
                    string eastBoundary = currentCell.IsLinked(currentCell.east) ? " " : "|";
                    top += body + eastBoundary;

                    // Bottom boundary for the cell
                    string SouthBoundary = currentCell.IsLinked(currentCell.south) ? "   " : "---";
                    bottom += SouthBoundary + "+";
                }

                // Append the constructed top and bottom parts for the row
                output.AppendLine(top);
                output.AppendLine(bottom);
            }
            return output.ToString();
        }

        public virtual Image<Rgba64> ToImg(int cellSize = 30, int margin = 60)
        {
            int imgWidth = columns * cellSize + margin * 2;
            int imgHeight = rows * cellSize + margin * 2;

            //create a blank white image
            Image<Rgba64> image = new Image<Rgba64>(imgWidth + 1, imgHeight + 1);
            image.Mutate(ctx => ctx.BackgroundColor(Color.White)); // Set background to white

            foreach (var mode in new[] { "background", "wall" })
            {
                foreach (var cell in EachCell())
                {
                    if (cell == null) continue;

                    int x1 = cell.column * cellSize + margin;
                    int y1 = cell.row * cellSize + margin;
                    int x2 = (cell.column + 1) * cellSize + margin;
                    int y2 = (cell.row + 1) * cellSize + margin;

                    if (mode == "background")
                    {
                        Color? backgroundColor = BackgroundColorFor(cell);
                        if (backgroundColor != null) image.Mutate(ctx => ctx.Fill((Color)backgroundColor, new RectangleF(x1, y1, cellSize, cellSize)));
                    }
                    else //mode == "wall"
                    {
                        // Draw the walls based on cell connections
                        if (cell.north == null) // No northern link
                        {
                            image.Mutate(ctx => ctx.DrawLine(Color.Black, 1, new PointF(x1, y1), new PointF(x2, y1)));
                        }
                        if (cell.west == null) // No western link
                        {
                            image.Mutate(ctx => ctx.DrawLine(Color.Black, 1, new PointF(x1, y1), new PointF(x1, y2)));
                        }
                        if (!cell.IsLinked(cell.east)) // Not linked to the east
                        {
                            image.Mutate(ctx => ctx.DrawLine(Color.Black, 1, new PointF(x2, y1), new PointF(x2, y2)));
                        }
                        if (!cell.IsLinked(cell.south)) // Not linked to the south
                        {
                            image.Mutate(ctx => ctx.DrawLine(Color.Black, 1, new PointF(x1, y2), new PointF(x2, y2)));
                        }
                    }
                }
            }

            return image;
        }

        public List<Cell> DeadEnds()
        {
            List<Cell> deadEnds = new List<Cell>();
            foreach (var cell in EachCell())
            {
                if (cell.Links().Count() == 1)
                {
                    deadEnds.Add(cell);
                }
            }
            return deadEnds;
        }
    }
}