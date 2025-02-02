using SixLabors.ImageSharp;

namespace Maze_Console
{
    internal class DistanceGrid : Grid
    {
        public Dictionary<Cell, int> distanceMap { get; set; }   
        public DistanceGrid(int rows, int columns) : base(rows, columns) 
        { 
            this.distanceMap = new Dictionary<Cell, int>();
        }
        

        protected override string Contentsof(Cell cell)
        {
            if (distanceMap != null && distanceMap.ContainsKey(cell))
            {
                return ConvertDistanceToBase36(distanceMap[cell]);
            }
            else
                return base.Contentsof(cell);
        }

        private string ConvertDistanceToBase36(int distance)
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyz";
            string result = "";

            // Convert distance to base 36
            do
            {
                result = chars[distance % 36] + result;
                distance /= 36;
            } while (distance > 0);

            return result;
        }
    }
}