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
            float intensity = (float)(maximum - distance) / maximum;

            byte bright = (byte)(intensity * 240);
            byte dark = (byte)(60 * intensity);

            return Color.FromRgb(dark, bright, dark);
        }
    }
}
