using SixLabors.ImageSharp;

namespace Maze_Console
{
    internal class ColoredGrid : Grid
    {
        public Distances _distanceMap;

        public Distances distanceMap
        {
            get { return _distanceMap; }

            set
            {
                _distanceMap = value;
                var (farthestCell, maxDistance) = _distanceMap.Max();
                this.farthest = farthestCell;
                this.maximum = maxDistance;
            }
        }

        private Cell farthest;
        private int maximum;

        public ColoredGrid(int rows, int columns) : base(rows, columns)
        {
            distanceMap = new Distances(null);
            var (farthestCell, maxDistance) = distanceMap.Max();
            this.farthest = farthestCell;
            this.maximum = maxDistance;
        }

        protected override Color? BackgroundColorFor(Cell cell)
        {
            if (distanceMap[cell] == null) return null;
            int? distance = distanceMap[cell];

            // SINGLE SHADE OF COLOR
            float intensity = (float)(maximum - distance) / maximum;
            byte bright = (byte)(intensity * 255);
            byte dark = (byte)(30 * intensity);
            return Color.FromRgb(dark, bright, dark);

            // MORE COLORFUL VERSION
            //int hue = (int)Math.Floor(100 - (100 * ((decimal)distance / maximum)));
            //HSL hsl = new HSL(hue, 100, 50);
            //RGB rgb = ColorHelper.ColorConverter.HslToRgb(hsl);
            //return Color.FromRgb(rgb.R, rgb.G, rgb.B);
        }

        public void SetGrid(Cell[,] grid)
        {
            this.grid = grid;
        }
    }
}