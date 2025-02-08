namespace Maze_Console;

internal class MaskedGrid : Grid
{
    public Mask mask { get; }

    public MaskedGrid(Mask mask)
        : base(mask.rows, mask.columns)
    {
        this.mask = mask;
        PrepareGrid();
        base.ConfigureCells();
    }

    protected void PrepareGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                if (mask[row, column])
                    grid[row, column] = new Cell(row, column);
                else
                    grid[row, column] = null;
            }
        }
    }

    public override Cell RandomCell()
    {
        (int row, int column) = mask.RandomLocation();

        return this[row, column];
    }

    public override int Size()
    {
        return mask.Count();
    }

    private Distances GetDistances(Cell root)
    {
        var distances = root.Distances();
        return distances;
    }

    public ColoredGrid ToColoredGrid(Cell root)
    {
        var distanceMap = GetDistances(root);
        var coloredGrid = new ColoredGrid(mask.rows, mask.columns);
        coloredGrid.SetGrid(this.grid);
        coloredGrid.distanceMap = distanceMap;
        return coloredGrid;
    }
}
