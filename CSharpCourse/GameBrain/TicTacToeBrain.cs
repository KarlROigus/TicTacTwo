﻿namespace GameBrain;

public class TicTacToeBrain
{
    private EGamePiece[,] _gameBoard;
    private EGamePiece _nextMoveBy { get; set; } = EGamePiece.X;

    private TicTacToeBrain(int boardX, int boardY)
    {
        _gameBoard = new EGamePiece[boardX, boardY];
    }

    public TicTacToeBrain(int boardSize) : this(boardSize, boardSize) {}

   

    public EGamePiece[,] GameBoard
    {
        get => GetBoard();
        private set => _gameBoard = value;
    }
 
    public int DimX => _gameBoard.GetLength(0);
    public int DimY => _gameBoard.GetLength(1);

    private EGamePiece[,] GetBoard()
    {
        var copyOfBoard = new EGamePiece[_gameBoard.GetLength(0), _gameBoard.GetLength(1)];

        for (var i = 0; i < _gameBoard.GetLength(0); i++)
        {
            for (var y = 0; y < _gameBoard.GetLength(1); y++)
            {
                copyOfBoard[i, y] = _gameBoard[i, y];
            }
        }

        return copyOfBoard;

    }

    public bool MakeAMove(int x, int y)
    {
        if (_gameBoard[x, y] != EGamePiece.Empty)
        {
            return false;
        }

        _gameBoard[x, y] = _nextMoveBy;
        _nextMoveBy = _nextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
        return true;
    }

    public void ResetGame()
    {
        _gameBoard = new EGamePiece[_gameBoard.GetLength(0), _gameBoard.GetLength(1)];
        _nextMoveBy = EGamePiece.X;

    }
}