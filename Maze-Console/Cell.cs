namespace Maze_Console
{
    internal class Cell
    {
        public int row { get; private set; }
        public int column { get; private set; }

        public Cell north;
        public Cell south;
        public Cell east;
        public Cell west;

        public Dictionary<Cell, bool> links { get; private set; }

        public Cell(int row, int column)
        {
            this.row = row;
            this.column = column;
            this.links = new Dictionary<Cell, bool>();
        }

        public void Link(Cell cell, bool bidi = true)
        {
            links[cell] = true;
            if (bidi)
            {
                cell.Link(this, false);
            }
        }

        public void Unlink(Cell cell, bool bidi = true)
        {
            links.Remove(cell);
            if (bidi)
            {
                cell.Unlink(this, false);
            }
        }

        public IEnumerable<Cell> Links()
        {
            return links.Keys;
        }

        public bool IsLinked(Cell cell)
        {
            if (cell == null) return false;
            return links.ContainsKey(cell);
        }

        public List<Cell> Neighbors()
        {
            List<Cell> neighbors = new List<Cell>();

            if (north != null) neighbors.Add(north);
            if (south != null) neighbors.Add(south);
            if (east != null) neighbors.Add(east);
            if (west != null) neighbors.Add(west);

            return neighbors;
        }

        public Distances Distances()
        {
            var distances = new Distances(this); //create a new instance of Distances with the current cell as the root
            var frontier = new List<Cell> { this }; //create a list of cells with the current cell as the only member

            while (frontier.Any()) //while there are still cells in the frontier
            {
                var newFrontier = new List<Cell>();

                foreach (var cell in frontier)
                {
                    //Iterate through each linked cell (neighbors)
                    foreach (var linked in cell.Links())
                    {
                        if (distances[linked] != null) continue; //if the linked cell is already in the distances dictionary, skip it
                        distances.SetDistance(linked, distances[cell].Value + 1);
                        newFrontier.Add(linked);
                    }
                }

                frontier = newFrontier;
            }

            return distances;
        }
    }
}