namespace GameBrain;

public class TicTacTwoBrain
{
    private readonly GameState _gameState;
    

    public TicTacTwoBrain(GameState gameState)
    {

        _gameState = new GameState(gameState.Grid, 
            gameState.GameBoard, 
            gameState.GameConfiguration, 
            gameState.MovesMade,
            gameState.NextMoveBy);
        
    }

    public string GetGameStateJson()
    {
        return _gameState.ToString();
    }

    public string GetGameConfigName()
    {
        return _gameState.GameConfiguration.Name;
    }
    public Grid GetGrid()
    {
        return _gameState.Grid;
    }

    public int GetMovesMade()
    {
        return _gameState.MovesMade;
    }
    

    public SpotOnTheBoard[][] GameBoard
    {
        get => GetBoard();
        private set => _gameState.GameBoard = value;
    }

    public int DimX => _gameState.GameBoard[0].Length;
    public int DimY => _gameState.GameBoard.Length;
    
    private SpotOnTheBoard[][] GetBoard()
    {
        var copyOfBoard = new SpotOnTheBoard[DimY][];
        for (var y = 0; y < DimY; y++)
        {
            copyOfBoard[y] = new SpotOnTheBoard[DimY];
            
            for (var x = 0; x < DimX; x++)
            {
                copyOfBoard[y][x] =  _gameState.GameBoard[y][x];
            }
        }
    
        return copyOfBoard;

    }

    public bool MoveTheGrid(int centerX, int centerY)
    {
        
        var newGrid = new Grid(centerX, centerY, DimY, _gameState.Grid.GetGridLength());
        
        for (int y = 0; y < DimY; y++)
        {
            for (int x = 0; x < DimX; x++)
            {
                var currentSpot = _gameState.GameBoard[y][x];
                currentSpot.SetSpotBoolean(newGrid.BooleanAt(x, y));
            }
        }
        _gameState.NextMoveBy = _gameState.NextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
        _gameState.Grid = newGrid;
        Console.WriteLine("Grid location changed successfully! Press Enter to continue.");
        Console.ReadLine();
        
        return true;
    }
    

    public bool MakeAMove(int x, int y)
    {
        if (_gameState.GameBoard[y][x].GetSpotValue() != EGamePiece.Empty)
        {
            return false;
        }

        _gameState.GameBoard[y][x].SetSpotValue(_gameState.NextMoveBy);
        _gameState.NextMoveBy = _gameState.NextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
        _gameState.MovesMade++;
        
        return true;
    }
    

    public EGamePiece GetNextOneToMove()
    {
        return _gameState.NextMoveBy;
    }
    
    
    public EGamePiece GetPreviousMover()
    {
        return _gameState.NextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
    }
    

    public bool SomebodyHasWon()
    {
        return GameWinThroughARow() || GameWinThroughAColumn() || GameWinThroughADiagonal();
    }

    private bool GameWinThroughARow()
    {
        for (int y = 0; y < _gameState.GameConfiguration.BoardHeight; y++)
        {
            var sumOfRow = 0;
            for (int x = 0; x < _gameState.GameConfiguration.BoardWidth; x++)
            {
                var currentPiece = _gameState.GameBoard[y][x];
                if (currentPiece.IsPartOfGrid)
                {
                    sumOfRow += currentPiece.GetSpotValue() == EGamePiece.X ? 1 :
                        currentPiece.GetSpotValue() == EGamePiece.Empty ? 0 :
                        -1;
                }

            }
            if (Math.Abs(sumOfRow) == _gameState.GameConfiguration.WinCondition)
            {
                return true;
            }
        }
        
        return false;
    }


    private bool GameWinThroughAColumn()
    {

        for (int x = 0; x < _gameState.GameConfiguration.BoardWidth; x++)
        {
            var sumOfColumn = 0;
            for (int y = 0; y < _gameState.GameConfiguration.BoardHeight; y++)
            {
                var currentSpot = _gameState.GameBoard[y][x];
                if (currentSpot.IsPartOfGrid)
                {
                    sumOfColumn += currentSpot.GetSpotValue() == EGamePiece.X ? 1 :
                        currentSpot.GetSpotValue() == EGamePiece.Empty ? 0 :
                        -1;
                }
            }
            if (Math.Abs(sumOfColumn) == _gameState.GameConfiguration.WinCondition)
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
        var leftDiagonalStartIndexY = _gameState.Grid.GetGridMiddleYValue() - (_gameState.Grid.GetGridLength() - 1) / 2;
        var leftDiagonalEndIndexY = _gameState.Grid.GetGridMiddleYValue() + (_gameState.Grid.GetGridLength() - 1) / 2;

        var leftDiagonalStartIndexX = _gameState.Grid.GetGridMiddleXValue() - (_gameState.Grid.GetGridLength() - 1) / 2;

        for (int y = leftDiagonalStartIndexY; y <= leftDiagonalEndIndexY; y++)
        {
            var currentSpot = _gameState.GameBoard[y][leftDiagonalStartIndexX];
            if (currentSpot.IsPartOfGrid)
            {
                sumOfDiagonal += currentSpot.GetSpotValue() == EGamePiece.X ? 1 :
                    currentSpot.GetSpotValue() == EGamePiece.Empty ? 0 :
                    -1;
            }

            leftDiagonalStartIndexX++;

        }
        return Math.Abs(sumOfDiagonal) == _gameState.GameConfiguration.WinCondition;
    }

    private bool GameWinThroughRightDiagonal()
    {

        var sumOfDiagonal = 0;
        var rightDiagonalStartIndexY = _gameState.Grid.GetGridMiddleYValue() - (_gameState.Grid.GetGridLength() - 1) / 2;
        var rightDiagonalEndIndexY = _gameState.Grid.GetGridMiddleYValue() + (_gameState.Grid.GetGridLength() - 1) / 2;
        
        var rightDiagonalStartIndexX = _gameState.Grid.GetGridMiddleXValue() + (_gameState.Grid.GetGridLength() - 1) / 2;
        
        for (int y = rightDiagonalStartIndexY; y <= rightDiagonalEndIndexY; y++)
        {
            var currentSpot = _gameState.GameBoard[y][rightDiagonalStartIndexX];
            if (currentSpot.IsPartOfGrid)
            {
                sumOfDiagonal += currentSpot.GetSpotValue() == EGamePiece.X ? 1 :
                    currentSpot.GetSpotValue() == EGamePiece.Empty ? 0 :
                    -1;
            }

            rightDiagonalStartIndexX--;

        }
        
        return Math.Abs(sumOfDiagonal) == _gameState.GameConfiguration.WinCondition;

    }


    public void SetGameStateFromSavedFile()
    {
        
    }
    
}