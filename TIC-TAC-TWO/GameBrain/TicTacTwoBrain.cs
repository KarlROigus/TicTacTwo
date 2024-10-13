namespace GameBrain;

public class TicTacTwoBrain
{
    private Grid _grid;
    private SpotOnTheBoard[,] _gameBoard;
    private int _movesMade = 0;
    private EGamePiece NextMoveBy { get; set; } = EGamePiece.X;

    private readonly GameConfiguration _gameConfiguration;
    

    public TicTacTwoBrain(GameConfiguration gameConfiguration)
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
        
        for (int y = 0; y < DimY; y++)
        {
            for (int x = 0; x < DimX; x++)
            {
                var currentSpot = _gameBoard[x, y];
                currentSpot.SetSpotBoolean(newGrid.BooleanAt(x, y));
            }
        }
        NextMoveBy = NextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
        _grid = newGrid;
        Console.WriteLine("Grid location changed successfully! Press Enter to continue.");
        Console.ReadLine();
        
        return true;
    }
    

    public bool MakeAMove(int x, int y)
    {
        if (_gameBoard[x, y].GetSpotValue() != EGamePiece.Empty)
        {
            return false;
        }

        _gameBoard[x, y].SetSpotValue(NextMoveBy);
        NextMoveBy = NextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
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
        
        NextMoveBy = EGamePiece.X;
    }

    public EGamePiece GetNextOneToMove()
    {
        return NextMoveBy;
    }
    
    
    public EGamePiece GetPreviousMover()
    {
        return NextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
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
                var currentSpot = _gameBoard[x, y];
                if (currentSpot.IsPartOfGrid)
                {
                    sumOfColumn += currentSpot.GetSpotValue() == EGamePiece.X ? 1 :
                        currentSpot.GetSpotValue() == EGamePiece.Empty ? 0 :
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
        return GameWinThroughLeftDiagonal() || GameWinThroughRightDiagonal();


    }

    private bool GameWinThroughLeftDiagonal()
    {
        var sumOfDiagonal = 0;
        var leftDiagonalStartIndexY = _grid.GetGridMiddleYValue() - (_grid.GetGridLength() - 1) / 2;
        var leftDiagonalEndIndexY = _grid.GetGridMiddleYValue() + (_grid.GetGridLength() - 1) / 2;

        var leftDiagonalStartIndexX = _grid.GetGridMiddleXValue() - (_grid.GetGridLength() - 1) / 2;

        for (int y = leftDiagonalStartIndexY; y <= leftDiagonalEndIndexY; y++)
        {
            var currentSpot = _gameBoard[leftDiagonalStartIndexX, y];
            if (currentSpot.IsPartOfGrid)
            {
                sumOfDiagonal += currentSpot.GetSpotValue() == EGamePiece.X ? 1 :
                    currentSpot.GetSpotValue() == EGamePiece.Empty ? 0 :
                    -1;
            }

            leftDiagonalStartIndexX++;

        }
        return Math.Abs(sumOfDiagonal) == _gameConfiguration.WinCondition;
    }

    private bool GameWinThroughRightDiagonal()
    {
        return false;
    }
    
}