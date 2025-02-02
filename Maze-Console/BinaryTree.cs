namespace Maze_Console
{
    internal class BinaryTree
    {
        public static Grid On(Grid grid)
        {
            foreach (Cell cell in grid.EachCell())
            {
                List<Cell> neighbors = new List<Cell>();

                if (cell.north != null) neighbors.Add(cell.north);
                if (cell.east != null) neighbors.Add(cell.east);

                if (neighbors.Count == 0) continue;

                int index = new Random().Next(neighbors.Count);
                Cell neighbor = neighbors[index];
                cell.Link(neighbor);
            }

            return grid;
        }
    }
}