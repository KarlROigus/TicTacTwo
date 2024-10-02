namespace GameBrain;

public class TicTacTwoBrain
{
    private Grid _grid;
    private SpotOnTheBoard[,] _gameBoard;
    private bool _isGameOver = false;
    private EGamePiece _nextMoveBy { get; set; } = EGamePiece.X;

    public bool isGameOver
    {
        get => _isGameOver;
        private set => _isGameOver = value;
    }

    private TicTacTwoBrain(int boardX, int boardY)
    {
        _grid = new Grid(boardX, boardX / 2, boardY / 2);
        
        _gameBoard = new SpotOnTheBoard[boardX, boardY];
        for (int x = 0; x < boardX; x++)
        {
            for (int y = 0; y < boardY; y++)
            {
                _gameBoard[x, y] = new SpotOnTheBoard(EGamePiece.Empty, CheckIfSpotIsPartOfGrid(x, y));
            }
        }
    }

    private bool CheckIfSpotIsPartOfGrid(int x, int y)
    {
        return _grid.BooleanAt(x, y);
    }

    public TicTacTwoBrain() : this(5, 5) {}
    public TicTacTwoBrain(int boardSize) : this(boardSize, boardSize) { }
    

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

    public string ChangeGridSize()
    {
        Console.Clear();
        
        Console.WriteLine("Please enter the new grid WIDTH: ");
        var width = Console.ReadLine();
        
        Console.WriteLine("Please enter the new grid HEIGHT: ");
        var height = Console.ReadLine();
        
        
        Console.WriteLine(_grid);
        
        _grid.ChangeHeightAndWidth(int.Parse(height), int.Parse(width));

        Console.WriteLine(_grid);
        
        return "hi";
    }
}