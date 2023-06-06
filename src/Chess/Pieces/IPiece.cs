using Chess.Data;

namespace Chess.Pieces
{
    public interface IPiece
    {
        PlayerColor Color { get; }
        string DisplayChar { get; }
        Squere[] GetAvaliableSqueres(Board board);
    }
}
