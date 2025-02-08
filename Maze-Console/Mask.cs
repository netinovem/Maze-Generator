namespace Maze_Console;

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

    public bool this[int row, int column]
    {
        get
        {
            if (row >= 0 && row < rows && column >= 0 && column < columns)
                return bits[row, column];
            else
                return false;
        }
        set
        {
            if (row >= 0 && row < rows && column >= 0 && column < columns)
                bits[row, column] = value;
        }
    }

    public int Count()
    {
        int count = 0;

        foreach (var row in Enumerable.Range(0, rows))
        {
            foreach (var column in Enumerable.Range(0, columns))
            {
                if (bits[row, column])
                    count++;
            }
        }

        return count;
    }

    public (int row, int column) RandomLocation()
    {
        while (true)
        {
            int row = new Random().Next(0, rows);
            int column = new Random().Next(0, columns);

            if (bits[row, column])
            {
                return (row, column);
            }
        }
    }

    public static Mask FromTxt(string file)
    {
        var lines = File.ReadAllLines(file);

        while (lines.Length > 0 && string.IsNullOrWhiteSpace(lines[^1]))
        {
            Array.Resize(ref lines, lines.Length - 1);
        }

        int rows = lines.Length;
        int columns = lines[0].Length;
        var mask = new Mask(rows, columns);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Set the mask cell based on the character read from the file
                mask[row, col] = lines[row][col] != 'X'; // true for '.', false for 'X'
            }
        }

        return mask;
    }
}
