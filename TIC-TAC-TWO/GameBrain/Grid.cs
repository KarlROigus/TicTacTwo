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
        
        [JsonConstructor]
        public Grid(int middlePointX, int middlePointY, int bigBoardSize, int gridLength) 
        {
            MiddlePointX = middlePointX;
            MiddlePointY = middlePointY;
            GridLength = gridLength;
            BigBoardSize = bigBoardSize; 

            _grid = new bool[BigBoardSize][]; 

            for (int y = 0; y < BigBoardSize; y++)
            {
                _grid[y] = new bool[BigBoardSize];
                for (int x = 0; x < BigBoardSize; x++)
                {
                    _grid[y][x] = GiveSpotABooleanValue(x, y);
                }
            }
        }


        public override string ToString()
        {
            var answer = "";

            if (_grid == null) return answer;

            for (int y = 0; y < _grid.Length; y++) 
            {
                for (int x = 0; x < _grid[y].Length; x++) 
                {
                    answer += _grid[y][x] + ", "; 
                }
                answer += "\n";
            }

            return answer;
        }


        public bool GiveSpotABooleanValue(int x, int y)
        {
            var dispersion = (GridLength - 1) / 2;
            if (Math.Abs(MiddlePointX - x) <= dispersion && Math.Abs(MiddlePointY - y) <= dispersion)
            {
                return true;
            }
            return false;
        }


        public bool BooleanAt(int x, int y)
        {
            return _grid != null && _grid[y][x]; 
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
