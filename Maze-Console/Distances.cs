namespace Maze_Console;

internal class Distances
{
    private Cell root;
    private Dictionary<Cell, int> cells;

    public Distances(Cell root)
    {
        if (root == null)
            cells = new Dictionary<Cell, int>();
        else
        {
            this.root = root;
            cells = new Dictionary<Cell, int> { { root, 0 } };
        }
    }

    public int? this[Cell cell]
    {
        get
        {
            if (cells.ContainsKey(cell))
            {
                return cells[cell];
            }
            return null;
        }
    }

    public void SetDistance(Cell cell, int distance)
    {
        cells[cell] = distance;
    }

    public IEnumerable<Cell> GetCells()
    {
        return cells.Keys;
    }

    public Dictionary<Cell, int> ToDictionary()
    {
        return cells;
    }

    public Distances PathTo(Cell goal)
    {
        Cell current = goal;
        var breadcrumbs = new Distances(root);
        breadcrumbs.SetDistance(current, cells[current]);
        while (current != root)
        {
            foreach (var neighbor in current.Links())
            {
                if (cells[neighbor] < cells[current])
                {
                    breadcrumbs.SetDistance(neighbor, cells[neighbor]);
                    current = neighbor;
                    break;
                }
            }
        }
        return breadcrumbs;
    }

    public (Cell maxCell, int maxDistance) Max()
    {
        int maxDistance = 0;
        Cell maxCell = root;

        foreach (var keyValuePair in cells)
        {
            Cell cell = keyValuePair.Key;
            int distance = keyValuePair.Value;

            if (distance > maxDistance)
            {
                maxDistance = distance;
                maxCell = cell;
            }
        }

        return (maxCell, maxDistance);
    }
}
