namespace Maze_Console
{
    internal class Sidewinder
    {
        public static Grid On(Grid grid)
        {
            foreach (var row in grid.EachRow())
            {
                List<Cell> run = new List<Cell>();
                foreach (var cell in row)
                {
                    run.Add(cell);

                    //check if we are at the eastern or northern boundary for special handling
                    bool atEasternBoundary = cell.east == null;
                    bool atNorthernBoundary = cell.north == null;

                    bool shouldCloseOut = atEasternBoundary || (!atNorthernBoundary && new Random().Next(2) == 0);

                    if (shouldCloseOut)
                    {
                        Cell member = run[new Random().Next(run.Count)];
                        if (member.north != null) member.Link(member.north);
                        run.Clear();
                    }
                    else cell.Link(cell.east);
                }
            }
            return grid;
        }
    }
}