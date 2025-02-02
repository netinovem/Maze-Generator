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
            float intesity = (float) (maximum - distance) / maximum;
            byte dark = (byte)Math.Round((intesity * 255));
            byte bright =(byte)(Math.Round((intesity * 127)) + 128);

            return Color.FromRgb(dark, bright, dark);
        }
    }
}
