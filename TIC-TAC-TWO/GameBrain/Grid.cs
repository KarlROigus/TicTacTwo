namespace GameBrain;

public class Grid
{
    private int _middlePointX;
    private int _middlePointY;
    private bool[,]? _grid;

    private int _height;
    private int _width;
    

    public Grid(int boardSize, int middlePointX, int middlePointY)
    {

        _grid = new bool[boardSize, boardSize];
        _middlePointX = middlePointX;
        _middlePointY = middlePointY;
        _height = boardSize;
        _width = boardSize;
        
        for (int y = 0; y < _width; y++)
        {
            for (int x = 0; x < _height; x++)
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
        if (x + 1 == _middlePointX && y + 1 == _middlePointY)
        {
            return true;
        } if (x == _middlePointX && y + 1 == _middlePointY)
        {
            return true;
        } if (x - 1 == _middlePointX && y + 1 == _middlePointY)
        {
            return true;
        } if (x + 1 == _middlePointX && y == _middlePointY)
        {
            return true;
        } if (x == _middlePointX && y == _middlePointY)
        {
            return true;
        } if (x - 1 == _middlePointX && y == _middlePointY)
        {
            return true;
        } if (x + 1 == _middlePointX && y - 1 == _middlePointY)
        {
            return true;
        } if (x == _middlePointX && y - 1 == _middlePointY)
        {
            return true;
        } if (x - 1 == _middlePointX && y - 1 == _middlePointY)
        {
            return true;
        }
        return false;
    }

    public bool BooleanAt(int x, int y)
    {
        return _grid != null && _grid[x, y];
    }


    public void ChangeHeightAndWidth(int height, int width)
    {
        _height = height;
        _width = width;
        Console.WriteLine("here");
        
        for (int y = 0; y < _width; y++)
        {
            for (int x = 0; x < _height; x++)
            {
                if (_grid != null) _grid[x, y] = GiveSpotABooleanValue(x, y);
            }
        }
        
    }
    
}