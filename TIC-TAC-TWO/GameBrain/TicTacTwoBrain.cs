namespace GameBrain;

public class TicTacTwoBrain
{
    private Grid _grid;
    private SpotOnTheBoard[,] _gameBoard;
    private bool _isGameOver = false;
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
        _grid = new Grid(gameConfiguration.BoardWidth, gameConfiguration.GridMiddlePointXValue, gameConfiguration.GridMiddlePointYValue, gameConfiguration.GridWidth);
        
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

    public bool MakeAMove(int x, int y)
    {
        if (_gameBoard[x, y].GetSpotValue() != EGamePiece.Empty)
        {
            return false;
        }

        _gameBoard[x, y].SetSpotValue(_nextMoveBy);
        _nextMoveBy = _nextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X; 
        
        return true;
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

    public Grid GetGrid()
    {
        return _grid;
    }

    public EGamePiece GetNextOneToMove()
    {
        return _nextMoveBy;
    }

    public string ChangeGridSize()
    {
        Console.Clear();
        
        Console.WriteLine("Please enter the new grid HEIGHT: ");
        var height = Console.ReadLine();
        
        
        _grid.ChangeHeightAndWidth(int.Parse(height));
        
        
        for (int y = 0; y < _gameBoard.GetLength(0); y++)
        {
            for (int x = 0; x < _gameBoard.GetLength(1); x++)
            {
                _gameBoard[x, y] = new SpotOnTheBoard(EGamePiece.Empty, CheckIfSpotIsPartOfGrid(x, y));
            }
        }
        
        
        return "hi";
    }

    public bool SomebodyHasWon()
    {
        if (_isGameOver)
        {
            return true;
        }

        _isGameOver = !_isGameOver;
        return false;
    }
}