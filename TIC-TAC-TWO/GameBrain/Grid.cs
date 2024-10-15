using System.Text.Json.Serialization;

namespace GameBrain
{
    public class Grid
    {
        public bool[][]? _grid;

        public int MiddlePointX { get; set; }
        public int MiddlePointY { get; set; }
        public int BigBoardSize { get; set; }
        public int GridLength { get; set; }

        // Constructor
        public Grid(int boardSize, int middlePointX, int middlePointY, int gridLength)
        {
            _grid = new bool[boardSize][]; // Initialize the outer array (rows)

            MiddlePointX = middlePointX;
            MiddlePointY = middlePointY;
            BigBoardSize = boardSize;
            GridLength = gridLength;

            // Initialize each row (inner array)
            for (int y = 0; y < BigBoardSize; y++)
            {
                _grid[y] = new bool[BigBoardSize]; // Initialize each row (columns)
                for (int x = 0; x < BigBoardSize; x++)
                {
                    _grid[y][x] = GiveSpotABooleanValue(x, y); // Populate the grid
                }
            }
        }

        // Override ToString for printing the grid
        public override string ToString()
        {
            var answer = "";

            if (_grid == null) return answer;

            for (int y = 0; y < _grid.Length; y++) // Use Length for jagged arrays
            {
                for (int x = 0; x < _grid[y].Length; x++) // Use Length for jagged arrays
                {
                    answer += _grid[y][x] + ", "; // Access grid as [y][x]
                }
                answer += "\n";
            }

            return answer;
        }

        // Method to determine boolean value at a spot
        public bool GiveSpotABooleanValue(int x, int y)
        {
            var dispersion = (GridLength - 1) / 2;
            if (Math.Abs(MiddlePointX - x) <= dispersion && Math.Abs(MiddlePointY - y) <= dispersion)
            {
                return true;
            }
            return false;
        }

        // Access a specific boolean at position (x, y)
        public bool BooleanAt(int x, int y)
        {
            return _grid != null && _grid[y][x]; // Access as [y][x]
        }

        public int GetGridMiddleXValue()
        {
            return MiddlePointX;
        }

        public int GetGridMiddleYValue()
        {
            return MiddlePointY;
        }

        public int GetGridLength()
        {
            return GridLength;
        }
    }
}
