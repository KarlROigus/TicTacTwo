namespace GameBrain;

public class SpotOnTheBoard
{
    private EGamePiece _piece;
    private bool _isPartOfGrid;

    public bool IsPartOfGrid
    {
        get => _isPartOfGrid;
        private set => _isPartOfGrid = value;
    }

    public SpotOnTheBoard(EGamePiece piece, bool isPartOfGrid)
    {
        _piece = piece;
        IsPartOfGrid = isPartOfGrid;
    }

    public EGamePiece GetSpotValue()
    {
        return _piece;
    }

    public bool SetSpotValue(EGamePiece newPiece)
    {
        _piece = newPiece;
        return true;
    }

    public override string ToString()
    {
        return _piece.ToString() + ", " + _isPartOfGrid;
    }
    
}