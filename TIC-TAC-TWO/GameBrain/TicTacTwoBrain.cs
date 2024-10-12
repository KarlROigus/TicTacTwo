namespace GameBrain;

public class TicTacTwoBrain
{
    private Grid _grid;
    private SpotOnTheBoard[,] _gameBoard;
    private bool _isGameOver = false;
    private int _movesMade = 0;
    private EGamePiece _nextMoveBy { get; set; } = EGamePiece.X;

    private GameConfiguration _gameConfiguration;

    public bool IsGameOver
    {
        get => _isGameOver;
        private set => _isGameOver = value;
    }

    public TicTacTwoBrain( GameConfiguration gameConfiguration)
    {
        _gameConfiguration = gameConfiguration;
        _grid = gameConfiguration.Grid;
        _gameBoard = new SpotOnTheBoard[gameConfiguration.BoardWidth, gameConfiguration.BoardHeight];
        
        for (int y = 0; y < gameConfiguration.BoardHeight; y++)
        {
            for (int x = 0; x < gameConfiguration.BoardWidth; x++)
            {
                _gameBoard[x, y] = new SpotOnTheBoard(EGamePiece.Empty, CheckIfSpotIsPartOfGrid(x, y));
            }
        }
    }

    private bool CheckIfSpotIsPartOfGrid(int x, int y)
    {
        return _grid.BooleanAt(x, y);
    }

    public Grid GetGrid()
    {
        return _grid;
    }
    

    public SpotOnTheBoard[,] GameBoard
    {
        get => GetBoard();
        private set => _gameBoard = value;
    }

    public int DimX => _gameBoard.GetLength(0);
    public int DimY => _gameBoard.GetLength(1);
    
    private SpotOnTheBoard[,] GetBoard()
    {
        var copyOfBoard = new SpotOnTheBoard[_gameBoard.GetLength(0), _gameBoard.GetLength(1)]; 
        for (var x = 0; x < _gameBoard.GetLength(0); x++)
        {
            for (var y = 0; y < _gameBoard.GetLength(1); y++)
            {
                copyOfBoard[x, y] = _gameBoard[x, y];
            }
        }
        return copyOfBoard;

    }

    public bool MoveTheGrid(int centerX, int centerY)
    {
        
        var newGrid = new Grid(DimY, centerX, centerY, _grid.GetGridLength());

        Console.WriteLine(newGrid);
        Console.ReadLine();

        for (int y = 0; y < DimY; y++)
        {
            for (int x = 0; x < DimX; x++)
            {
                var currentSpot = _gameBoard[x, y];
                currentSpot.SetSpotBoolean(newGrid.BooleanAt(x, y));
            }
        }
        
        return true;
    }
    

    public bool MakeAMove(int x, int y)
    {
        if (_gameBoard[x, y].GetSpotValue() != EGamePiece.Empty)
        {
            return false;
        }

        _gameBoard[x, y].SetSpotValue(_nextMoveBy);
        _nextMoveBy = _nextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
        _movesMade++;
        
        return true;
    }

    public int GetMovesMade()
    {
        return _movesMade;
    }

    public void ResetGame()
    {
        _gameBoard = new SpotOnTheBoard[_gameBoard.GetLength(0), _gameBoard.GetLength(1)];
        
        for (int x = 0; x < _gameBoard.GetLength(0); x++)
        {
            for (int y = 0; y < _gameBoard.GetLength(1); y++)
            {
                _gameBoard[x, y] = new SpotOnTheBoard(EGamePiece.Empty, true);
            }
        }
        
        _nextMoveBy = EGamePiece.X;
        _isGameOver = false;
    }

    public EGamePiece GetNextOneToMove()
    {
        return _nextMoveBy;
    }
    
    
    public EGamePiece GetPreviousMover()
    {
        return _nextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
    }
    

    public bool SomebodyHasWon()
    {
        return GameWinThroughARow() || GameWinThroughAColumn() || GameWinThroughADiagonal();
    }

    private bool GameWinThroughARow()
    {
        for (int y = 0; y < _gameConfiguration.BoardHeight; y++)
        {
            var sumOfRow = 0;
            for (int x = 0; x < _gameConfiguration.BoardWidth; x++)
            {
                var currentPiece = _gameBoard[x, y];
                if (currentPiece.IsPartOfGrid)
                {
                    sumOfRow += currentPiece.GetSpotValue() == EGamePiece.X ? 1 :
                        currentPiece.GetSpotValue() == EGamePiece.Empty ? 0 :
                        -1;
                }

            }
            if (Math.Abs(sumOfRow) == _gameConfiguration.WinCondition)
            {
                return true;
            }
        }
        
        return false;
    }


    private bool GameWinThroughAColumn()
    {

        for (int x = 0; x < _gameConfiguration.BoardWidth; x++)
        {
            var sumOfColumn = 0;
            for (int y = 0; y < _gameConfiguration.BoardHeight; y++)
            {
                var currentPiece = _gameBoard[x, y];
                if (currentPiece.IsPartOfGrid)
                {
                    sumOfColumn += currentPiece.GetSpotValue() == EGamePiece.X ? 1 :
                        currentPiece.GetSpotValue() == EGamePiece.Empty ? 0 :
                        -1;
                }
            }
            if (Math.Abs(sumOfColumn) == _gameConfiguration.WinCondition)
            {
                return true;
            }
        }
        

        return false;
    }


    private bool GameWinThroughADiagonal()
    {
        Console.WriteLine(_grid.GetGridMiddleXValue());
        Console.WriteLine(_grid.GetGridMiddleYValue());
        
        return false;
    }

    
}