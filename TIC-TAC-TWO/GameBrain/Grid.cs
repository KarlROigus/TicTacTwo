namespace GameBrain;

public class Grid
{
    private bool[,]? _grid;

    private int _middlePointX;
    private int _middlePointY;
    private int _bigBoardSize;
    private int _gridLength;
    

    public Grid(int boardSize, int middlePointX, int middlePointY, int gridLength)
    {
        _grid = new bool[boardSize, boardSize];
        _middlePointX = middlePointX;
        _middlePointY = middlePointY;
        _bigBoardSize = boardSize;
        _gridLength = gridLength;
        
        for (int y = 0; y < _bigBoardSize; y++)
        {
            for (int x = 0; x < _bigBoardSize; x++)
            {
                _grid[x, y] = GiveSpotABooleanValue(x, y);
            }
        }
    }

    public override string ToString()
    {
        var answer = "";

        if (_grid == null) return answer;
        for (int y = 0; y < _grid.GetLength(0); y++)
        {
            for (int x = 0; x < _grid.GetLength(1); x++)
            {
                answer += _grid[x, y] + ", ";
            }

            answer += "\n";
        }

        return answer;
    }

    private bool GiveSpotABooleanValue(int x, int y)
    {

        var dispersion = (_gridLength - 1) / 2;
        if (Math.Abs(_middlePointX - x) <= dispersion && Math.Abs(_middlePointY - y) <= dispersion)
        {
            return true;
        }
        return false;
    }

    public bool BooleanAt(int x, int y)
    {
        return _grid != null && _grid[x, y];
    }

    public int GetGridMiddleXValue()
    {
        return _middlePointX;
    }

    public int GetGridMiddleYValue()
    {
        return _middlePointY;
    }
    
    
    
    
}